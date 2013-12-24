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
    public class NonNeutralBaseRaceRandomizerTests : StressTest
    {
        [Inject]
        public NonNeutralBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        private IEnumerable<String> baseRaces;

        protected override IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<NonNeutralBaseRaceRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            var neutralBaseRaces = new[]
                {
                    RaceConstants.BaseRaces.Doppelganger,
                    RaceConstants.BaseRaces.Lizardfolk
                };

            baseRaces = RaceConstants.BaseRaces.GetBaseRaces().Except(neutralBaseRaces);

            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void NonNeutralBaseRaceRandomizerAlwaysReturnsNonNeutralBaseRace()
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