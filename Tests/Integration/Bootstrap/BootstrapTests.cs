using System;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap
{
    [TestFixture]
    public abstract class BootstrapTests : IntegrationTests
    {
        protected void AssertSingleton<T>()
        {
            var first = GetNewInstanceOf<T>();
            var second = GetNewInstanceOf<T>();
            Assert.That(first, Is.EqualTo(second));
        }

        protected void AssertNotSingleton<T>()
        {
            var first = GetNewInstanceOf<T>();
            var second = GetNewInstanceOf<T>();
            Assert.That(first, Is.Not.EqualTo(second));
        }

        protected void AssertNotSingleton<T>(String name)
        {
            var first = GetNewInstanceOf<T>(name);
            var second = GetNewInstanceOf<T>(name);
            Assert.That(first, Is.Not.EqualTo(second));
        }

        protected void AssertNamedIsInstanceOf<I, T>(String name)
        {
            var item = GetNewInstanceOf<I>(name);
            Assert.That(item, Is.InstanceOf<T>());
        }

        protected void AssertIsInstanceOf<I, T>()
        {
            var item = GetNewInstanceOf<I>();
            Assert.That(item, Is.InstanceOf<T>());
        }
    }
}