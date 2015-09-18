using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Selectors;
using Moq;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnimalBaseRaceRandomizerTests
    {
        private RaceRandomizer animalBaseRaceRandomizer;
        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            animalBaseRaceRandomizer = new AnimalBaseRaceRandomizer();
        }
    }
}
