using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class HitPointsFactoryTests
    {
        private Mock<IDice> mockDice;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            characterClass = new CharacterClass();
        }

        [Test]
        public void FighterGetsD10ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Fighter;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d10(1, 0), Times.Once());
        }

        [Test]
        public void PaladinGetsD10ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Paladin;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d10(1, 0), Times.Once());
        }

        [Test]
        public void BarbarianGetsD12ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Barbarian;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d12(1, 0), Times.Once());
        }

        [Test]
        public void ClericGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Cleric;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d8(1, 0), Times.Once());
        }

        [Test]
        public void DruidGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Druid;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d8(1, 0), Times.Once());
        }

        [Test]
        public void MonkGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Monk;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d8(1, 0), Times.Once());
        }

        [Test]
        public void RangerGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Ranger;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d8(1, 0), Times.Once());
        }

        [Test]
        public void BardGetsD6ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Bard;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d6(1, 0), Times.Once());
        }

        [Test]
        public void RogueGetsD6ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Rogue;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d6(1, 0), Times.Once());
        }

        [Test]
        public void SorcererGetsD4ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Sorcerer;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d4(1, 0), Times.Once());
        }

        [Test]
        public void WizardGetsD4ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Wizard;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d4(1, 0), Times.Once());
        }

        [Test]
        public void RollHitPointsPerLevel()
        {
            characterClass.ClassName = CharacterClassConstants.Barbarian;

            for (var level = 1; level <= 20; level++)
            {
                mockDice.ResetCalls();
                characterClass.Level = level;

                HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
                mockDice.Verify(d => d.d12(1, 0), Times.Exactly(level));
            }
        }

        [Test]
        public void ConstitutionBonusApplied()
        {
            characterClass.Level = 1;
            var constitutionBonus = 2;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, 0);
            mockDice.Verify(d => d.d12(1, constitutionBonus), Times.Exactly(1));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerLevel()
        {
            for (var level = 1; level <= 20; level++)
            {
                characterClass.Level = level;

                var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, -9000);
                Assert.That(hitPoints, Is.EqualTo(level));
            }
        }
    }
}