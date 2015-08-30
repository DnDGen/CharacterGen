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
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class HitPointsGeneratorTests
    {
        private Mock<IDice> mockDice;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private IHitPointsGenerator hitPointsGenerator;

        private CharacterClass characterClass;
        private Race race;
        private Int32 constitutionBonus;
        private Dictionary<String, Int32> hitDice;
        private List<Feat> feats;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            hitPointsGenerator = new HitPointsGenerator(mockDice.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            characterClass = new CharacterClass();
            race = new Race();
            feats = new List<Feat>();
            hitDice = new Dictionary<String, Int32>();

            characterClass.ClassName = "class name";
            race.Metarace = "metarace";
            constitutionBonus = 0;
            hitDice[characterClass.ClassName] = 9266;
            hitDice["otherclassname"] = 42;

            mockDice.Setup(d => d.Roll(0).d8()).Returns(0);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ClassHitDice)).Returns(hitDice);
        }

        [Test]
        public void GetHitDieFromAdjustments()
        {
            characterClass.Level = 1;
            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            mockDice.Verify(d => d.Roll(1).d(9266), Times.Once);
        }

        [Test]
        public void RollHitPointsPerLevel()
        {
            characterClass.Level = 600;
            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            mockDice.Verify(d => d.Roll(1).d(9266), Times.Exactly(600));
        }

        [Test]
        public void ConstitutionBonusAppliedPerLevel()
        {
            characterClass.Level = 2;
            constitutionBonus = 5;

            mockDice.Setup(d => d.Roll(1).d(9266)).Returns(45100);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(90210));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerLevel()
        {
            characterClass.Level = 600;
            constitutionBonus = Int32.MinValue;

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
            mockDice.Setup(d => d.Roll(1).d(9266)).Returns(7);

            var monsterHitDice = new Dictionary<String, Int32>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice["baserace"] = 2345;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(14));
            mockAdjustmentsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice), Times.Never);
        }

        [Test]
        public void MonstersGetAdditionalHitDice()
        {
            characterClass.Level = 2;
            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });
            mockDice.Setup(d => d.Roll(1).d(9266)).Returns(7);

            var monsterHitDice = new Dictionary<String, Int32>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice["baserace"] = 2345;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockDice.Setup(d => d.Roll(2345).d8()).Returns(5);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(19));
        }

        [Test]
        public void HalfDragonIncreasesMonsterHitDie()
        {
            characterClass.Level = 2;
            race.BaseRace = "baserace";
            race.Metarace = RaceConstants.Metaraces.HalfDragon;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });
            mockDice.Setup(d => d.Roll(1).d(9266)).Returns(7);

            var monsterHitDice = new Dictionary<String, Int32>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice["baserace"] = 2345;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockDice.Setup(d => d.Roll(2345).d10()).Returns(5);
            mockDice.Setup(d => d.Roll(2345).d8()).Returns(9);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(19));
            mockDice.Verify(d => d.Roll(2345).d8(), Times.Never);
        }

        [Test]
        public void ToughnessIncreassHitPoints()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Strength = 3 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(3));
        }

        [Test]
        public void ToughnessIncreassHitPointsMultipleTimes()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Strength = 3 });
            feats.Add(new Feat { Name = FeatConstants.Toughness, Strength = 3 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(6));
        }
    }
}