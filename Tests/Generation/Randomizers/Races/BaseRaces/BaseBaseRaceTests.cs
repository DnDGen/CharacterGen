using System;
using System.Linq;
using Moq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseBaseRaceTests
    {
        private TestBaseRaceRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        private String firstBaseRace = "first base race";
        private String secondBaseRace = "second base race";

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(new[] { firstBaseRace, secondBaseRace, String.Empty });
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns(firstBaseRace);

            randomizer = new TestBaseRaceRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(String.Empty, String.Empty);
            mockPercentileResultProvider.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            randomizer.Randomize(String.Empty, String.Empty);
        }

        [Test]
        public void RandomizeReturnsBaseRaceFromPercentileResultProvider()
        {
            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo(firstBaseRace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            randomizer.Randomize("goodness", "className");
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("goodnessclassNameBaseRaces"), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedBaseRaceIsRolled()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns("invalid base race")
                .Returns(firstBaseRace);

            randomizer.Randomize(String.Empty, String.Empty);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromProvider()
        {
            randomizer.GetAllPossibleResults(String.Empty, String.Empty);
            mockPercentileResultProvider.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutEmptyStrings()
        {
            var classNames = randomizer.GetAllPossibleResults(String.Empty, String.Empty);

            Assert.That(classNames.Contains(firstBaseRace), Is.True);
            Assert.That(classNames.Contains(secondBaseRace), Is.True);
            Assert.That(classNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            randomizer.GetAllPossibleResults("goodness", "className");
            mockPercentileResultProvider.Verify(p => p.GetAllResults("goodnessclassNameBaseRaces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.NotAllowedBaseRace = firstBaseRace;
            var results = randomizer.GetAllPossibleResults(String.Empty, String.Empty);

            Assert.That(results.Contains(secondBaseRace), Is.True);
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private class TestBaseRaceRandomizer : BaseBaseRace
        {
            public String NotAllowedBaseRace { get; set; }

            public TestBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider)
                : base(percentileResultProvider) { }

            protected override Boolean BaseRaceIsAllowed(String baseRace)
            {
                return baseRace != NotAllowedBaseRace;
            }
        }
    }
}