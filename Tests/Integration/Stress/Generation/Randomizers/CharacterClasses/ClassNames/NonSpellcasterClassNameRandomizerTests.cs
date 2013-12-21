using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : StressTest
    {
        [Inject]
        public NonSpellcasterClassNameRandomizer ClassNameRandomizer { get; set; }

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
        public void NonSpellcasterClassNameRandomizerReturnsClassName()
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
        public void NonSpellcasterClassNameRandomizerAlwaysReturnsNonSpellcaster()
        {
            var nonSpellcasters = new[]
                {
                    CharacterClassConstants.Fighter,
                    CharacterClassConstants.Rogue,
                    CharacterClassConstants.Monk,
                    CharacterClassConstants.Barbarian
                };

            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var className = ClassNameRandomizer.Randomize(alignment);
                Assert.That(nonSpellcasters.Contains(className), Is.True);
            }

            AssertIterations();
        }
    }
}