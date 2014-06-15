using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class AlignmentGeneratorTests
    {
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private IAlignmentGenerator alignmentFactory;

        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(alignment);
            alignmentFactory = new AlignmentGenerator();
        }

        [Test]
        public void FactoryReturnsRandomizedAlignment()
        {
            var generatedAlignment = alignmentFactory.CreateWith(mockAlignmentRandomizer.Object);
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
        }
    }
}