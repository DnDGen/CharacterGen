using System;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterClassFactoryTests
    {
        private Mock<IDice> mockDice;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;

        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();

            alignment = new Alignment();
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void FactoryReturnsRandomizedLevel()
        {
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(9266);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        [Test]
        public void FactoryReturnsRandomizedClass()
        {
            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Barbarian));
        }

        [Test]
        public void FighterGetsGoodBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Fighter);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void PaladinGetsGoodBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Paladin);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void RangerGetsGoodBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Ranger);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test]
        public void BarbarianGetsGoodBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Barbarian);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(20));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidClassNameThrowsError()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns("invalid class");
            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
        }

        [Test]
        public void BardGetsAverageBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Bard);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void ClericGetsAverageBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Cleric);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void MonkGetsAverageBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Monk);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void RogueGetsAverageBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Rogue);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void DruidGetsAverageBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Druid);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(15));
        }

        [Test]
        public void SorcererGetsPoorBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Sorcerer);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(10));
        }

        [Test]
        public void WizardGetsPoorBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Wizard);
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(20);

            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(10));
        }

        [Test]
        public void GoodBaseAttackBonus()
        {
            for (var level = 1; level <= 20; level++)
            {
                mockLevelRandomizer.Setup(r => r.Randomize()).Returns(level);
                var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level));
            }
        }

        [Test]
        public void AverageBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Cleric);

            for (var level = 1; level <= 20; level++)
            {
                mockLevelRandomizer.Setup(r => r.Randomize()).Returns(level);
                var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level * 3 / 4));
            }
        }

        [Test]
        public void PoorBaseAttackBonus()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Wizard);

            for (var level = 1; level <= 20; level++)
            {
                mockLevelRandomizer.Setup(r => r.Randomize()).Returns(level);
                var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.EqualTo(level / 2));
            }
        }
    }
}