using System;
using D20Dice;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Combats;
using NPCGen.Generators.Interfaces.Combats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class HitPointsGeneratorTests
    {
        private Mock<IDice> mockDice;
        private IHitPointsGenerator hitPointsGenerator;

        private CharacterClass characterClass;
        private Race race;
        private Int32 constitutionBonus;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            hitPointsGenerator = new HitPointsGenerator(mockDice.Object);

            characterClass = new CharacterClass();
            characterClass.ClassName = CharacterClassConstants.Barbarian;
            race = new Race();
            constitutionBonus = 0;
        }

        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Paladin)]
        public void D10ForHitPoints(String className)
        {
            characterClass.Level = 1;
            characterClass.ClassName = className;

            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d10(1), Times.Once());
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        public void D12ForHitPoints(String className)
        {
            characterClass.Level = 1;
            characterClass.ClassName = className;

            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d12(1), Times.Once());
        }

        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Druid)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Ranger)]
        public void D8ForHitPoints(String className)
        {
            characterClass.Level = 1;
            characterClass.ClassName = className;

            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(1), Times.Once());
        }

        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Rogue)]
        public void D6ForHitPoints(String className)
        {
            characterClass.Level = 1;
            characterClass.ClassName = className;

            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d6(1), Times.Once());
        }

        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard)]
        public void D4ForHitPoints(String className)
        {
            characterClass.Level = 1;
            characterClass.ClassName = className;

            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d4(1), Times.Once());
        }

        [Test]
        public void RollHitPointsPerLevel()
        {
            characterClass.Level = 9266;

            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d12(1), Times.Exactly(9266));
        }

        [Test]
        public void ConstitutionBonusAppliedPerLevel()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.d12(1)).Returns(4600);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, 33, race);
            Assert.That(hitPoints, Is.EqualTo(9266));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerLevel()
        {
            characterClass.Level = 9266;
            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, Int32.MinValue, race);
            Assert.That(hitPoints, Is.EqualTo(9266));
        }

        [TestCase(RaceConstants.BaseRaces.Bugbear, 3)]
        [TestCase(RaceConstants.BaseRaces.Derro, 3)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 2)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 8)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 6)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 5)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 2)]
        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        public void AdditionalHitDice(String baseRace, Int32 numberOfHitDice)
        {
            race.BaseRace = baseRace;
            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(numberOfHitDice), Times.Once);
        }

        [TestCase(RaceConstants.BaseRaces.Bugbear, 3)]
        [TestCase(RaceConstants.BaseRaces.Derro, 3)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 2)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 8)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 6)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 5)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 2)]
        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        public void HalfDragonIncreasesHitDieFromD8ToD10(String baseRace, Int32 numberOfHitDice)
        {
            race.Metarace = RaceConstants.Metaraces.HalfDragon;
            race.BaseRace = baseRace;

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d10(numberOfHitDice), Times.Once);
            mockDice.Verify(d => d.d8(It.IsAny<Int32>()), Times.Never);
        }

        [TestCase(RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(RaceConstants.Metaraces.HalfFiend)]
        [TestCase(RaceConstants.Metaraces.Werebear)]
        [TestCase(RaceConstants.Metaraces.Wereboar)]
        [TestCase(RaceConstants.Metaraces.Wererat)]
        [TestCase(RaceConstants.Metaraces.Weretiger)]
        [TestCase(RaceConstants.Metaraces.Werewolf)]
        [TestCase(RaceConstants.Metaraces.None)]
        public void MetaraceDoesNotIncreaseHitDice(String metarace)
        {
            race.Metarace = metarace;
            race.BaseRace = RaceConstants.BaseRaces.Derro;

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.d8(3), Times.Once);
            mockDice.Verify(d => d.d10(It.IsAny<Int32>()), Times.Never);
        }
    }
}