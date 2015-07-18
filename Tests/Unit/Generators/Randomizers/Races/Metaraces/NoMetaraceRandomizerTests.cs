using System;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
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