using CharacterGen.Generators;
using CharacterGen.Generators.Domain;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class IterativeGeneratorTests
    {
        private Generator generator;

        [SetUp]
        public void Setup()
        {
            generator = new IterativeGenerator();
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
            Assert.That(date.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
        }

        private DateTime Build()
        {
            return DateTime.Now;
        }

        private Boolean IsValid(DateTime date)
        {
            return date.Year == DateTime.Now.Year;
        }

        [Test]
        public void BuildNull()
        {
            var randomObject = generator.Generate<String>(() => null, s => true);
            Assert.That(randomObject, Is.Null);
        }

        [Test]
        public void RebuildIfInvalid()
        {
            var count = 0;

            var randomNumber = generator.Generate(() => count++, i => i > 0 && i % 2 == 0);
            Assert.That(randomNumber, Is.EqualTo(2));
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void ReturnDefaultAfterTooManyRetries()
        {
            var count = 0;

            var randomNumber = generator.Generate(() => count++, i => false);

            Assert.That(count, Is.EqualTo(10000));
            Assert.That(randomNumber, Is.EqualTo(0));
        }

        [Test]
        public void ReturnNullAfterTooManyRetries()
        {
            var count = 0;

            var randomString = generator.Generate(() => count++.ToString(), i => false);
            Assert.That(count, Is.EqualTo(10000));
            Assert.That(randomString, Is.Null);
        }

        [Test]
        public void ReturnValidObjectAfterTooManyRetries()
        {
            var count = 0;

            var randomString = generator.Generate(() => count++.ToString(), i => Convert.ToInt32(i) == 9999);
            Assert.That(count, Is.EqualTo(10000));
            Assert.That(randomString, Is.EqualTo("9999"));
        }
    }
}
