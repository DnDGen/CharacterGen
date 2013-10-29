using System;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class HitPointsFactoryTests
    {
        private Mock<IDice> mockDice;
        private CharacterClass characterClass;
        private Race race;
        private Int32 constitutionBonus;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            characterClass = new CharacterClass();
            characterClass.ClassName = CharacterClassConstants.Barbarian;
            race = new Race();
            constitutionBonus = 0;
        }

        [Test]
        public void FighterGetsD10ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Fighter;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d10(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void PaladinGetsD10ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Paladin;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d10(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void BarbarianGetsD12ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Barbarian;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d12(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void ClericGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Cleric;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void DruidGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Druid;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void MonkGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Monk;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void RangerGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Ranger;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void BardGetsD6ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Bard;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d6(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void RogueGetsD6ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Rogue;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d6(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void SorcererGetsD4ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Sorcerer;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d4(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void WizardGetsD4ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Wizard;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d4(1, constitutionBonus), Times.Once());
        }

        [Test]
        public void RollHitPointsPerLevel()
        {
            for (var level = 1; level <= 20; level++)
            {
                mockDice.ResetCalls();
                characterClass.Level = level;

                HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
                mockDice.Verify(d => d.d12(1, constitutionBonus), Times.Exactly(level));
            }
        }

        [Test]
        public void ConstitutionBonusApplied()
        {
            characterClass.Level = 1;
            constitutionBonus = 2;

            HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d12(1, constitutionBonus), Times.Exactly(1));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerLevel()
        {
            for (var level = 1; level <= 20; level++)
            {
                characterClass.Level = level;

                var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, Int32.MinValue, race);
                Assert.That(hitPoints, Is.EqualTo(level));
            }
        }

        [Test]
        public void BugbearGains3d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Bugbear;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(3, 0), Times.Once);
        }

        [Test]
        public void DerroGains3d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Derro;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(3, 0), Times.Once);
        }

        [Test]
        public void DoppelgangerGains4d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Doppelganger;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(4, 0), Times.Once);
        }

        [Test]
        public void GnollGains2d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Gnoll;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(2, 0), Times.Once);
        }

        [Test]
        public void HalfDragonIncreasesHitDieFromD8ToD10()
        {
            race.Metarace = RaceConstants.Metaraces.HalfDragon;
            race.BaseRace = RaceConstants.BaseRaces.Derro;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d10(3, 0), Times.Once);
        }

        [Test]
        public void LizardfolkGains2d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Lizardfolk;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(2, 0), Times.Once);
        }

        [Test]
        public void MindFlayerGains8d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.MindFlayer;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(8, 0), Times.Once);
        }

        [Test]
        public void MinotaurGains6d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Derro;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(6, 0), Times.Once);
        }

        [Test]
        public void OgreGains4d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Ogre;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(4, 0), Times.Once);
        }

        [Test]
        public void OgreMageGains5d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Derro;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(5, 0), Times.Once);
        }

        [Test]
        public void TroglodyteGains2d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Derro;

            var hitPoints = HitPointsFactory.CreateUsing(mockDice.Object, characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(2, 0), Times.Once);
        }
    }
}