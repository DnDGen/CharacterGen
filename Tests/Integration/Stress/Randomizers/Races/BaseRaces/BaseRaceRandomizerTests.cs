using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : StressTests
    {
        protected abstract IEnumerable<String> particularBaseRaces { get; }

        private IEnumerable<String> baseRaces;

        [SetUp]
        public void Setup()
        {
            baseRaces = particularBaseRaces;
        }

        protected override void MakeAssertions()
        {
            var data = GetNewDependentData();
            var baseRace = BaseRaceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
            Assert.That(baseRaces, Contains.Item(baseRace));
        }
    }
}