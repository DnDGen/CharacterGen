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
    public class BaseRaceRandomizerTests : StressTest
    {
        [Inject]
        public NonGoodBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        private IEnumerable<String> baseRaces;

        protected override IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<NonGoodBaseRaceRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            var goodBaseRaces = new[]
                {
                    RaceConstants.BaseRaces.Aasimar,
                    RaceConstants.BaseRaces.Svirfneblin
                };

            baseRaces = RaceConstants.BaseRaces.GetBaseRaces().Except(goodBaseRaces);

            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void NonGoodBaseRaceRandomizerAlwaysReturnsNonGoodBaseRace()
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