using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests
    {
        private RaceRandomizer randomizer;
        private Alignment alignment;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            randomizer = new NoMetaraceRandomizer();
            alignment = new Alignment();
            characterClass = new CharacterClass();
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
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.None));
            Assert.That(metaraces.Count(), Is.EqualTo(1));
        }
    }
}