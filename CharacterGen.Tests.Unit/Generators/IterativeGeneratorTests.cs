using CharacterGen.Domain.Generators;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class IterativeGeneratorTests
    {
        private const int Limit = 30000;

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
            var randomString = generator.Generate(() => builtString, s => s.Contains("string"));
            Assert.That(randomString, Is.EqualTo(builtString));
        }

        [Test]
        public void BuildWithMethods()
        {
            var date = generator.Generate(Build, IsValid);
            Assert.That(iterations, Is.EqualTo(1));
            Assert.That(date.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
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

        [Test]
        public void BuildNull()
        {
            var randomObject = generator.Generate<string>(() => null, s => true);
            Assert.That(randomObject, Is.Null);
        }

        [Test]
        public void RebuildIfInvalid()
        {
            var randomNumber = generator.Generate(() => iterations++, i => i > 0 && i % 2 == 0);
            Assert.That(randomNumber, Is.EqualTo(2));
            Assert.That(iterations, Is.EqualTo(3));
        }

        [Test]
        public void ThrowExceptionAfterTooManyRetries()
        {
            Assert.That(() => generator.Generate(() => iterations++, i => false), Throws.Exception.With.Message.EqualTo($"Failed to generate Int32 after {Limit + 1} iterations"));
            Assert.That(iterations, Is.EqualTo(Limit));
        }

        [Test]
        public void ReturnValidObjectAfterTooManyRetries()
        {
            var randomString = generator.Generate(() => iterations++.ToString(), i => Convert.ToInt32(i) == Limit - 1);
            Assert.That(iterations, Is.EqualTo(Limit));
            Assert.That(randomString, Is.EqualTo($"{Limit - 1}"));
        }
    }
}
