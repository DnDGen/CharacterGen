using CharacterGen.Generators.Domain;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class IterativeBuilderTests : IterativeBuilder
    {
        [Test]
        public void BuildWithLambda()
        {
            var builtString = "built string";

            var randomString = Build<String>(
                () => builtString,
                s => s.Contains("string"));

            Assert.That(randomString, Is.EqualTo(builtString));
        }

        [Test]
        public void BuildWithMethods()
        {
            var date = Build<DateTime>(Build, IsValid);
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
            var randomObject = Build<String>(
                () => null,
                s => true);

            Assert.That(randomObject, Is.Null);
        }

        [Test]
        public void RebuildIfInvalid()
        {
            var count = 0;

            var randomNumber = Build<Int32>(
                () => count++,
                i => i > 0 && i % 2 == 0);

            Assert.That(randomNumber, Is.EqualTo(2));
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void ThrowExceptionAfterTooManyRetries()
        {
            var count = 0;

            Assert.That(() => Build<Int32>(
                () => count++,
                i => false), Throws.Exception.With.Message.EqualTo("Exceeded max retries to build"));

            Assert.That(count, Is.EqualTo(1000001));
        }
    }
}
