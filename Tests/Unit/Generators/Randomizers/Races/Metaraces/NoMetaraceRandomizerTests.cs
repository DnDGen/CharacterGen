using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests
    {
        private IMetaraceRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new NoMetaraceRandomizer();
        }

        [Test]
        public void RandomizeAlwaysReturnsEmptyString()
        {
            var metarace = randomizer.Randomize(String.Empty, new CharacterClass());
            Assert.That(metarace, Is.EqualTo(RaceConstants.Metaraces.None));
        }

        [Test]
        public void GetAllPossibleResultsReturnsEnumerableOfEmptyString()
        {
            var metaraces = randomizer.GetAllPossible(String.Empty, new CharacterClass());
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.None));
            Assert.That(metaraces.Count(), Is.EqualTo(1));
        }
    }
}