using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class BaseAlignmentTests
    {
        private TestAlignmentRandomizer randomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.d3(1)).Returns(1);

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(AlignmentConstants.GetGoodnesses());
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns(AlignmentConstants.Good);

            randomizer = new TestAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
        }

        [Test]
        public void LawfulIs3OnD3()
        {
            mockDice.Setup(d => d.d3(1)).Returns(3);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful));
        }

        [Test]
        public void NeutralLawfulnessIs2OnD3()
        {
            mockDice.Setup(d => d.d3(1)).Returns(2);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void ChaoticIs1OnD3()
        {
            mockDice.Setup(d => d.d3(1)).Returns(1);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Chaotic));
        }

        [Test]
        public void GetsGoodnessFromProvider()
        {
            randomizer.Randomize();
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RandomizerAccessesTableAlignmentGoodness()
        {
            randomizer.Randomize();
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("AlignmentGoodness"), Times.Once);
        }

        [Test]
        public void ReturnsGoodnessFromProvider()
        {
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns(AlignmentConstants.Good);

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good));
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize();
            mockPercentileResultProvider.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            randomizer.Randomize();
        }

        [Test]
        public void RandomizeLoopsUntilAlignmentWithAllowedGoodnessIsRolled()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns("invalid class name")
                .Returns(AlignmentConstants.Good);

            randomizer.Randomize();
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsGoodnessesFromProvider()
        {
            var classNames = randomizer.GetAllPossibleResults();
            mockPercentileResultProvider.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodness()
        {
            randomizer.Randomize();
            mockPercentileResultProvider.Verify(p => p.GetAllResults("AlignmentGoodness"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedCharacterClasses()
        {
            randomizer.NotAllowedLawfulness = AlignmentConstants.Lawful;
            var results = randomizer.GetAllPossibleResults();

            Assert.That(results.Any(a => a.Lawfulness == AlignmentConstants.Lawful), Is.False);
        }

        private class TestAlignmentRandomizer : BaseAlignmentRandomizer
        {
            public String NotAllowedLawfulness { get; set; }

            public TestAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

            protected override Boolean AlignmentIsAllowed(Alignment alignment)
            {
                return alignment.Lawfulness != NotAllowedLawfulness;
            }
        }
    }
}