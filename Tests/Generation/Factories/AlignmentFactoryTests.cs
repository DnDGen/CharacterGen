using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class AlignmentFactoryTests
    {
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Alignment alignment;
        private IAlignmentFactory alignmentFactory;

        [SetUp]
        public void Setup()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            alignment = new Alignment();
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Good;
            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(alignment);

            alignmentFactory = new AlignmentFactory(mockAlignmentRandomizer.Object);
        }

        [Test]
        public void RandomizerProperlySet()
        {
            Assert.That(alignmentFactory.AlignmentRandomizer, Is.EqualTo(mockAlignmentRandomizer.Object));
        }

        [Test]
        public void FactoryReturnsRandomizedAlignment()
        {
            Assert.That(alignmentFactory.Generate(), Is.EqualTo(alignment));
        }

        [Test]
        public void ChangeAlignmentRandomizer()
        {
            var differentRandomizer = new Mock<IAlignmentRandomizer>();
            var differentAlignment = new Alignment();
            differentAlignment.Lawfulness = AlignmentConstants.Chaotic;
            differentAlignment.Goodness = AlignmentConstants.Evil;
            differentRandomizer.Setup(r => r.Randomize()).Returns(differentAlignment);

            alignmentFactory.AlignmentRandomizer = differentRandomizer.Object;

            Assert.That(alignmentFactory.Generate(), Is.EqualTo(differentAlignment));
        }
    }
}