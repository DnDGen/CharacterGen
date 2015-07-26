using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Domain.Randomizers.Alignments;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using RollGen;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Alignments
{
    [TestFixture]
    public class BaseAlignmentTests
    {
        private TestAlignmentRandomizer randomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileSelector> mockPercentileResultSelector;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.Roll(1).d3()).Returns(1);

            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(AlignmentConstants.GetGoodnesses());
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<String>())).Returns(AlignmentConstants.Good);

            randomizer = new TestAlignmentRandomizer(mockDice.Object, mockPercentileResultSelector.Object);
        }

        [Test]
        public void LawfulIs3OnD3()
        {
            mockDice.Setup(d => d.Roll(1).d3()).Returns(3);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful));
        }

        [Test]
        public void NeutralLawfulnessIs2OnD3()
        {
            mockDice.Setup(d => d.Roll(1).d3()).Returns(2);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void ChaoticIs1OnD3()
        {
            mockDice.Setup(d => d.Roll(1).d3()).Returns(1);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Chaotic));
        }

        [Test]
        public void ReturnsGoodnessFromSelector()
        {
            mockPercentileResultSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns(AlignmentConstants.Evil);

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Evil));
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize();
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            Assert.That(() => randomizer.Randomize(), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeLoopsUntilAlignmentWithAllowedGoodnessIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<String>())).Returns("invalid class name")
                .Returns(AlignmentConstants.Good);

            randomizer.Randomize();
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodness()
        {
            randomizer.Randomize();
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("AlignmentGoodness"), Times.Once);
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

            public TestAlignmentRandomizer(IDice dice, IPercentileSelector Selector) : base(dice, Selector) { }

            protected override Boolean AlignmentIsAllowed(Alignment alignment)
            {
                return alignment.Lawfulness != NotAllowedLawfulness;
            }
        }
    }
}