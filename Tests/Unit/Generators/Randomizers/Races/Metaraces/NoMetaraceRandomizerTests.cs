using System;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests
    {
        private NoMetaraceRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new NoMetaraceRandomizer();
        }

        [Test]
        public void RandomizeAlwaysReturnsEmptyString()
        {
            var metarace = randomizer.Randomize(String.Empty, new CharacterClass());
            Assert.That(metarace, Is.EqualTo(String.Empty));
        }

        [Test]
        public void GetAllPossibleResultsReturnsEnumerableOfEmptyString()
        {
            var metaraces = randomizer.GetAllPossibleResults(String.Empty, new CharacterClass());
            Assert.That(metaraces, Contains.Item(String.Empty));
            Assert.That(metaraces.Count(), Is.EqualTo(1));
        }
    }
}