using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseBaseRaceTests
    {
        private TestBaseRaceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<IAdjustmentsSelector> mockLevelAdjustmentsSelector;

        private String firstBaseRace = "first base race";
        private String secondBaseRace = "second base race";
        private CharacterClass characterClass;
        private Dictionary<String, Int32> adjustments;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockLevelAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            randomizer = new TestBaseRaceRandomizer(mockPercentileResultSelector.Object, mockLevelAdjustmentsSelector.Object);

            characterClass = new CharacterClass();
            alignment = new Alignment();

            var baseRaces = new[] { firstBaseRace, secondBaseRace, String.Empty };

            adjustments = new Dictionary<String, Int32>();
            foreach (var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);

            characterClass.Level = 1;

            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(baseRaces);
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<String>())).Returns(firstBaseRace);
            mockLevelAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);

        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
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
            characterClass.ClassName = "className";
            alignment.Goodness = "goodness";

            randomizer.Randomize(alignment, characterClass);
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, "goodness", characterClass.ClassName);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedBaseRaceIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<String>())).Returns("invalid base race")
                .Returns(firstBaseRace);

            randomizer.Randomize(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            randomizer.GetAllPossible(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
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
            characterClass.ClassName = "className";
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

        private class TestBaseRaceRandomizer : BaseRaceRandomizer
        {
            public String ForbiddenBaseRace { get; set; }

            public TestBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector)
                : base(percentileResultSelector, levelAdjustmentSelector, new ConfigurableIterationGenerator(2))
            { }

            protected override Boolean BaseRaceIsAllowed(String baseRace)
            {
                return baseRace != ForbiddenBaseRace;
            }
        }
    }
}