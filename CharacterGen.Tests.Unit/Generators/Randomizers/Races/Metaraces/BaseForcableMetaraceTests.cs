using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Randomizers.Races.Metaraces;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class BaseForcableMetaraceTests
    {
        private TestMetaraceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

        private string firstMetarace = "first metarace";
        private string secondMetarace = "second metarace";
        private CharacterClass characterClass;
        private Dictionary<string, int> adjustments;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            randomizer = new TestMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object);

            adjustments = new Dictionary<string, int>();
            alignment = new Alignment();
            characterClass = new CharacterClass();

            var metaraces = new[] { firstMetarace, secondMetarace, RaceConstants.Metaraces.None };
            foreach (var metarace in metaraces)
                adjustments[metarace] = 0;

            characterClass.Level = 1;

            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(metaraces);
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstMetarace);
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromPercentileResultSelector()
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
        public void RandomizeReturnsMetaraceFromSelector()
        {
            var result = randomizer.Randomize(alignment, characterClass);
            Assert.That(result, Is.EqualTo(firstMetarace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameMetaraces()
        {
            characterClass.ClassName = "className";
            alignment.Goodness = "goodness";

            randomizer.Randomize(alignment, characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, "goodness", characterClass.ClassName);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedMetaraceIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>())).Returns("invalid metarace")
                .Returns(firstMetarace);

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
        public void GetAllPossibleResultsGetsNonEmptyResults()
        {
            var races = randomizer.GetAllPossible(alignment, characterClass);

            Assert.That(races, Contains.Item(firstMetarace));
            Assert.That(races, Contains.Item(secondMetarace));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            alignment.Goodness = "goodness";

            randomizer.GetAllPossible(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("goodnessclassNameMetaraces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.ForbiddenMetarace = firstMetarace;
            var results = randomizer.GetAllPossible(alignment, characterClass);

            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        [Test]
        public void IfForceMetaraceIsTrueThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.ForceMetarace = true;
            var results = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Is.All.Not.EqualTo(RaceConstants.Metaraces.None));
        }

        [Test]
        public void IfForceMetaraceIsFalseThenEmptyMetaraceIsAllowed()
        {
            randomizer.ForceMetarace = false;
            var results = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(RaceConstants.Metaraces.None));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesWithTooHighLevelAdjustments()
        {
            adjustments[firstMetarace] = 1;

            var results = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        private class TestMetaraceRandomizer : BaseForcableMetarace
        {
            public string ForbiddenMetarace { get; set; }

            public TestMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector)
                : base(percentileResultSelector, levelAdjustmentsSelector, new ConfigurableIterationGenerator(2))
            { }

            protected override bool MetaraceIsAllowed(string metarace)
            {
                return metarace != ForbiddenMetarace;
            }
        }
    }
}