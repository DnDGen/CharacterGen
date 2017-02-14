using CharacterGen.Domain.Generators;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class IterativeGeneratorTests
    {
        private const int Limit = 10000;

        private Generator generator;
        private int iterations;

        [SetUp]
        public void Setup()
        {
            generator = new IterativeGenerator();
            iterations = 0;
        }

        [Test]
        public void BuildWithLambda()
        {
            var builtString = "built string";
            var randomString = generator.Generate(() => builtString, s => s.Contains("string"), () => string.Empty);
            Assert.That(randomString, Is.EqualTo(builtString));
        }

        [Test]
        public void BuildWithMethods()
        {
            var date = generator.Generate(Build, IsValid, BuildDefault);
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
            var randomObject = generator.Generate(() => null, s => true, () => new object());
            Assert.That(randomObject, Is.Null);
        }

        [Test]
        public void RebuildIfInvalid()
        {
            var randomNumber = generator.Generate(() => iterations++, i => i > 0 && i % 2 == 0, () => -1);
            Assert.That(randomNumber, Is.EqualTo(2));
            Assert.That(iterations, Is.EqualTo(3));
        }

        [Test]
        public void ReturnDefault()
        {
            var number = generator.Generate(() => iterations++, i => false, () => -1);
            Assert.That(number, Is.EqualTo(-1));
            Assert.That(iterations, Is.EqualTo(Limit));
        }

        [Test]
        public void ReturnValidObjectAfterTooManyRetries()
        {
            var randomString = generator.Generate(() => iterations++.ToString(), i => Convert.ToInt32(i) == Limit - 1, () => "nope");
            Assert.That(iterations, Is.EqualTo(Limit));
            Assert.That(randomString, Is.EqualTo($"{Limit - 1}"));
        }
    }
}
