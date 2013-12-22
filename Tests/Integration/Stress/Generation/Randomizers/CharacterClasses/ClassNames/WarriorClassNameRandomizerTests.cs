using System.Linq;
using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Tests.Integration.Common;
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
                var data = GetNewInstanceOf<DependentDataCollection>();
                var className = ClassNameRandomizer.Randomize(data.Alignment);
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
                var data = GetNewInstanceOf<DependentDataCollection>();
                var className = ClassNameRandomizer.Randomize(data.Alignment);
                Assert.That(warriors.Contains(className), Is.True);
            }

            AssertIterations();
        }
    }
}