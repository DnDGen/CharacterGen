using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();

            randomizer = new TestAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
        }

        [Test]
        public void LawfulIs3OnD3()
        {
            mockDice.Setup(d => d.d3(1, 0)).Returns(3);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful));
        }

        [Test]
        public void NeutralLawfulnessIs2OnD3()
        {
            mockDice.Setup(d => d.d3(1, 0)).Returns(2);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void ChaoticIs1OnD3()
        {
            mockDice.Setup(d => d.d3(1, 0)).Returns(1);
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Chaotic));
        }

        [Test]
        public void GetsGoodnessFromProvider()
        {
            var alignment = randomizer.Randomize();
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Once());
        }

        [Test]
        public void AccessesTableAlignmentGoodness()
        {
            var alignment = randomizer.Randomize();
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("AlignmentGoodness"), Times.Once());
        }

        [Test]
        public void ReturnsGoodnessFromProvider()
        {
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("9266");

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(9266));
        }

        [Test]
        public void GetAllPossibleResultsGrabsFromProvider()
        {
            randomizer.GetAllPossibleResults();
            mockPercentileResultProvider.Verify(p => p.GetAllResults("AlignmentGoodness"), Times.Once);
        }

        private class TestAlignmentRandomizer : BaseAlignmentRandomizer
        {
            public TestAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

            public override Alignment Randomize()
            {
                var alignment = new Alignment();

                alignment.Goodness = RollGoodness();
                alignment.Lawfulness = RollLawfulness();

                return alignment;
            }

            public override IEnumerable<Alignment> GetAllPossibleResults()
            {
                var goodnesses = GetAllGoodnesses();
                var alignments = new List<Alignment>();

                foreach (var goodness in goodnesses)
                    alignments.Add(new Alignment() { Goodness = goodness });

                return alignments;
            }
        }
    }
}