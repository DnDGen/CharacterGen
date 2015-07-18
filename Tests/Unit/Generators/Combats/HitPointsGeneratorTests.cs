using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Combats;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Combats
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

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            hitPointsGenerator = new HitPointsGenerator(mockDice.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            characterClass = new CharacterClass();
            characterClass.ClassName = "class name";
            race = new Race();
            race.Metarace = "metarace";
            constitutionBonus = 0;
            mockDice.Setup(d => d.Roll(0).d8()).Returns(0);

            hitDice = new Dictionary<String, Int32>();
            hitDice[characterClass.ClassName] = 9266;
            hitDice["otherclassname"] = 42;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ClassHitDice)).Returns(hitDice);
        }

        [Test]
        public void GetHitDieFromAdjustments()
        {
            characterClass.Level = 1;
            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.Roll(1).d(9266), Times.Once);
        }

        [Test]
        public void RollHitPointsPerLevel()
        {
            characterClass.Level = 600;
            hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            mockDice.Verify(d => d.Roll(1).d(9266), Times.Exactly(600));
        }

        [Test]
        public void ConstitutionBonusAppliedPerLevel()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.Roll(1).d(9266)).Returns(45100);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, 5, race);
            Assert.That(hitPoints, Is.EqualTo(90210));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerLevel()
        {
            characterClass.Level = 90210;
            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, Int32.MinValue, race);
            Assert.That(hitPoints, Is.EqualTo(90210));
        }

        [Test]
        public void NonMonsterDoNotGetadditionalHitDice()
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

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
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

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
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

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race);
            Assert.That(hitPoints, Is.EqualTo(19));
            mockDice.Verify(d => d.Roll(2345).d8(), Times.Never);
        }
    }
}