using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public abstract class ClassNameRandomizerTests : StressTests
    {
        protected abstract IEnumerable<String> allowedClassNames { get; }

        private IEnumerable<String> classNames;

        [SetUp]
        public void Setup()
        {
            classNames = allowedClassNames;
        }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        protected void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var className = ClassNameRandomizer.Randomize(alignment);
            Assert.That(classNames, Contains.Item(className));
        }
    }
}