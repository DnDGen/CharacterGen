using System;
using System.Collections.Generic;
using D20Dice.Bootstrap;
using EquipmentGen.Bootstrap;
using Ninject;
using NPCGen.Bootstrap;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Randomizers.Alignments;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Generators.Randomizers.Stats;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Common
{
    [TestFixture]
    public abstract class IntegrationTests
    {
        private IKernel kernel;

        public IntegrationTests()
        {
            kernel = new StandardKernel();

            var diceLoader = new D20DiceModuleLoader();
            diceLoader.LoadModules(kernel);

            var equipmentGenLoader = new EquipmentGenModuleLoader();
            equipmentGenLoader.LoadModules(kernel);

            var npcGenLoader = new NPCGenModuleLoader();
            npcGenLoader.LoadModules(kernel);

            LoadRandomizerBindings();
            LoadDataBindings();

            kernel.Inject(this);
        }

        private void LoadRandomizerBindings()
        {
            kernel.Bind<IAlignmentRandomizer>().To<AnyAlignmentRandomizer>();
            kernel.Bind<IClassNameRandomizer>().ToMethod(c => GetClassNameRandomizer(c.Kernel));
            kernel.Bind<ILevelRandomizer>().To<AnyLevelRandomizer>();
            kernel.Bind<IBaseRaceRandomizer>().ToMethod(c => GetBaseRaceRandomizer(c.Kernel));
            kernel.Bind<IMetaraceRandomizer>().ToMethod(c => GetMetaraceRandomizer(c.Kernel));
            kernel.Bind<IStatsRandomizer>().To<RawStatsRandomizer>();
        }

        protected virtual IClassNameRandomizer GetClassNameRandomizer(IKernel kernel)
        {
            return kernel.Get<AnyClassNameRandomizer>();
        }

        protected virtual IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<AnyBaseRaceRandomizer>();
        }

        protected virtual IMetaraceRandomizer GetMetaraceRandomizer(IKernel kernel)
        {
            var randomizer = kernel.Get<AnyMetaraceRandomizer>();
            randomizer.AllowNoMetarace = true;
            return randomizer;
        }

        private void LoadDataBindings()
        {
            kernel.Bind<DependentDataCollection>().ToMethod(c => GetNewInstanceOfDependentData(c.Kernel));
            kernel.Bind<Dictionary<String, Stat>>().ToMethod(c => GetStats(c.Kernel));
        }

        private Dictionary<String, Stat> GetStats(IKernel kernel)
        {
            var factory = kernel.Get<IStatsGenerator>();
            var randomizer = kernel.Get<IStatsRandomizer>();
            var data = kernel.Get<DependentDataCollection>();

            return factory.CreateWith(randomizer, data.CharacterClass, data.Race);
        }

        private Alignment GenerateAlignment(IKernel kernel)
        {
            Alignment alignment;
            var factory = kernel.Get<IAlignmentGenerator>();
            var verifier = kernel.Get<IRandomizerVerifier>();
            var alignmentRandomizer = kernel.Get<IAlignmentRandomizer>();
            var classNameRandomizer = kernel.Get<IClassNameRandomizer>();
            var levelRandomizer = kernel.Get<ILevelRandomizer>();
            var baseRaceRandomizer = kernel.Get<IBaseRaceRandomizer>();
            var metaraceRandomizer = kernel.Get<IMetaraceRandomizer>();

            do alignment = factory.CreateWith(alignmentRandomizer);
            while (!verifier.VerifyAlignmentCompatibility(alignment, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));

            return alignment;
        }

        private CharacterClassPrototype GenerateCharacterClassPrototype(IKernel kernel, Alignment alignment)
        {
            CharacterClassPrototype prototype;
            var factory = kernel.Get<ICharacterClassGenerator>();
            var verifier = kernel.Get<IRandomizerVerifier>();
            var classNameRandomizer = kernel.Get<IClassNameRandomizer>();
            var levelRandomizer = kernel.Get<ILevelRandomizer>();
            var baseRaceRandomizer = kernel.Get<IBaseRaceRandomizer>();
            var metaraceRandomizer = kernel.Get<IMetaraceRandomizer>();

            do prototype = factory.CreatePrototypeWith(alignment, levelRandomizer, classNameRandomizer);
            while (!verifier.VerifyCharacterClassCompatibility(alignment.Goodness, prototype, baseRaceRandomizer, metaraceRandomizer));

            return prototype;
        }

        private Race GenerateRace(IKernel kernel, Alignment alignment, CharacterClassPrototype prototype)
        {
            Race race;
            var levelAdjustments = kernel.Get<ILevelAdjustmentsSelector>().GetLevelAdjustments();
            var factory = kernel.Get<IRaceGenerator>();
            var baseRaceRandomizer = kernel.Get<IBaseRaceRandomizer>();
            var metaraceRandomizer = kernel.Get<IMetaraceRandomizer>();

            do race = factory.CreateWith(alignment.Goodness, prototype, baseRaceRandomizer, metaraceRandomizer);
            while (levelAdjustments[race.BaseRace] + levelAdjustments[race.Metarace] >= prototype.Level);

            return race;
        }

        protected T GetNewInstanceOf<T>()
        {
            return kernel.Get<T>();
        }

        private DependentDataCollection GetNewInstanceOfDependentData(IKernel kernel)
        {
            var collection = new DependentDataCollection();

            collection.Alignment = GenerateAlignment(kernel);
            collection.CharacterClassPrototype = GenerateCharacterClassPrototype(kernel, collection.Alignment);
            collection.Race = GenerateRace(kernel, collection.Alignment, collection.CharacterClassPrototype);

            var factory = kernel.Get<ICharacterClassGenerator>();
            collection.CharacterClass = factory.CreateWith(collection.CharacterClassPrototype);

            return collection;
        }
    }
}