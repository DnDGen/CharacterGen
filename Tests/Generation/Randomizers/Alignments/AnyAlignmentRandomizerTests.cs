using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class AnyAlignmentRandomizerTests
    {
        [Test]
        public void ReturnsUnalteredAlignmentFromBase()
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.d3(1, 0)).Returns(42);

            var mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("9266");

            var randomizer = new AnyAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
            var testRandomizer = new TestAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);

            var alignment = randomizer.Randomize();
            var testAlignment = testRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(testAlignment.Lawfulness));
            Assert.That(alignment.Goodness, Is.EqualTo(testAlignment.Goodness));
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
        }
    }
}