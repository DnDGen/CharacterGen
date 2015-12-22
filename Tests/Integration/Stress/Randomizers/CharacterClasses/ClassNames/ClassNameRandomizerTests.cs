using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public abstract class ClassNameRandomizerTests : StressTests
    {
        protected abstract IEnumerable<string> allowedClassNames { get; }

        private IEnumerable<string> classNames;

        [SetUp]
        public void Setup()
        {
            classNames = allowedClassNames;
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var className = ClassNameRandomizer.Randomize(alignment);
            Assert.That(classNames, Contains.Item(className));
        }
    }
}