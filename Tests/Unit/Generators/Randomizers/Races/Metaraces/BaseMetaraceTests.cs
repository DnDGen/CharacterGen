using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class BaseMetaraceTests
    {
        private TestMetaraceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

        private String firstMetarace = "first metarace";
        private String secondMetarace = "second metarace";
        private CharacterClass characterClass;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var metaraces = new[] { firstMetarace, secondMetarace, RaceConstants.Metaraces.None };
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(metaraces);
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<String>())).Returns(firstMetarace);

            adjustments = new Dictionary<String, Int32>();
            foreach (var metarace in metaraces)
                adjustments.Add(metarace, 0);

            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockAdjustmentsSelector.Setup(p => p.SelectFrom("LevelAdjustments")).Returns(adjustments);

            characterClass = new CharacterClass();
            characterClass.Level = 1;

            randomizer = new TestMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object);
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
        public void RandomizeReturnsMetaraceFromPercentileResultSelector()
        {
            var result = randomizer.Randomize(String.Empty, characterClass);
            Assert.That(result, Is.EqualTo(firstMetarace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameMetaraces()
        {
            characterClass.ClassName = "className";
            randomizer.Randomize("goodness", characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectFrom("goodnessclassNameMetaraces"), Times.Once);
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
            randomizer.GetAllPossibleResults(String.Empty, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsGetsNonEmptyResults()
        {
            var classNames = randomizer.GetAllPossibleResults(String.Empty, characterClass);

            Assert.That(classNames, Contains.Item(firstMetarace));
            Assert.That(classNames, Contains.Item(secondMetarace));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.GetAllPossibleResults("goodness", characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("goodnessclassNameMetaraces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.NotAllowedMetarace = firstMetarace;
            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);

            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.Not.Contains(firstMetarace));
        }

        [Test]
        public void IfForceMetaraceIsTrueThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.ForceMetarace = true;
            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(results, Is.Not.Contains(String.Empty));
        }

        [Test]
        public void IfForceMetaraceIsFalseThenEmptyMetaraceIsAllowed()
        {
            randomizer.ForceMetarace = false;
            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(results, Contains.Item(String.Empty));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesWithTooHighLevelAdjustments()
        {
            adjustments[firstMetarace] = 1;

            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.Not.Contains(firstMetarace));
        }

        private class TestMetaraceRandomizer : BaseMetarace
        {
            public String NotAllowedMetarace { get; set; }
            public Boolean ForceMetarace { get; set; }

            protected override Boolean forceMetarace
            {
                get { return ForceMetarace; }
            }

            public TestMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector)
                : base(percentileResultSelector, levelAdjustmentsSelector) { }

            protected override Boolean MetaraceIsAllowed(String metarace)
            {
                return metarace != NotAllowedMetarace;
            }
        }
    }
}