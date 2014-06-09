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
    public class StealthClassNameRandomizerTests : StressTest
    {
        [Inject]
        public StealthClassNameRandomizer ClassNameRandomizer { get; set; }

        private IEnumerable<String> classNames;

        protected override IClassNameRandomizer GetClassNameRandomizer(IKernel kernel)
        {
            return kernel.Get<StealthClassNameRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            classNames = new[]
                {
                    CharacterClassConstants.Bard,
                    CharacterClassConstants.Ranger,
                    CharacterClassConstants.Rogue
                };
        }

        [Test]
        public void StealthClassNameRandomizerAlwaysReturnsStealth()
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