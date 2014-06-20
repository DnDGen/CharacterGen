using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
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
        private Mock<ILevelAdjustmentsSelector> mockLevelAdjustmentsSelector;

        private String firstMetarace = "first metarace";
        private String secondMetarace = "second metarace";
        private CharacterClassPrototype prototype;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var metaraces = new[] { firstMetarace, secondMetarace, String.Empty };
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(metaraces);
            mockPercentileResultSelector.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns(firstMetarace);

            adjustments = new Dictionary<String, Int32>();
            foreach (var metarace in metaraces)
                adjustments.Add(metarace, 0);

            mockLevelAdjustmentsSelector = new Mock<ILevelAdjustmentsSelector>();
            mockLevelAdjustmentsSelector.Setup(p => p.GetLevelAdjustments()).Returns(adjustments);

            prototype = new CharacterClassPrototype();
            prototype.Level = 1;

            randomizer = new TestMetaraceRandomizer(mockPercentileResultSelector.Object, mockLevelAdjustmentsSelector.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromPercentileResultSelector()
        {
            randomizer.Randomize(String.Empty, prototype);
            mockPercentileResultSelector.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            randomizer.Randomize(String.Empty, prototype);
        }

        [Test]
        public void RandomizeReturnsMetaraceFromPercentileResultSelector()
        {
            var result = randomizer.Randomize(String.Empty, prototype);
            Assert.That(result, Is.EqualTo(firstMetarace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameMetaraces()
        {
            prototype.ClassName = "className";
            randomizer.Randomize("goodness", prototype);
            mockPercentileResultSelector.Verify(p => p.GetPercentileResult("goodnessclassNameMetaraces"), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedMetaraceIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns("invalid metarace")
                .Returns(firstMetarace);

            randomizer.Randomize(String.Empty, prototype);
            mockPercentileResultSelector.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            randomizer.GetAllPossibleResults(String.Empty, prototype);
            mockPercentileResultSelector.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsGetsNonEmptyResults()
        {
            var classNames = randomizer.GetAllPossibleResults(String.Empty, prototype);

            Assert.That(classNames.Contains(firstMetarace), Is.True);
            Assert.That(classNames.Contains(secondMetarace), Is.True);
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            prototype.ClassName = "className";
            randomizer.GetAllPossibleResults("goodness", prototype);
            mockPercentileResultSelector.Verify(p => p.GetAllResults("goodnessclassNameMetaraces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.NotAllowedMetarace = firstMetarace;
            var results = randomizer.GetAllPossibleResults(String.Empty, prototype);

            Assert.That(results.Contains(secondMetarace), Is.True);
        }

        [Test]
        public void IfAllowNoMetaraceIsTrueThenEmptyMetaraceIsAllowed()
        {
            randomizer.AllowNoMetarace = true;

            var results = randomizer.GetAllPossibleResults(String.Empty, prototype);
            Assert.That(results.Contains(String.Empty), Is.True);
        }

        [Test]
        public void IfAllowNoMetaraceIsFalseThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.AllowNoMetarace = false;

            var results = randomizer.GetAllPossibleResults(String.Empty, prototype);
            Assert.That(results.Contains(String.Empty), Is.False);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesWithTooHighLevelAdjustments()
        {
            adjustments[firstMetarace] = 1;

            var results = randomizer.GetAllPossibleResults(String.Empty, prototype);
            Assert.That(results.Contains(secondMetarace), Is.True);
            Assert.That(results.Contains(firstMetarace), Is.False);
        }

        private class TestMetaraceRandomizer : BaseMetarace
        {
            public String NotAllowedMetarace { get; set; }
            public Boolean AllowNoMetarace { get; set; }

            protected override Boolean allowNoMetarace
            {
                get { return AllowNoMetarace; }
            }

            public TestMetaraceRandomizer(IPercentileSelector percentileResultSelector, ILevelAdjustmentsSelector levelAdjustmentsSelector)
                : base(percentileResultSelector, levelAdjustmentsSelector) { }

            protected override Boolean MetaraceIsAllowed(String metarace)
            {
                return metarace != NotAllowedMetarace;
            }
        }
    }
}