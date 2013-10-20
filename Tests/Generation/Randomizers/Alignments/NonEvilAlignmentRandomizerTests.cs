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
    public class NonEvilAlignmentRandomizerTests
    {
        private IAlignmentRandomizer randomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new NonEvilAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
        }

        [Test]
        public void ForcedNotEvil()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Evil.ToString()).Returns(AlignmentConstants.Neutral.ToString());

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
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
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == AlignmentConstants.Lawful), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Neutral), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Lawful), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == AlignmentConstants.Chaotic), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Chaotic), Is.True);
            Assert.That(alignments.Count(), Is.EqualTo(6));
        }
    }
}