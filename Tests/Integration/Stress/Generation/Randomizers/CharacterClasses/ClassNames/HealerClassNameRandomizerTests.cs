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
    public class HealerClassNameRandomizerTests : StressTest
    {
        [Inject]
        public HealerClassNameRandomizer ClassNameRandomizer { get; set; }

        private IEnumerable<String> classNames;

        protected override IClassNameRandomizer GetClassNameRandomizer(IKernel kernel)
        {
            return kernel.Get<HealerClassNameRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            classNames = new[]
                {
                    CharacterClassConstants.Bard,
                    CharacterClassConstants.Druid,
                    CharacterClassConstants.Paladin,
                    CharacterClassConstants.Ranger,
                    CharacterClassConstants.Cleric
                };

            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void HealerClassNameRandomizerAlwaysReturnsHealer()
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