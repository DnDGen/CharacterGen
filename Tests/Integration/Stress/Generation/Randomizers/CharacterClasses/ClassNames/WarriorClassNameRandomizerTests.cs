using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class WarriorClassNameRandomizerTests : StressTest
    {
        [Inject]
        public WarriorClassNameRandomizer ClassNameRandomizer { get; set; }

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
        public void WarriorClassNameRandomizerReturnsClassName()
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
                var alignment = GetNewInstanceOf<Alignment>();
                var className = ClassNameRandomizer.Randomize(alignment);
                Assert.That(warriors.Contains(className), Is.True);
            }

            AssertIterations();
        }
    }
}