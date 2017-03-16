using CharacterGen.Alignments;
using CharacterGen.Domain.Generators.Alignments;
using CharacterGen.Randomizers.Alignments;
using Moq;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Alignments
{
    [TestFixture]
    public class AlignmentGeneratorTests
    {
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private IAlignmentGenerator alignmentGenerator;

        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            alignmentGenerator = new AlignmentGenerator();

            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(alignment);
        }

        [Test]
        public void GeneratorReturnsRandomizedAlignment()
        {
            var generatedAlignment = alignmentGenerator.GenerateWith(mockAlignmentRandomizer.Object);
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
        }
    }
}