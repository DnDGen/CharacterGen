using System;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterClassFactoryTests
    {
        private Mock<IDice> mockDice;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IClassNameRandomizer> mockClassRandomizer;

        private ICharacterClassFactory characterClassFactory;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockClassRandomizer = new Mock<IClassNameRandomizer>();

            alignment = new Alignment();
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Barbarian);

            characterClassFactory = new CharacterClassFactory(mockDice.Object, mockLevelRandomizer.Object,
                mockClassRandomizer.Object);
        }

        [Test]
        public void RandomizersProperlySet()
        {
            Assert.That(characterClassFactory.CharacterClassRandomizer, Is.EqualTo(mockClassRandomizer.Object));
            Assert.That(characterClassFactory.LevelRandomizer, Is.EqualTo(mockLevelRandomizer.Object));
        }

        [Test]
        public void FactoryReturnsRandomizedLevel()
        {
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(9266);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        [Test]
        public void ChangeLevelRandomizer()
        {
            var differentRandomizer = new Mock<ILevelRandomizer>();
            differentRandomizer.Setup(r => r.Randomize()).Returns(42);
            characterClassFactory.LevelRandomizer = differentRandomizer.Object;

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.Level, Is.EqualTo(42));
        }

        [Test]
        public void FactoryReturnsRandomizedClass()
        {
            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Barbarian));
        }

        [Test]
        public void ChangeClassRandomizer()
        {
            var differentRandomizer = new Mock<IClassNameRandomizer>();
            differentRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Wizard);
            characterClassFactory.CharacterClassRandomizer = differentRandomizer.Object;

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void FighterGetsGoodBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Fighter);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void PaladinGetsGoodBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Paladin);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void RangerGetsGoodBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Ranger);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void BarbarianGetsGoodBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Barbarian);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidClassNameThrowsError()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns("invalid class");
            var characterClass = characterClassFactory.Generate(alignment, 0);
        }

        [Test]
        public void BardGetsAverageBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Bard);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void ClericGetsAverageBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Cleric);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void MonkGetsAverageBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Monk);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void RogueGetsAverageBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Rogue);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void DruidGetsAverageBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Druid);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void SorcererGetsPoorBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Sorcerer);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(10));
        }

        [Test]
        public void WizardGetsPoorBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Wizard);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(10));
        }

        [Test]
        public void GoodBaseAttackBonus()
        {
            for (var level = 1; level <= 20; level++)
            {
                mockLevelRandomizer.Setup(r => r.Randomize()).Returns(level);
                var characterClass = characterClassFactory.Generate(alignment, 0);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level));
            }
        }

        [Test]
        public void AverageBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Cleric);

            for (var level = 1; level <= 20; level++)
            {
                mockLevelRandomizer.Setup(r => r.Randomize()).Returns(level);
                var characterClass = characterClassFactory.Generate(alignment, 0);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level * 3 / 4));
            }
        }

        [Test]
        public void PoorBaseAttackBonus()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Wizard);

            for (var level = 1; level <= 20; level++)
            {
                mockLevelRandomizer.Setup(r => r.Randomize()).Returns(level);
                var characterClass = characterClassFactory.Generate(alignment, 0);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level / 2));
            }
        }
    }
}