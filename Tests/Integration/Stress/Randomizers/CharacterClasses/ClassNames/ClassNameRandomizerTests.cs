using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public abstract class ClassNameRandomizerTests : StressTests
    {
        protected abstract IEnumerable<String> particularClassNames { get; }

        private IEnumerable<String> classNames;

        [SetUp]
        public void Setup()
        {
            classNames = particularClassNames;
        }

        protected override void MakeAssertions()
        {
            var data = GetNewDependentData();
            var className = ClassNameRandomizer.Randomize(data.Alignment);
            Assert.That(classNames, Contains.Item(className));
        }
    }
}