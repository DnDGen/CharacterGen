using System;
using D20Dice;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories;
using NUnit.Framework;
using NPCGen.Core.Generation.Factories.Interfaces;

namespace NPCGen.Tests.Unit.Generation.Factories
{
    [TestFixture]
    public class HitPointsFactoryTests
    {
        private Mock<IDice> mockDice;
        private IHitPointsFactory hitPointsFactory;

        private CharacterClass characterClass;
        private Race race;
        private Int32 constitutionBonus;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            hitPointsFactory = new HitPointsFactory(mockDice.Object);

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

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d10(1), Times.Once());
        }

        [Test]
        public void PaladinGetsD10ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Paladin;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d10(1), Times.Once());
        }

        [Test]
        public void BarbarianGetsD12ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Barbarian;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d12(1), Times.Once());
        }

        [Test]
        public void ClericGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Cleric;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1), Times.Once());
        }

        [Test]
        public void DruidGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Druid;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1), Times.Once());
        }

        [Test]
        public void MonkGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Monk;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1), Times.Once());
        }

        [Test]
        public void RangerGetsD8ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Ranger;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1), Times.Once());
        }

        [Test]
        public void BardGetsD6ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Bard;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d6(1), Times.Once());
        }

        [Test]
        public void RogueGetsD6ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Rogue;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d6(1), Times.Once());
        }

        [Test]
        public void SorcererGetsD4ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Sorcerer;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d4(1), Times.Once());
        }

        [Test]
        public void WizardGetsD4ForHitPoints()
        {
            characterClass.Level = 1;
            characterClass.ClassName = CharacterClassConstants.Wizard;

            hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d4(1), Times.Once());
        }

        [Test]
        public void RollHitPointsPerLevel()
        {
            for (var level = 1; level <= 20; level++)
            {
                mockDice.ResetCalls();
                characterClass.Level = level;

                hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
                mockDice.Verify(d => d.d12(1), Times.Exactly(level));
            }
        }

        [Test]
        public void ConstitutionBonusApplied()
        {
            characterClass.Level = 1;
            constitutionBonus = 2;
            var roll = 4;
            mockDice.Setup(d => d.d12(1)).Returns(roll);

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            Assert.That(hitPoints, Is.EqualTo(roll + constitutionBonus));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerLevel()
        {
            for (var level = 1; level <= 20; level++)
            {
                characterClass.Level = level;

                var hitPoints = hitPointsFactory.CreateWith(characterClass, Int32.MinValue, race);
                Assert.That(hitPoints, Is.EqualTo(level));
            }
        }

        [Test]
        public void BugbearGains3d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Bugbear;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(3), Times.Once);
        }

        [Test]
        public void DerroGains3d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Derro;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(3), Times.Once);
        }

        [Test]
        public void DoppelgangerGains4d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Doppelganger;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(4), Times.Once);
        }

        [Test]
        public void GnollGains2d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Gnoll;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(2), Times.Once);
        }

        [Test]
        public void HalfDragonIncreasesHitDieFromD8ToD10()
        {
            race.Metarace = RaceConstants.Metaraces.HalfDragon;
            race.BaseRace = RaceConstants.BaseRaces.Derro;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d10(It.IsAny<Int32>()), Times.Once);
        }

        [Test]
        public void AnyMetaraceExceptHalfDragonGetsD8HitDice()
        {
            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(It.IsAny<Int32>()), Times.Once);
        }

        [Test]
        public void LizardfolkGains2d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Lizardfolk;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(2), Times.Once);
        }

        [Test]
        public void MindFlayerGains8d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.MindFlayer;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(8), Times.Once);
        }

        [Test]
        public void MinotaurGains6d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Minotaur;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(6), Times.Once);
        }

        [Test]
        public void OgreGains4d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Ogre;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(4), Times.Once);
        }

        [Test]
        public void OgreMageGains5d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.OgreMage;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(5), Times.Once);
        }

        [Test]
        public void TroglodyteGains2d8HitDice()
        {
            race.BaseRace = RaceConstants.BaseRaces.Troglodyte;

            var hitPoints = hitPointsFactory.CreateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(2), Times.Once);
        }
    }
}