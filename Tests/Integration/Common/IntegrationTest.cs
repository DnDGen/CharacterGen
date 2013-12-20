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
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Common
{
    [TestFixture]
    public abstract class IntegrationTest
    {
        protected IKernel kernel;

        public IntegrationTest()
        {
            kernel = new StandardKernel();

            var npcGenLoader = new NPCGenModuleLoader();
            npcGenLoader.LoadModules(kernel);

            LoadDefaultDataBindings();

            kernel.Inject(this);
        }

        private void LoadDefaultDataBindings()
        {
            kernel.Bind<Alignment>().ToMethod(c => GenerateAlignment(c.Kernel));
            kernel.Bind<CharacterClass>().ToMethod(c => c.Kernel.Get<ICharacterClassFactory>().CreateWith(c.Kernel.Get<CharacterClassPrototype>()));
            kernel.Bind<CharacterClassPrototype>().ToMethod(c => GenerateCharacterClassPrototype(c.Kernel));
            kernel.Bind<Race>().ToMethod(c => GenerateRace(c.Kernel));
            kernel.Bind<Dictionary<String, Stat>>().ToMethod(c => c.Kernel.Get<IStatsFactory>().CreateWith(c.Kernel.Get<RawStatsRandomizer>(),
                c.Kernel.Get<CharacterClass>(), c.Kernel.Get<Race>()));
        }

        private Alignment GenerateAlignment(IKernel kernel)
        {
            Alignment alignment;
            var factory = kernel.Get<IAlignmentFactory>();
            var verifier = kernel.Get<IRandomizerVerifier>();
            var alignmentRandomizer = GetAlignmentRandomizer(kernel);
            var classNameRandomizer = GetClassNameRandomizer(kernel);
            var levelRandomizer = GetLevelRandomizer(kernel);
            var baseRaceRandomizer = GetBaseRaceRandomizer(kernel);
            var metaraceRandomizer = GetMetaraceRandomizer(kernel);

            do alignment = factory.CreateWith(alignmentRandomizer);
            while (!verifier.VerifyAlignmentCompatibility(alignment, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));

            return alignment;
        }

        protected virtual IAlignmentRandomizer GetAlignmentRandomizer(IKernel kernel)
        {
            return kernel.Get<AnyAlignmentRandomizer>();
        }

        protected virtual IClassNameRandomizer GetClassNameRandomizer(IKernel kernel)
        {
            return kernel.Get<AnyClassNameRandomizer>();
        }

        protected virtual ILevelRandomizer GetLevelRandomizer(IKernel kernel)
        {
            return kernel.Get<AnyLevelRandomizer>();
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

        protected virtual CharacterClassPrototype GenerateCharacterClassPrototype(IKernel kernel)
        {
            CharacterClassPrototype prototype;
            var alignment = kernel.Get<Alignment>();
            var factory = kernel.Get<ICharacterClassFactory>();
            var verifier = kernel.Get<IRandomizerVerifier>();
            var classNameRandomizer = GetClassNameRandomizer(kernel);
            var levelRandomizer = GetLevelRandomizer(kernel);
            var baseRaceRandomizer = GetBaseRaceRandomizer(kernel);
            var metaraceRandomizer = GetMetaraceRandomizer(kernel);

            do prototype = factory.CreatePrototypeWith(alignment, levelRandomizer, classNameRandomizer);
            while (!verifier.VerifyCharacterClassCompatibility(alignment.Goodness, prototype, baseRaceRandomizer, metaraceRandomizer));

            return prototype;
        }

        protected virtual Race GenerateRace(IKernel kernel)
        {
            Race race;
            var alignment = kernel.Get<Alignment>();
            var prototype = kernel.Get<CharacterClassPrototype>();
            var levelAdjustments = kernel.Get<ILevelAdjustmentsProvider>().GetLevelAdjustments();
            var factory = kernel.Get<IRaceFactory>();
            var baseRaceRandomizer = GetBaseRaceRandomizer(kernel);
            var metaraceRandomizer = GetMetaraceRandomizer(kernel);

            do race = factory.CreateWith(alignment.Goodness, prototype, baseRaceRandomizer, metaraceRandomizer);
            while (levelAdjustments[race.BaseRace] + levelAdjustments[race.Metarace] >= prototype.Level);

            return race;
        }
    }
}