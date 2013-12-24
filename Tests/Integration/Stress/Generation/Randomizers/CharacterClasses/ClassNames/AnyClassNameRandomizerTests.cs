using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyClassNameRandomizerTests : StressTest
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
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
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