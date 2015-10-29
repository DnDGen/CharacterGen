using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : StressTests
    {
        protected abstract IEnumerable<String> allowedBaseRaces { get; }

        private IEnumerable<String> baseRaces;

        [SetUp]
        public void Setup()
        {
            baseRaces = allowedBaseRaces;
        }

        private readonly String testType;

        public BaseRaceRandomizerTests()
        {
            var classType = GetType().ToString();
            var segments = classType.Split('.');
            testType = segments.Last();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var baseRace = BaseRaceRandomizer.Randomize(alignment, characterClass);
            Assert.That(baseRaces, Contains.Item(baseRace), testType);
        }
    }
}