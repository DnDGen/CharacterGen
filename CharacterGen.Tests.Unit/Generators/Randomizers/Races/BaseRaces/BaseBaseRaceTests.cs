using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
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

        private string firstBaseRace = "first base race";
        private string secondBaseRace = "second base race";
        private CharacterClass characterClass;
        private Dictionary<string, int> adjustments;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockLevelAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            randomizer = new TestBaseRaceRandomizer(mockPercentileResultSelector.Object, mockLevelAdjustmentsSelector.Object);

            characterClass = new CharacterClass();
            alignment = new Alignment();

            var baseRaces = new[] { firstBaseRace, secondBaseRace, string.Empty };

            adjustments = new Dictionary<string, int>();
            foreach (var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);

            characterClass.Level = 1;

            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(baseRaces);
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstBaseRace);
            mockLevelAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);

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
            characterClass.Name = "className";
            alignment.Goodness = "goodness";

            randomizer.Randomize(alignment, characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, "goodness", characterClass.Name);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedBaseRaceIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>())).Returns("invalid base race")
                .Returns(firstBaseRace);

            randomizer.Randomize(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
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

        private class TestBaseRaceRandomizer : BaseRaceRandomizer
        {
            public string ForbiddenBaseRace { get; set; }

            public TestBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector)
                : base(percentileResultSelector, levelAdjustmentSelector, new ConfigurableIterationGenerator(2))
            { }

            protected override bool BaseRaceIsAllowed(string baseRace)
            {
                return baseRace != ForbiddenBaseRace;
            }
        }
    }
}