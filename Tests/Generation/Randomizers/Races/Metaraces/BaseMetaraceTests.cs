using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class BaseMetaraceTests
    {
        private TestMetaraceRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Mock<ILevelAdjustmentsProvider> mockLevelAdjustmentsProvider;

        private String firstMetarace = "first metarace";
        private String secondMetarace = "second metarace";
        private CharacterClass characterClass;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var metaraces = new[] { firstMetarace, secondMetarace, String.Empty };
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(metaraces);
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns(firstMetarace);

            adjustments = new Dictionary<String, Int32>();
            foreach (var metarace in metaraces)
                adjustments.Add(metarace, 0);

            mockLevelAdjustmentsProvider = new Mock<ILevelAdjustmentsProvider>();
            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(adjustments);

            characterClass = new CharacterClass();
            characterClass.Level = 1;

            randomizer = new TestMetaraceRandomizer(mockPercentileResultProvider.Object, mockLevelAdjustmentsProvider.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(String.Empty, characterClass);
            mockPercentileResultProvider.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            randomizer.Randomize(String.Empty, characterClass);
        }

        [Test]
        public void RandomizeReturnsBaseRaceFromPercentileResultProvider()
        {
            var result = randomizer.Randomize(String.Empty, characterClass);
            Assert.That(result, Is.EqualTo(firstMetarace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.Randomize("goodness", characterClass);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("goodnessclassNameMetaraces"), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedBaseRaceIsRolled()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns("invalid metarace")
                .Returns(firstMetarace);

            randomizer.Randomize(String.Empty, characterClass);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromProvider()
        {
            randomizer.GetAllPossibleResults(String.Empty, characterClass);
            mockPercentileResultProvider.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsGetsNonEmptyResults()
        {
            var classNames = randomizer.GetAllPossibleResults(String.Empty, characterClass);

            Assert.That(classNames.Contains(firstMetarace), Is.True);
            Assert.That(classNames.Contains(secondMetarace), Is.True);
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.ClassName = "className";
            randomizer.GetAllPossibleResults("goodness", characterClass);
            mockPercentileResultProvider.Verify(p => p.GetAllResults("goodnessclassNameMetaraces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.NotAllowedMetarace = firstMetarace;
            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);

            Assert.That(results.Contains(secondMetarace), Is.True);
        }

        [Test]
        public void IfAllowNoMetaraceIsTrueThenEmptyMetaraceIsAllowed()
        {
            randomizer.AllowNoMetarace = true;

            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(results.Contains(String.Empty), Is.True);
        }

        [Test]
        public void IfAllowNoMetaraceIsFalseThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.AllowNoMetarace = false;

            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(results.Contains(String.Empty), Is.False);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesWithTooHighLevelAdjustments()
        {
            adjustments[firstMetarace] = 1;

            var results = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(results.Contains(secondMetarace), Is.True);
            Assert.That(results.Contains(firstMetarace), Is.False);
        }

        private class TestMetaraceRandomizer : BaseMetarace
        {
            public String NotAllowedMetarace { get; set; }

            public TestMetaraceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentsProvider)
                : base(percentileResultProvider, levelAdjustmentsProvider) { }

            protected override Boolean MetaraceIsAllowed(String metarace)
            {
                return metarace != NotAllowedMetarace;
            }
        }
    }
}