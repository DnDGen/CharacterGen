using CharacterGen.Common.Alignments;
using CharacterGen.Generators;
using CharacterGen.Generators.Domain;
using CharacterGen.Generators.Randomizers.Alignments;
using Moq;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators
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