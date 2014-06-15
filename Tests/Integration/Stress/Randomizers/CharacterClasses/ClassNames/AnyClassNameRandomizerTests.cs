using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyClassNameRandomizerTests : StressTests
    {
        [Inject]
        public AnyClassNameRandomizer ClassNameRandomizer { get; set; }

        private IEnumerable<String> classNames;

        protected override IClassNameRandomizer GetClassNameRandomizer(IKernel kernel)
        {
            return kernel.Get<AnyClassNameRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            classNames = CharacterClassConstants.GetClassNames();
        }

        [Test]
        public void AnyClassNameRandomizerReturnsClassName()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var className = ClassNameRandomizer.Randomize(data.Alignment);
                Assert.That(classNames.Contains(className), Is.True);
            }

            AssertIterations();
        }
    }
}