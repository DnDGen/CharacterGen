using System.Linq;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterClassGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterClassGenerator CharacterClassFactory { get; set; }
        [Inject]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public ILevelRandomizer LevelRandomizer { get; set; }

        [Test]
        public void CharacterClassFactoryReturnsPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(prototype, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsPrototypeWithClassName()
        {
            var classNames = CharacterClassConstants.GetClassNames();

            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(classNames.Contains(prototype.ClassName), Is.True);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsPrototypeWithLevel()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(prototype.Level, Is.GreaterThan(0));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClass()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithClassNameFromPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.ClassName, Is.EqualTo(prototype.ClassName));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithLevelFromPrototype()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.Level, Is.EqualTo(prototype.Level));
            }

            AssertIterations();
        }

        [Test]
        public void CharacterClassFactoryReturnsCharacterClassWithBaseAttack()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = CharacterClassFactory.CreatePrototypeWith(alignment, LevelRandomizer, ClassNameRandomizer);
                var characterClass = CharacterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.BaseAttack, Is.Not.Null);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.Not.Negative);
            }

            AssertIterations();
        }
    }
}