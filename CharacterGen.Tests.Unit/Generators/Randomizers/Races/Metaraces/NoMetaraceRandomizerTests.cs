using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Randomizers.Races.Metaraces;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests
    {
        private RaceRandomizer randomizer;
        private Alignment alignment;
        private CharacterClassPrototype characterClass;

        [SetUp]
        public void Setup()
        {
            randomizer = new NoMetaraceRandomizer();
            alignment = new Alignment();
            characterClass = new CharacterClassPrototype();
        }

        [Test]
        public void RandomizeAlwaysReturnsEmptyString()
        {
            var metarace = randomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(RaceConstants.Metaraces.None));
        }

        [Test]
        public void GetAllPossibleResultsReturnsEnumerableOfEmptyString()
        {
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces.Single(), Is.EqualTo(RaceConstants.Metaraces.None));
        }
    }
}