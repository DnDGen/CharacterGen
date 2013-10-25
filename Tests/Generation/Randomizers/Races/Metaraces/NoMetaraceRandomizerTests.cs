using System;
using System.Linq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
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
            Assert.That(metaraces.First(), Is.EqualTo(String.Empty));
            Assert.That(metaraces.Count(), Is.EqualTo(1));
        }
    }
}