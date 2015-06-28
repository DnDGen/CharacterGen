using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators.Combats;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class CombatGeneratorTests
    {
        private ICombatGenerator combatGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private Dictionary<String, Stat> stats;
        private Equipment equipment;
        private Mock<IArmorClassGenerator> mockArmorClassGenerator;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Race race;
        private Mock<ISavingThrowsGenerator> mockSavingThrowsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Dictionary<String, Int32> maxDexterityBonuses;
        private List<String> averageBaseAttacks;
        private List<String> goodBaseAttacks;
        private Dictionary<String, Int32> racialBaseAttackAdjustments;
        private Dictionary<String, Int32> racialInitiativeAdjustments;
        private Dictionary<String, Int32> featInitiativeAdjustments;

        [SetUp]
        public void Setup()
        {
            mockArmorClassGenerator = new Mock<IArmorClassGenerator>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockSavingThrowsGenerator = new Mock<ISavingThrowsGenerator>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            combatGenerator = new CombatGenerator(mockArmorClassGenerator.Object, mockHitPointsGenerator.Object, mockSavingThrowsGenerator.Object,
                mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            stats = new Dictionary<String, Stat>();
            equipment = new Equipment();
            race = new Race();
            maxDexterityBonuses = new Dictionary<String, Int32>();
            averageBaseAttacks = new List<String>();
            goodBaseAttacks = new List<String>();
            racialBaseAttackAdjustments = new Dictionary<String, Int32>();
            racialInitiativeAdjustments = new Dictionary<String, Int32>();
            featInitiativeAdjustments = new Dictionary<String, Int32>();

            characterClass.ClassName = "class name";
            characterClass.Level = 20;
            stats[StatConstants.Constitution] = new Stat { Value = 9266 };
            stats[StatConstants.Dexterity] = new Stat { Value = 42 };
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MaxDexterityBonus)).Returns(maxDexterityBonuses);
            maxDexterityBonuses[String.Empty] = 42;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.GoodBaseAttack))
                .Returns(goodBaseAttacks);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.AverageBaseAttack))
                .Returns(averageBaseAttacks);
            averageBaseAttacks.Add("other class name");
            goodBaseAttacks.Add("other class name");

            racialBaseAttackAdjustments[String.Empty] = 0;
            racialInitiativeAdjustments[String.Empty] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments)).Returns(racialBaseAttackAdjustments);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.RacialInitiativeBonuses)).Returns(racialInitiativeAdjustments);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.FeatInitiativeBonuses)).Returns(featInitiativeAdjustments);
        }

        [Test]
        public void GetGoodBaseAttack()
        {
            goodBaseAttacks.Add(characterClass.ClassName);
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(20));
        }

        [Test]
        public void GetAverageBaseAttack()
        {
            averageBaseAttacks.Add(characterClass.ClassName);
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(15));
        }

        [Test]
        public void DefaultIsPoorBaseAttack()
        {
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(10));
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 9)]
        [TestCase(10, 10)]
        [TestCase(11, 11)]
        [TestCase(12, 12)]
        [TestCase(13, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(17, 17)]
        [TestCase(18, 18)]
        [TestCase(19, 19)]
        [TestCase(20, 20)]
        public void GoodBaseAttackBonus(Int32 level, Int32 bonus)
        {
            characterClass.Level = level;
            goodBaseAttacks.Add(characterClass.ClassName);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(bonus));
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 3)]
        [TestCase(6, 4)]
        [TestCase(7, 5)]
        [TestCase(8, 6)]
        [TestCase(9, 6)]
        [TestCase(10, 7)]
        [TestCase(11, 8)]
        [TestCase(12, 9)]
        [TestCase(13, 9)]
        [TestCase(14, 10)]
        [TestCase(15, 11)]
        [TestCase(16, 12)]
        [TestCase(17, 12)]
        [TestCase(18, 13)]
        [TestCase(19, 14)]
        [TestCase(20, 15)]
        public void AverageBaseAttackBonus(Int32 level, Int32 bonus)
        {
            characterClass.Level = level;
            averageBaseAttacks.Add(characterClass.ClassName);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(bonus));
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 4)]
        [TestCase(9, 4)]
        [TestCase(10, 5)]
        [TestCase(11, 5)]
        [TestCase(12, 6)]
        [TestCase(13, 6)]
        [TestCase(14, 7)]
        [TestCase(15, 7)]
        [TestCase(16, 8)]
        [TestCase(17, 8)]
        [TestCase(18, 9)]
        [TestCase(19, 9)]
        [TestCase(20, 10)]
        public void PoorBaseAttackBonus(Int32 level, Int32 bonus)
        {
            characterClass.Level = level;
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(bonus));
        }

        [Test]
        public void GetAllRacialBaseAttackAdjustments()
        {
            race.BaseRace.Id = "baserace";
            race.Metarace.Id = "metarace";

            var racialAdjustments = new Dictionary<String, Int32>();
            racialAdjustments["baserace"] = 1;
            racialAdjustments["otherbaserace"] = 7;
            racialAdjustments["metarace"] = 3;
            racialAdjustments["othermetarace"] = 5;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments)).Returns(racialAdjustments);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(14));
        }

        [Test]
        public void LargeCreaturesAreMinusOneOnBaseAttack()
        {
            race.Size = RaceConstants.Sizes.Large;
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(9));
        }

        [Test]
        public void SmallCreaturesArePlusOneOnBaseAttack()
        {
            race.Size = RaceConstants.Sizes.Small;
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.EqualTo(11));
        }

        [Test]
        public void ReturnCombatWithBaseAttack()
        {
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);
            Assert.That(combat.BaseAttack, Is.EqualTo(baseAttack));
        }

        [Test]
        public void AdjustedDexterityBonusIsBonus()
        {
            equipment.Armor.Name = "armor";
            maxDexterityBonuses[equipment.Armor.Name] = 17;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.AdjustedDexterityBonus, Is.EqualTo(16));
        }

        [Test]
        public void AdjustedDexterityBonusIsMaxBonusOfArmor()
        {
            equipment.Armor.Name = "armor";
            maxDexterityBonuses[equipment.Armor.Name] = 5;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.AdjustedDexterityBonus, Is.EqualTo(5));
        }

        [Test]
        public void GetArmorClassFromGeneratorWithMaxDexterityBonus()
        {
            equipment.Armor.Name = "armor";
            maxDexterityBonuses[equipment.Armor.Name] = 5;
            var armorClass = new ArmorClass();
            mockArmorClassGenerator.Setup(g => g.GenerateWith(equipment, 5, feats, race)).Returns(armorClass);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GetArmorClassFromGeneratorWithDexterityBonus()
        {
            var armorClass = new ArmorClass();
            mockArmorClassGenerator.Setup(g => g.GenerateWith(equipment, 16, feats, race)).Returns(armorClass);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GetHitPointsFromGenerator()
        {
            mockHitPointsGenerator.Setup(g => g.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race)).Returns(90210);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.HitPoints, Is.EqualTo(90210));
        }

        [Test]
        public void GetSavingThrowsFromGenerator()
        {
            var savingThrows = new SavingThrows();
            mockSavingThrowsGenerator.Setup(g => g.GenerateWith(characterClass, feats, stats)).Returns(savingThrows);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.SavingThrows, Is.EqualTo(savingThrows));
        }

        [Test]
        public void InitiativeBonusIsSumOfBonuses()
        {
            race.BaseRace.Id = "baserace";
            race.Metarace.Id = "metarace";

            var feat1 = new Feat();
            feat1.Name.Id = "feat 1";
            feats.Add(feat1);

            var feat2 = new Feat();
            feat2.Name.Id = "feat 2";
            feats.Add(feat2);

            var feat3 = new Feat();
            feat3.Name.Id = "feat 3";
            feats.Add(feat3);

            racialInitiativeAdjustments[race.BaseRace.Id] = 1;
            racialInitiativeAdjustments[race.Metarace.Id] = 1;
            racialInitiativeAdjustments["other race"] = 5;

            featInitiativeAdjustments[feats[0].Name.Id] = 1;
            featInitiativeAdjustments[feats[1].Name.Id] = 0;
            featInitiativeAdjustments[feats[2].Name.Id] = 1;

            racialBaseAttackAdjustments[race.BaseRace.Id] = 0;
            racialBaseAttackAdjustments[race.Metarace.Id] = 0;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.InitiativeBonus, Is.EqualTo(4));
        }
    }
}