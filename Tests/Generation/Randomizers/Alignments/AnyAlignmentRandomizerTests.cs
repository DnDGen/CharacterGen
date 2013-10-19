using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class AnyAlignmentRandomizerTests
    {
        private IAlignmentRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Mock<IDice> mockDice;
        private IAlignmentRandomizer testRandomizer;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();

            randomizer = new AnyAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
            testRandomizer = new TestAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
        }

        [Test]
        public void ReturnsUnalteredAlignmentFromBase()
        {
            mockDice.Setup(d => d.d3(1, 0)).Returns(42);
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("9266");

            var alignment = randomizer.Randomize();
            var testAlignment = testRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(testAlignment.Lawfulness));
            Assert.That(alignment.Goodness, Is.EqualTo(testAlignment.Goodness));
        }

        [Test]
        public void ReturnUnfilteredAlignments()
        {
            var alignments = randomizer.GetAllPossibleResults();
            var testAlignments = testRandomizer.GetAllPossibleResults();

            foreach (var testAlignment in testAlignments)
                Assert.That(alignments.Contains(testAlignment), Is.True);

            Assert.That(alignments.Count(), Is.EqualTo(testAlignments.Count()));
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
                {
                    alignments.Add(new Alignment() { Goodness = goodness, Lawfulness = AlignmentConstants.Chaotic });
                    alignments.Add(new Alignment() { Goodness = goodness, Lawfulness = AlignmentConstants.Lawful });
                    alignments.Add(new Alignment() { Goodness = goodness, Lawfulness = AlignmentConstants.Neutral });
                }

                return alignments;
            }
        }
    }
}