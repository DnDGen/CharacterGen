using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class WarriorClassNameRandomizerTests : IntegrationTest
    {
        [Inject]
        public WarriorClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void WarriorClassNameRandomizerReturnsClassName()
        {
            while (TestShouldKeepRunning())
            {
                var className = ClassNameRandomizer.Randomize(Alignment);
                Assert.That(className, Is.Not.Null);
                Assert.That(className, Is.Not.Empty);
            }
        }

        [Test]
        public void WarriorClassNameRandomizerAlwaysReturnsWarrior()
        {
            var warriors = new[]
                {
                    CharacterClassConstants.Barbarian,
                    CharacterClassConstants.Monk,
                    CharacterClassConstants.Paladin,
                    CharacterClassConstants.Fighter,
                    CharacterClassConstants.Ranger
                };

            while (TestShouldKeepRunning())
            {
                var className = ClassNameRandomizer.Randomize(Alignment);
                Assert.That(warriors.Contains(className), Is.True);
            }
        }
    }
}