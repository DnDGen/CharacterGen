using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AquaticBaseRaceRandomizerTests
    {
        private RaceRandomizer aquaticBaseRaceRandomizer;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionSelector;

        private string firstAquaticBaseRace = "first aquatic base race";
        private string secondAquaticBaseRace = "second aquatic base race";
        private CharacterClass characterClass;
        private Dictionary<string, int> adjustments;
        private Alignment alignment;
        private List<string> alignmentRaces;
        private List<string> aquaticBaseRaces;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionSelector = new Mock<ICollectionsSelector>();
            aquaticBaseRaceRandomizer = new AquaticBaseRaceRandomizer(mockAdjustmentsSelector.Object, mockCollectionSelector.Object);

            alignment = new Alignment();
            characterClass = new CharacterClass();
            alignmentRaces = new List<string>();
            aquaticBaseRaces = new List<string>() { firstAquaticBaseRace, secondAquaticBaseRace };
            adjustments = new Dictionary<string, int>();

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();

            alignmentRaces.Add(firstAquaticBaseRace);
            alignmentRaces.Add(secondAquaticBaseRace);

            characterClass.Name = "class name";
            characterClass.Level = 1;

            foreach (var baseRace in aquaticBaseRaces)
                adjustments.Add(baseRace, 0);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, It.IsAny<string>())).Returns((string table, string name) => adjustments[name]);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full)).Returns(alignmentRaces);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Aquatic)).Returns(aquaticBaseRaces);

            var index = 0;
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            var baseRace = aquaticBaseRaceRandomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(firstAquaticBaseRace));
            mockCollectionSelector.Verify(p => p.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Aquatic), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockCollectionSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Aquatic)).Returns(Enumerable.Empty<string>());
            Assert.That(() => aquaticBaseRaceRandomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            var baseRaces = aquaticBaseRaceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.EquivalentTo(aquaticBaseRaces));
            mockCollectionSelector.Verify(p => p.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Aquatic), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutBaseRacesWithTooHighLevelAdjustments()
        {
            adjustments[firstAquaticBaseRace] = 1;

            var results = aquaticBaseRaceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondAquaticBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutBaseRacesNotAllowedByAlignment()
        {
            alignmentRaces.Remove(firstAquaticBaseRace);

            var results = aquaticBaseRaceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondAquaticBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }
    }
}