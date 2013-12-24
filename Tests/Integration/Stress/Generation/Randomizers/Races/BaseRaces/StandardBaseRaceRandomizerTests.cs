using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class StandardBaseRaceRandomizerTests : StressTest
    {
        [Inject]
        public StandardBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        private IEnumerable<String> baseRaces;

        protected override IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<StandardBaseRaceRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            baseRaces = new[]
                {
                    RaceConstants.BaseRaces.RockGnome,
                    RaceConstants.BaseRaces.HalfElf,
                    RaceConstants.BaseRaces.HalfOrc,
                    RaceConstants.BaseRaces.HighElf,
                    RaceConstants.BaseRaces.HillDwarf,
                    RaceConstants.BaseRaces.Human,
                    RaceConstants.BaseRaces.LightfootHalfling
                };

            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void StandardBaseRaceRandomizerAlwaysReturnsStandardBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var baseRace = BaseRaceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(baseRaces.Contains(baseRace), Is.True);
            }

            AssertIterations();
        }
    }
}