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
    public class LawfulAlignmentRandomizerTests
    {
        private IAlignmentRandomizer randomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new LawfulAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
        }

        [Test]
        public void ForcedLawful()
        {
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful));
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

            Assert.That(alignments.All(a => a.Lawfulness == AlignmentConstants.Lawful), Is.True);
            Assert.That(alignments.Count(), Is.EqualTo(3));
            Assert.That(alignments.Count(a => a.Goodness == AlignmentConstants.Good), Is.EqualTo(1));
            Assert.That(alignments.Count(a => a.Goodness == AlignmentConstants.Neutral), Is.EqualTo(1));
            Assert.That(alignments.Count(a => a.Goodness == AlignmentConstants.Evil), Is.EqualTo(1));
        }
    }
}