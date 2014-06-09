using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generators;
using NPCGen.Core.Generators.Interfaces;
using NPCGen.Core.Generators.Randomizers.CharacterClasses.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class CharacterClassFactoryTests
    {
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private ICharacterClassFactory characterClassFactory;

        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            characterClassFactory = new CharacterClassFactory();

            alignment = new Alignment();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
        }

        [Test]
        public void FactoryReturnsRandomizedLevel()
        {
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(9266);

            var characterClass = characterClassFactory.CreatePrototypeWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        [Test]
        public void FactoryReturnsRandomizedClass()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Barbarian);

            var characterClass = characterClassFactory.CreatePrototypeWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Barbarian));
        }

        [Test]
        public void FighterGetsGoodBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Fighter;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void PaladinGetsGoodBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Paladin;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void RangerGetsGoodBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Ranger;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void BarbarianGetsGoodBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Barbarian;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void BardGetsAverageBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Bard;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void ClericGetsAverageBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Cleric;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void MonkGetsAverageBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Monk;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void RogueGetsAverageBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Rogue;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void DruidGetsAverageBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Druid;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void SorcererGetsPoorBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Sorcerer;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(10));
        }

        [Test]
        public void WizardGetsPoorBaseAttackBonus()
        {
            var prototype = new CharacterClassPrototype();
            prototype.ClassName = CharacterClassConstants.Wizard;
            prototype.Level = 20;

            var characterClass = characterClassFactory.CreateWith(prototype);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(10));
        }

        [Test]
        public void GoodBaseAttackBonus()
        {
            for (var level = 1; level <= 20; level++)
            {
                var prototype = new CharacterClassPrototype();
                prototype.ClassName = CharacterClassConstants.Fighter;
                prototype.Level = level;

                var characterClass = characterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level));
            }
        }

        [Test]
        public void AverageBaseAttackBonus()
        {
            for (var level = 1; level <= 20; level++)
            {
                var prototype = new CharacterClassPrototype();
                prototype.ClassName = CharacterClassConstants.Cleric;
                prototype.Level = level;

                var characterClass = characterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level * 3 / 4));
            }
        }

        [Test]
        public void PoorBaseAttackBonus()
        {
            for (var level = 1; level <= 20; level++)
            {
                var prototype = new CharacterClassPrototype();
                prototype.ClassName = CharacterClassConstants.Wizard;
                prototype.Level = level;

                var characterClass = characterClassFactory.CreateWith(prototype);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level / 2));
            }
        }
    }
}