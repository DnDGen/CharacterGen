using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseRaceRandomizerTests
    {
        private TestBaseRaceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionSelector;

        private string firstBaseRace = "first base race";
        private string secondBaseRace = "second base race";
        private CharacterClass characterClass;
        private Dictionary<string, int> adjustments;
        private Alignment alignment;
        private List<string> alignmentRaces;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionSelector = new Mock<ICollectionsSelector>();
            randomizer = new TestBaseRaceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object, mockCollectionSelector.Object);

            alignment = new Alignment();
            characterClass = new CharacterClass();
            alignmentRaces = new List<string>();
            adjustments = new Dictionary<string, int>();

            characterClass.Name = "class name";
            characterClass.Level = 1;

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();

            alignmentRaces.Add(firstBaseRace);
            alignmentRaces.Add(secondBaseRace);

            var baseRaces = new[] { firstBaseRace, secondBaseRace, string.Empty };

            foreach (var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);

            mockPercentileResultSelector.Setup(s => s.SelectAllFrom(It.IsAny<string>())).Returns(baseRaces);
            mockPercentileResultSelector.Setup(s => s.SelectFrom(It.IsAny<string>())).Returns(firstBaseRace);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, It.IsAny<string>())).Returns((string table, string name) => adjustments[name]);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full)).Returns(alignmentRaces);

            var index = 0;
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(Enumerable.Empty<string>());
            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeReturnsBaseRaceFromPercentileResultSelector()
        {
            var result = randomizer.Randomize(alignment, characterClass);
            Assert.That(result, Is.EqualTo(firstBaseRace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            randomizer.Randomize(alignment, characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.Name);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeRerollsEmptyBaseRace()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(string.Empty)
                .Returns(firstBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(firstBaseRace));
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidBaseRaceBecauseNotAllowed()
        {
            randomizer.ForbiddenBaseRace = firstBaseRace;

            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstBaseRace)
                .Returns(secondBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(secondBaseRace));
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidBaseRaceBecauseOfAdjustment()
        {
            adjustments[firstBaseRace] = 1;

            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstBaseRace)
                .Returns(secondBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(secondBaseRace));
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidBaseRaceBecauseAlignmentDoesNotAllow()
        {
            alignmentRaces.Remove(firstBaseRace);

            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstBaseRace)
                .Returns(secondBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(secondBaseRace));
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeReturnsDefaultBaseRace()
        {
            alignmentRaces.Remove(firstBaseRace);

            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(secondBaseRace));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            randomizer.GetAllPossible(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutEmptyStrings()
        {
            var classNames = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(classNames, Contains.Item(firstBaseRace));
            Assert.That(classNames, Contains.Item(secondBaseRace));
            Assert.That(classNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.Name = "className";
            alignment.Goodness = "goodness";

            randomizer.GetAllPossible(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("goodnessclassNameBaseRaces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.ForbiddenBaseRace = firstBaseRace;

            var results = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutBaseRacesWithTooHighLevelAdjustments()
        {
            adjustments[firstBaseRace] = 1;

            var results = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutBaseRacesNotAllowedByAlignment()
        {
            alignmentRaces.Remove(firstBaseRace);

            var results = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private class TestBaseRaceRandomizer : BaseRaceRandomizerBase
        {
            public string ForbiddenBaseRace { get; set; }

            public TestBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, ICollectionsSelector collectionSelector)
                : base(percentileResultSelector, levelAdjustmentSelector, new ConfigurableIterationGenerator(2), collectionSelector)
            { }

            protected override bool BaseRaceIsAllowedByRandomizer(string baseRace)
            {
                return baseRace != ForbiddenBaseRace;
            }
        }
    }
}