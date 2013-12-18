using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Factories
{
    [TestFixture]
    public class CharacterClassFactoryTests : IntegrationTest
    {
        [Inject]
        public ICharacterClassFactory CharacterClassFactory { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }
        [Inject]
        public AnyLevelRandomizer LevelRandomizer { get; set; }
        [Inject]
        public AnyClassNameRandomizer ClassNameRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void SingleCharacterClassGeneration()
        {
            CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
        }

        [Test]
        public void CharacterClassFactoryReturnsPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var prototype = CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(prototype, Is.Not.Null);
            }
        }

        [Test]
        public void CharacterClassFactoryReturnsPrototypeWithClassName()
        {
            var classNames = CharacterClassConstants.GetClassNames();

            while (TestShouldKeepRunning())
            {
                var prototype = CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(classNames.Contains(prototype.ClassName), Is.True);
            }
        }

        [Test]
        public void CharacterClassFactoryReturnsPrototypeWithLevel()
        {
            while (TestShouldKeepRunning())
            {
                var prototype = CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(prototype.Level, Is.GreaterThan(0));
            }
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClass()
        {
            while (TestShouldKeepRunning())
            {
                var prototype = CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass, Is.Not.Null);
            }
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithClassNameFromPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var prototype = CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.ClassName, Is.EqualTo(prototype.ClassName));
            }
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithLevelFromPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var prototype = CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.Level, Is.EqualTo(prototype.Level));
            }
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithBaseAttack()
        {
            while (TestShouldKeepRunning())
            {
                var prototype = CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.BaseAttack, Is.Not.Null);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.Not.Negative);
            }
        }
    }
}