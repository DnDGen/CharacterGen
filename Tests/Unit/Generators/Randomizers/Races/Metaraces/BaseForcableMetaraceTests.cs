using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class BaseForcableMetaraceTests
    {
        private TestMetaraceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<INameSelector> mockNameSelector;

        private String firstMetarace = "first metarace";
        private String secondMetarace = "second metarace";
        private String firstMetaraceId = "firstmetarace";
        private String secondMetaraceId = "secondmetarace";
        private CharacterClass characterClass;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var metaraceIds = new[] { firstMetaraceId, secondMetaraceId, RaceConstants.Metaraces.None };
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(metaraceIds);
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<String>())).Returns(firstMetaraceId);

            adjustments = new Dictionary<String, Int32>();
            foreach (var metaraceId in metaraceIds)
                adjustments.Add(metaraceId, 0);

            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);

            characterClass = new CharacterClass();
            characterClass.Level = 1;

            mockNameSelector = new Mock<INameSelector>();
            mockNameSelector.Setup(s => s.Select(firstMetaraceId)).Returns(firstMetarace);
            mockNameSelector.Setup(s => s.Select(secondMetaraceId)).Returns(secondMetarace);

            randomizer = new TestMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object, mockNameSelector.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromPercentileResultSelector()
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
        public void RandomizeReturnsMetaraceFromSelector()
        {
            var result = randomizer.Randomize(String.Empty, characterClass);
            Assert.That(result.Id, Is.EqualTo(firstMetaraceId));
            Assert.That(result.Name, Is.EqualTo(firstMetarace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameMetaraces()
        {
            characterClass.ClassName = "className";
            randomizer.Randomize("goodness", characterClass);
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, "goodness", characterClass.ClassName);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedMetaraceIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<String>())).Returns("invalid metarace")
                .Returns(firstMetarace);

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
        public void GetAllPossibleResultsGetsNonEmptyResults()
        {
            var classNames = randomizer.GetAllPossibleIds(String.Empty, characterClass);

            Assert.That(classNames, Contains.Item(firstMetarace));
            Assert.That(classNames, Contains.Item(secondMetarace));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.GetAllPossibleIds("goodness", characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("goodnessclassNameMetaraces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.NotAllowedMetarace = firstMetarace;
            var results = randomizer.GetAllPossibleIds(String.Empty, characterClass);

            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.Not.Contains(firstMetarace));
        }

        [Test]
        public void IfForceMetaraceIsTrueThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.ForceMetarace = true;
            var results = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            Assert.That(results, Is.Not.Contains(String.Empty));
        }

        [Test]
        public void IfForceMetaraceIsFalseThenEmptyMetaraceIsAllowed()
        {
            randomizer.ForceMetarace = false;
            var results = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            Assert.That(results, Contains.Item(String.Empty));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesWithTooHighLevelAdjustments()
        {
            adjustments[firstMetarace] = 1;

            var results = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.Not.Contains(firstMetarace));
        }

        private class TestMetaraceRandomizer : BaseForcableMetarace
        {
            public String NotAllowedMetarace { get; set; }

            public TestMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, INameSelector nameSelector)
                : base(percentileResultSelector, levelAdjustmentsSelector, nameSelector) { }

            protected override Boolean MetaraceIsAllowed(String metarace)
            {
                return metarace != NotAllowedMetarace;
            }
        }
    }
}