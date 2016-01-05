using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class HitPointsGeneratorTests
    {
        private Mock<Dice> mockDice;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private IHitPointsGenerator hitPointsGenerator;

        private CharacterClass characterClass;
        private Race race;
        private int constitutionBonus;
        private Dictionary<string, int> hitDice;
        private List<Feat> feats;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            hitPointsGenerator = new HitPointsGenerator(mockDice.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            characterClass = new CharacterClass();
            race = new Race();
            feats = new List<Feat>();
            hitDice = new Dictionary<string, int>();

            characterClass.ClassName = "class name";
            race.Metarace = "metarace";
            constitutionBonus = 0;
            hitDice[characterClass.ClassName] = 9266;
            hitDice["otherclassname"] = 42;

            mockDice.Setup(d => d.Roll(0).d8()).Returns(0);
            mockDice.Setup(d => d.Roll(1).d(9266)).Returns(42);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ClassHitDice)).Returns(hitDice);
        }

        [Test]
        public void GetHitDieFromAdjustments()
        {
            characterClass.Level = 1;
            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(42));
        }

        [Test]
        public void RollHitPointsPerLevel()
        {
            characterClass.Level = 600;

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(42 * 600));
            mockDice.Verify(d => d.Roll(1).d(9266), Times.Exactly(600));
        }

        [Test]
        public void ConstitutionBonusAppliedPerLevel()
        {
            characterClass.Level = 2;
            constitutionBonus = 5;

            mockDice.Setup(d => d.Roll(1).d(9266)).Returns(42);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(94));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerLevel()
        {
            characterClass.Level = 600;
            constitutionBonus = int.MinValue;

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(600));
        }

        [Test]
        public void NonMonsterDoNotGetAdditionalHitDice()
        {
            characterClass.Level = 2;
            race.BaseRace = "differentbaserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(84));
            mockAdjustmentsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice), Times.Never);
        }

        [Test]
        public void MonstersGetAdditionalHitDice()
        {
            characterClass.Level = 2;
            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockDice.Setup(d => d.Roll(1).d(8)).Returns(5);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(99));
        }

        [Test]
        public void MonstersApplyConstitutionBonusForEachAdditionalHitDie()
        {
            characterClass.Level = 2;
            race.BaseRace = "baserace";
            constitutionBonus = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockDice.Setup(d => d.Roll(1).d(8)).Returns(5);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(124));
        }

        [Test]
        public void MonstersCannotGainFewerThan1HitPointPerHitDie()
        {
            characterClass.Level = 2;
            race.BaseRace = "baserace";
            constitutionBonus = -99999;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockDice.Setup(d => d.Roll(1).d(8)).Returns(7);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(5));
        }

        [Test]
        public void HalfDragonIncreasesMonsterHitDie()
        {
            characterClass.Level = 2;
            race.BaseRace = "baserace";
            race.Metarace = RaceConstants.Metaraces.HalfDragon;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockDice.Setup(d => d.Roll(1).d(10)).Returns(5);
            mockDice.Setup(d => d.Roll(1).d(8)).Returns(9);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(99));
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).d(8), Times.Never);
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).d8(), Times.Never);
        }

        [Test]
        public void UndeadIncreasesMonsterHitDie()
        {
            characterClass.Level = 2;
            race.BaseRace = "baserace";
            race.Metarace = "undead";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead))
                .Returns(new[] { "undead", "other undead" });

            mockDice.SetupSequence(d => d.Roll(1).d(12)).Returns(1).Returns(3).Returns(5).Returns(6).Returns(8);

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(23));
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).d(8), Times.Never);
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).d8(), Times.Never);
        }

        [Test]
        public void UndeadSetsClassHitDieTo12()
        {
            characterClass.Level = 2;
            race.Metarace = "undead";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead))
                .Returns(new[] { "undead", "other undead" });
            mockDice.Setup(d => d.Roll(1).d(12)).Returns(7);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(14));
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).d(9266), Times.Never);
        }

        [Test]
        public void ToughnessIncreassHitPoints()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(3));
        }

        [Test]
        public void ToughnessIncreassHitPointsMultipleTimes()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(6));
        }

        [Test]
        public void MinimumCheckAppliedPerLevel()
        {
            characterClass.Level = 3;
            constitutionBonus = -2;

            mockDice.SetupSequence(d => d.Roll(1).d(9266)).Returns(1).Returns(2).Returns(4);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(4));
        }

        [Test]
        public void MinimumCheckAppliedPerMonsterHitDie()
        {
            characterClass.Level = 2;
            race.BaseRace = "baserace";
            constitutionBonus = -2;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockDice.SetupSequence(d => d.Roll(1).d(8)).Returns(1).Returns(2).Returns(4);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            var classHitPoints = 80;
            var monsterHitPoints = 4;
            Assert.That(hitPoints, Is.EqualTo(classHitPoints + monsterHitPoints));
        }
    }
}