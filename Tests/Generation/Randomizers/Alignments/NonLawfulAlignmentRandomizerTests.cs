using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonLawfulAlignmentRandomizerTests
    {
        private IAlignmentRandomizer randomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new NonLawfulAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
        }

        [Test]
        public void ForcedNotLawful()
        {
            mockDice.SetupSequence(d => d.d3(1, 0)).Returns(3).Returns(2);

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
            mockDice.Verify(d => d.d3(1, 0), Times.Exactly(2));
        }

        [Test]
        public void ReturnFilteredAlignments()
        {
            var goodnesses = new[] 
            { 
                AlignmentConstants.Good.ToString(),
                AlignmentConstants.Neutral.ToString(), 
                AlignmentConstants.Evil.ToString() 
            };

            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(goodnesses);

            var alignments = randomizer.GetAllPossibleResults();

            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == AlignmentConstants.Neutral), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == AlignmentConstants.Chaotic), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Neutral), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Chaotic), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == AlignmentConstants.Neutral), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == AlignmentConstants.Chaotic), Is.True);
            Assert.That(alignments.Count(), Is.EqualTo(6));
        }
    }
}