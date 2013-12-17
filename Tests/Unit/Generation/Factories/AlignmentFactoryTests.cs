using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Factories
{
    [TestFixture]
    public class AlignmentFactoryTests
    {
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private IAlignmentFactory alignmentFactory;

        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(alignment);
            alignmentFactory = new AlignmentFactory();
        }

        [Test]
        public void FactoryReturnsRandomizedAlignment()
        {
            var generatedAlignment = alignmentFactory.CreateWith(mockAlignmentRandomizer.Object);
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
        }
    }
}