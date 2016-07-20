using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public abstract class ClassNameRandomizerTests : StressTests
    {
        protected abstract IEnumerable<string> allowedClassNames { get; }

        protected void AssertClassName()
        {
            var alignment = GetNewAlignment();
            var className = ClassNameRandomizer.Randomize(alignment);
            Assert.That(allowedClassNames, Contains.Item(className));
        }
    }
}