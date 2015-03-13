using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : StressTests
    {
        protected abstract IEnumerable<String> allowedBaseRaces { get; }

        private IEnumerable<String> baseRaceIds;

        [SetUp]
        public void Setup()
        {
            baseRaceIds = allowedBaseRaces;
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var baseRace = BaseRaceRandomizer.Randomize(alignment.Goodness, characterClass);
            Assert.That(baseRaceIds, Contains.Item(baseRace.Id), testType);
        }
    }
}