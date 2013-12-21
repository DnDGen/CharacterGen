using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
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
                var alignment = GetNewInstanceOf<Alignment>();
                var className = ClassNameRandomizer.Randomize(alignment);
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
                var alignment = GetNewInstanceOf<Alignment>();
                var className = ClassNameRandomizer.Randomize(alignment);
                Assert.That(stealthClasses.Contains(className), Is.True);
            }

            AssertIterations();
        }
    }
}