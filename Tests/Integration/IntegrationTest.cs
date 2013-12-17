using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Bootstrap;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration
{
    [TestFixture]
    public abstract class IntegrationTest
    {
        protected const Int32 ConfidenceLevel = 10000;

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
            kernel.Bind<Alignment>().ToMethod(c => c.Kernel.Get<IAlignmentFactory>().CreateWith(c.Kernel.Get<AnyAlignmentRandomizer>()));
            kernel.Bind<CharacterClass>().ToMethod(c => c.Kernel.Get<ICharacterClassFactory>().CreateWith(c.Kernel.Get<CharacterClassPrototype>()));
            kernel.Bind<CharacterClassPrototype>().ToMethod(c => c.Kernel.Get<ICharacterClassFactory>().CreatePrototypeWith(c.Kernel.Get<Alignment>(),
                c.Kernel.Get<AnyLevelRandomizer>(), c.Kernel.Get<AnyClassNameRandomizer>()));
            kernel.Bind<Dictionary<String, Stat>>().ToMethod(c => c.Kernel.Get<IStatsFactory>().CreateWith(c.Kernel.Get<RawStatsRandomizer>(),
                c.Kernel.Get<CharacterClass>(), c.Kernel.Get<Race>()));
            kernel.Bind<Race>().ToMethod(c => c.Kernel.Get<IRaceFactory>().CreateWith(c.Kernel.Get<Alignment>().Goodness, c.Kernel.Get<CharacterClassPrototype>(),
                c.Kernel.Get<AnyBaseRaceRandomizer>(), c.Kernel.Get<AnyMetaraceRandomizer>()));
        }
    }
}