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
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration
{
    [TestFixture]
    public abstract class IntegrationTest
    {
        private const Int32 ConfidentIterations = 1000000;
        private const Int32 TimeLimitInSeconds = 10;

        private DateTime start;
        private Int32 iterations;

        [SetUp]
        public void Setup()
        {
            var kernel = new StandardKernel();
            var loader = new ModuleLoader();
            loader.LoadModules(kernel);

            AddTestBindings(kernel);

            kernel.Inject(this);
        }

        private void AddTestBindings(IKernel kernel)
        {
            kernel.Bind<Alignment>().ToMethod(c => GenerateAlignment(c.Kernel)).InSingletonScope();
            kernel.Bind<CharacterClass>().ToMethod(c => c.Kernel.Get<ICharacterClassFactory>().CreateWith(
                c.Kernel.Get<CharacterClassPrototype>())).InSingletonScope();
            kernel.Bind<CharacterClassPrototype>().ToMethod(c => GenerateCharacterClassPrototype(c.Kernel)).InSingletonScope();
            kernel.Bind<Race>().ToMethod(c => GenerateRace(c.Kernel)).InSingletonScope();
            kernel.Bind<Dictionary<String, Stat>>().ToMethod(c => c.Kernel.Get<IStatsFactory>().CreateWith(c.Kernel.Get<RawStatsRandomizer>(),
                c.Kernel.Get<CharacterClass>(), c.Kernel.Get<Race>())).InSingletonScope();
        }

        private Alignment GenerateAlignment(IKernel kernel)
        {
            Alignment alignment;

            do alignment = kernel.Get<IAlignmentFactory>().CreateWith(kernel.Get<AnyAlignmentRandomizer>());
            while (!kernel.Get<IRandomizerVerifier>().VerifyAlignmentCompatibility(alignment, kernel.Get<AnyClassNameRandomizer>(),
                kernel.Get<AnyLevelRandomizer>(), kernel.Get<AnyBaseRaceRandomizer>(), kernel.Get<AnyMetaraceRandomizer>()));

            return alignment;
        }

        private CharacterClassPrototype GenerateCharacterClassPrototype(IKernel kernel)
        {
            CharacterClassPrototype prototype;

            do prototype = kernel.Get<ICharacterClassFactory>().CreatePrototypeWith(kernel.Get<Alignment>(),
                kernel.Get<AnyLevelRandomizer>(), kernel.Get<AnyClassNameRandomizer>());
            while (!kernel.Get<IRandomizerVerifier>().VerifyCharacterClassCompatibility(kernel.Get<Alignment>().Goodness, prototype,
                kernel.Get<AnyBaseRaceRandomizer>(), kernel.Get<AnyMetaraceRandomizer>()));

            return prototype;
        }

        private Race GenerateRace(IKernel kernel)
        {
            Race race;
            var levelAdjustments = kernel.Get<ILevelAdjustmentsProvider>().GetLevelAdjustments();

            do race = kernel.Get<IRaceFactory>().CreateWith(kernel.Get<Alignment>().Goodness, kernel.Get<CharacterClassPrototype>(),
                kernel.Get<AnyBaseRaceRandomizer>(), kernel.Get<AnyMetaraceRandomizer>());
            while (levelAdjustments[race.BaseRace] + levelAdjustments[race.Metarace] >= kernel.Get<CharacterClassPrototype>().Level);

            return race;
        }

        protected void StartTest()
        {
            start = DateTime.Now;
            iterations = 0;
        }

        protected Boolean TestShouldKeepRunning()
        {
            return DateTime.Now.Subtract(start).Seconds < TimeLimitInSeconds && iterations++ < ConfidentIterations;
        }
    }
}