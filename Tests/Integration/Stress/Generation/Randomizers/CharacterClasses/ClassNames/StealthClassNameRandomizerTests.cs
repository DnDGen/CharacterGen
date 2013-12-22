using System.Linq;
using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class StealthClassNameRandomizerTests : StressTest
    {
        [Inject]
        public StealthClassNameRandomizer ClassNameRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void StealthClassNameRandomizerReturnsClassName()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var className = ClassNameRandomizer.Randomize(data.Alignment);
                Assert.That(className, Is.Not.Null);
                Assert.That(className, Is.Not.Empty);
            }

            AssertIterations();
        }

        [Test]
        public void StealthClassNameRandomizerAlwaysReturnsStealth()
        {
            var stealthClasses = new[]
                {
                    CharacterClassConstants.Bard,
                    CharacterClassConstants.Ranger,
                    CharacterClassConstants.Rogue
                };

            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var className = ClassNameRandomizer.Randomize(data.Alignment);
                Assert.That(stealthClasses.Contains(className), Is.True);
            }

            AssertIterations();
        }
    }
}