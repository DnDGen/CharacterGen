using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Bootstrap;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Common
{
    [TestFixture]
    public abstract class IntegrationTest
    {
        private IKernel kernel;

        public IntegrationTest()
        {
            kernel = new StandardKernel();

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
            var factory = kernel.Get<IStatsFactory>();
            var randomizer = kernel.Get<IStatsRandomizer>();
            var data = kernel.Get<DependentDataCollection>();

            return factory.CreateWith(randomizer, data.CharacterClass, data.Race);
        }

        private Alignment GenerateAlignment(IKernel kernel)
        {
            Alignment alignment;
            var factory = kernel.Get<IAlignmentFactory>();
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
            var factory = kernel.Get<ICharacterClassFactory>();
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
            var levelAdjustments = kernel.Get<ILevelAdjustmentsProvider>().GetLevelAdjustments();
            var factory = kernel.Get<IRaceFactory>();
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

            var factory = kernel.Get<ICharacterClassFactory>();
            collection.CharacterClass = factory.CreateWith(collection.CharacterClassPrototype);

            return collection;
        }
    }
}