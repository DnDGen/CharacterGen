using CharacterGen.Alignments;
using CharacterGen.Domain.Generators.Alignments;
using CharacterGen.Randomizers.Alignments;
using EventGen;
using Moq;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Alignments
{
    [TestFixture]
    public class AlignmentGeneratorEventGenDecoratorTests
    {
        private IAlignmentGenerator decorator;
        private Mock<IAlignmentGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IAlignmentGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new AlignmentGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
        }

        [Test]
        public void ReturnInnerAlignment()
        {
            var alignment = new Alignment();
            mockInnerGenerator.Setup(g => g.GenerateWith(mockAlignmentRandomizer.Object)).Returns(alignment);

            var generatedAlignment = decorator.GenerateWith(mockAlignmentRandomizer.Object);
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
        }

        [Test]
        public void LogEventsForAlignmentGeneration()
        {
            var alignment = new Alignment();
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockInnerGenerator.Setup(g => g.GenerateWith(mockAlignmentRandomizer.Object)).Returns(alignment);

            var generatedAlignment = decorator.GenerateWith(mockAlignmentRandomizer.Object);
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating alignment"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated lawfulness goodness"), Times.Once);
        }
    }
}
