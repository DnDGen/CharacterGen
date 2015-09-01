using CharacterGen.Generators.Domain;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class GeneratorTests : Generator
    {
        [Test]
        public void GenerateWithLambda()
        {
            var generatedString = "generated string";

            var randomString = Generate<String>(
                () => generatedString,
                s => s.Contains("string"));

            Assert.That(randomString, Is.EqualTo(generatedString));
        }

        [Test]
        public void GenerateWithMethods()
        {
            var date = Generate<DateTime>(Generate, IsValid);
            Assert.That(date.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
        }

        private DateTime Generate()
        {
            return DateTime.Now;
        }

        private Boolean IsValid(DateTime date)
        {
            return date.Year == DateTime.Now.Year;
        }

        [Test]
        public void GenerateNull()
        {
            var randomObject = Generate<String>(
                () => null,
                s => true);

            Assert.That(randomObject, Is.Null);
        }

        [Test]
        public void RegenerateIfInvalid()
        {
            var count = 0;

            var randomNumber = Generate<Int32>(
                () => count++,
                i => i > 0 && i % 2 == 0);

            Assert.That(randomNumber, Is.EqualTo(2));
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void ThrowExceptionAfterTooManyRetries()
        {
            var count = 0;

            Assert.That(() => Generate<Int32>(
                () => count++,
                i => false), Throws.Exception.With.Message.EqualTo("Exceeded max retries to generate"));

            Assert.That(count, Is.EqualTo(10001));
        }
    }
}
