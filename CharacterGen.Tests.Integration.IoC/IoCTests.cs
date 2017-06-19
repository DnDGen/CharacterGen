using Ninject;
using NUnit.Framework;
using System.Diagnostics;

namespace CharacterGen.Tests.Integration.IoC
{
    [TestFixture]
    [IoC]
    public abstract class IoCTests : IntegrationTests
    {
        [Inject]
        public Stopwatch Stopwatch { get; set; }

        [TearDown]
        public void IoCTeardown()
        {
            Stopwatch.Reset();
        }

        protected void AssertSingleton<T>()
        {
            var first = InjectAndAssertDuration<T>();
            var second = InjectAndAssertDuration<T>();
            Assert.That(first, Is.EqualTo(second));
        }

        private T InjectAndAssertDuration<T>()
        {
            Stopwatch.Start();

            var instance = GetNewInstanceOf<T>();
            Assert.That(Stopwatch.Elapsed.TotalMilliseconds, Is.LessThan(100));
            Stopwatch.Reset();

            return instance;
        }

        private T InjectAndAssertDuration<T>(string name)
        {
            Stopwatch.Start();

            var instance = GetNewInstanceOf<T>(name);
            Assert.That(Stopwatch.Elapsed.TotalMilliseconds, Is.LessThan(100));
            Stopwatch.Reset();

            return instance;
        }

        protected void AssertNotSingleton<T>()
        {
            var first = InjectAndAssertDuration<T>();
            var second = InjectAndAssertDuration<T>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        protected void AssertNotSingleton<T>(string name)
        {
            var first = InjectAndAssertDuration<T>(name);
            var second = InjectAndAssertDuration<T>(name);
            Assert.That(first, Is.Not.EqualTo(second));
        }

        protected void AssertNamedIsInstanceOf<I, T>(string name)
            where T : I
        {
            var item = InjectAndAssertDuration<I>(name);
            Assert.That(item, Is.InstanceOf<T>());
        }

        protected void AssertIsInstanceOf<I, T>()
            where T : I
        {
            var item = InjectAndAssertDuration<I>();
            Assert.That(item, Is.InstanceOf<T>());
        }
    }
}