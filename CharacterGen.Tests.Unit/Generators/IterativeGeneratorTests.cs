using CharacterGen.Domain.Generators;
using EventGen;
using Moq;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class IterativeGeneratorTests
    {
        private const int Limit = 1000;

        private Generator generator;
        private int iterations;
        private Mock<GenEventQueue> mockEventQueue;

        [SetUp]
        public void Setup()
        {
            mockEventQueue = new Mock<GenEventQueue>();
            generator = new IterativeGenerator(mockEventQueue.Object);
            iterations = 0;
        }

        [Test]
        public void BuildWithLambda()
        {
            var builtString = "built string";
            var randomString = generator.Generate(() => builtString, "unit test", s => s.Contains("string"), () => string.Empty, string.Empty);
            Assert.That(randomString, Is.EqualTo(builtString));
        }

        [Test]
        public void BuildWithMethods()
        {
            var date = generator.Generate(Build, "unit test", IsValid, BuildDefault, string.Empty);
            Assert.That(iterations, Is.EqualTo(1));
            Assert.That(date, Is.EqualTo(DateTime.Now).Within(1).Seconds);
        }

        private DateTime Build()
        {
            iterations++;
            return DateTime.Now;
        }

        private bool IsValid(DateTime date)
        {
            return date.Year == DateTime.Now.Year;
        }

        private DateTime BuildDefault()
        {
            return new DateTime(1989, 10, 29);
        }

        [Test]
        public void BuildNull()
        {
            var randomObject = generator.Generate(() => null, "unit test", s => true, () => new object(), string.Empty);
            Assert.That(randomObject, Is.Null);
        }

        [Test]
        public void RebuildIfInvalid()
        {
            var randomNumber = generator.Generate(() => iterations++, "unit test", i => i > 0 && i % 2 == 0, () => -1, string.Empty);
            Assert.That(randomNumber, Is.EqualTo(2));
            Assert.That(iterations, Is.EqualTo(3));
        }

        [Test]
        public void LogEventsOfIterativeGeneration()
        {
            var randomNumber = generator.Generate(() => iterations++, "unit test", i => i > 0 && i % 2 == 0, () => -1, string.Empty);
            Assert.That(randomNumber, Is.EqualTo(2));
            Assert.That(iterations, Is.EqualTo(3));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generating unit test by iteration"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generated unit test after 3 iterations"), Times.Once);
        }

        [Test]
        public void ReturnDefault()
        {
            var number = generator.Generate(() => iterations++, "unit test", i => false, () => -1, "a thing and stuff");
            Assert.That(number, Is.EqualTo(-1));
            Assert.That(iterations, Is.EqualTo(Limit));
        }

        [Test]
        public void LogEventWhenReturningDefault()
        {
            var number = generator.Generate(() => iterations++, "unit test", i => false, () => -1, "a thing and stuff");
            Assert.That(number, Is.EqualTo(-1));
            Assert.That(iterations, Is.EqualTo(Limit));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(5));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generating unit test by iteration"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Retried 500 times to generate unit test"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Retried 1000 times to generate unit test"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generating a thing and stuff by default"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generated unit test after 1000 iterations"), Times.Once);
        }

        [Test]
        public void UseNormalDescriptionIfNoDefaultDescriptionProvided()
        {
            var number = generator.Generate(() => iterations++, "unit test", i => false, () => -1);
            Assert.That(number, Is.EqualTo(-1));
            Assert.That(iterations, Is.EqualTo(Limit));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(5));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generating unit test by iteration"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Retried 500 times to generate unit test"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Retried 1000 times to generate unit test"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generating unit test by default"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generated unit test after 1000 iterations"), Times.Once);
        }

        [TestCase("")]
        [TestCase(null)]
        public void UseNormalDescriptionIfEmptyDefaultDescriptionProvided(string defaultDescription)
        {
            var number = generator.Generate(() => iterations++, "unit test", i => false, () => -1, defaultDescription);
            Assert.That(number, Is.EqualTo(-1));
            Assert.That(iterations, Is.EqualTo(Limit));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(5));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generating unit test by iteration"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Retried 500 times to generate unit test"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Retried 1000 times to generate unit test"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generating unit test by default"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generated unit test after 1000 iterations"), Times.Once);
        }

        [Test]
        public void ReturnValidObjectAfterTooManyRetries()
        {
            var randomString = generator.Generate(() => iterations++.ToString(), "unit test", i => Convert.ToInt32(i) == Limit - 1, () => "nope", string.Empty);
            Assert.That(iterations, Is.EqualTo(Limit));
            Assert.That(randomString, Is.EqualTo($"{Limit - 1}"));
        }
    }
}
