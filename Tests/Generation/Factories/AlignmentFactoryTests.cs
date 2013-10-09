using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class AlignmentFactoryTests
    {
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            alignment = new Alignment();
            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(alignment);
        }

        [Test]
        public void FactoryReturnsRandomizedAlignment()
        {
            var generatedAlignment = AlignmentFactory.CreateUsing(mockAlignmentRandomizer.Object);
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
        }
    }
}