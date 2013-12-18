using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : IntegrationTest
    {
        [Inject]
        public NonSpellcasterClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void NonSpellcasterClassNameSingleRandomization()
        {
            ClassNameRandomizer.Randomize(Alignment);
        }

        [Test]
        public void NonSpellcasterClassNameRandomizerReturnsClassName()
        {
            while (TestShouldKeepRunning())
            {
                var className = ClassNameRandomizer.Randomize(Alignment);
                Assert.That(className, Is.Not.Null);
                Assert.That(className, Is.Not.Empty);
            }
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
                var className = ClassNameRandomizer.Randomize(Alignment);
                Assert.That(nonSpellcasters.Contains(className), Is.True);
            }
        }
    }
}