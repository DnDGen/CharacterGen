using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseBaseRaceTests
    {
        private TestBaseRaceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<IAdjustmentsSelector> mockLevelAdjustmentsSelector;
        private Mock<INameSelector> mockNameSelector;

        private String firstBaseRaceId = "firstbaserace";
        private String secondBaseRaceId = "secondbaserace";
        private String firstBaseRace = "first base race";
        private String secondBaseRace = "second base race";
        private CharacterClass characterClass;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var baseRaceIds = new[] { firstBaseRaceId, secondBaseRaceId, String.Empty };
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(baseRaceIds);
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<String>())).Returns(firstBaseRaceId);

            adjustments = new Dictionary<String, Int32>();
            foreach (var baseRace in baseRaceIds)
                adjustments.Add(baseRace, 0);

            mockLevelAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockLevelAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);

            characterClass = new CharacterClass();
            characterClass.Level = 1;

            mockNameSelector = new Mock<INameSelector>();
            mockNameSelector.Setup(s => s.Select(firstBaseRaceId)).Returns(firstBaseRace);
            mockNameSelector.Setup(s => s.Select(secondBaseRaceId)).Returns(secondBaseRace);

            randomizer = new TestBaseRaceRandomizer(mockPercentileResultSelector.Object, mockLevelAdjustmentsSelector.Object, mockNameSelector.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            Assert.That(() => randomizer.Randomize(String.Empty, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeReturnsBaseRaceFromPercentileResultSelector()
        {
            var result = randomizer.Randomize(String.Empty, characterClass);
            Assert.That(result.Id, Is.EqualTo(firstBaseRaceId));
            Assert.That(result.Name, Is.EqualTo(firstBaseRace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.Randomize("goodness", characterClass);
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, "goodness", characterClass.ClassName);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedBaseRaceIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<String>())).Returns("invalid base race")
                .Returns(firstBaseRaceId);

            randomizer.Randomize(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            randomizer.GetAllPossibleIds(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutEmptyStrings()
        {
            var classNames = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            Assert.That(classNames, Contains.Item(firstBaseRaceId));
            Assert.That(classNames, Contains.Item(secondBaseRaceId));
            Assert.That(classNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.GetAllPossibleIds("goodness", characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("goodnessclassNameBaseRaces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.NotAllowedBaseRaceId = firstBaseRaceId;

            var results = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            Assert.That(results, Contains.Item(secondBaseRaceId));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutBaseRacesWithTooHighLevelAdjustments()
        {
            adjustments[firstBaseRaceId] = 1;

            var results = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            Assert.That(results, Contains.Item(secondBaseRaceId));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private class TestBaseRaceRandomizer : BaseBaseRace
        {
            public String NotAllowedBaseRaceId { get; set; }

            public TestBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
                INameSelector nameSelector)
                : base(percentileResultSelector, levelAdjustmentSelector, nameSelector) { }

            protected override Boolean BaseRaceIsAllowed(String baseRaceId)
            {
                return baseRaceId != NotAllowedBaseRaceId;
            }
        }
    }
}