﻿using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class CharacterCombatGeneratorTests
    {
        private Mock<ISavingThrowsGenerator> mockSavingThrowsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IArmorClassGenerator> mockArmorClassGenerator;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private ICombatGenerator combatGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private Dictionary<String, Stat> stats;
        private Equipment equipment;
        private Race race;
        private Dictionary<String, Int32> maxDexterityBonuses;
        private List<String> averageBaseAttacks;
        private List<String> goodBaseAttacks;
        private Dictionary<String, Int32> racialBaseAttackAdjustments;
        private List<String> initiativeFeats;

        [SetUp]
        public void Setup()
        {
            mockArmorClassGenerator = new Mock<IArmorClassGenerator>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockSavingThrowsGenerator = new Mock<ISavingThrowsGenerator>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            combatGenerator = new CharacterCombatGenerator(mockArmorClassGenerator.Object, mockHitPointsGenerator.Object, mockSavingThrowsGenerator.Object,
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
            initiativeFeats = new List<String>();

            characterClass.ClassName = "class name";
            characterClass.Level = 20;
            stats[StatConstants.Constitution] = new Stat { Value = 9266 };
            stats[StatConstants.Dexterity] = new Stat { Value = 42 };
            racialBaseAttackAdjustments[String.Empty] = 0;
            maxDexterityBonuses[String.Empty] = 42;
            averageBaseAttacks.Add("other class name");
            goodBaseAttacks.Add("other class name");

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MaxDexterityBonus)).Returns(maxDexterityBonuses);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.GoodBaseAttack))
                .Returns(goodBaseAttacks);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.AverageBaseAttack))
                .Returns(averageBaseAttacks);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Initiative))
                .Returns(initiativeFeats);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments)).Returns(racialBaseAttackAdjustments);
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
            race.BaseRace = "baserace";
            race.Metarace = "metarace";

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
            equipment.Armor = new Item { Name = "armor" };
            maxDexterityBonuses[equipment.Armor.Name] = 17;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.AdjustedDexterityBonus, Is.EqualTo(16));
        }

        [Test]
        public void AdjustedDexterityBonusIsMaxBonusOfArmor()
        {
            equipment.Armor = new Item { Name = "armor" };
            maxDexterityBonuses[equipment.Armor.Name] = 5;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.AdjustedDexterityBonus, Is.EqualTo(5));
        }

        [Test]
        public void MithralIncreasesArmorMaxDexterityBonusBy2()
        {
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Traits.Add(TraitConstants.Mithral);
            maxDexterityBonuses[equipment.Armor.Name] = 5;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.AdjustedDexterityBonus, Is.EqualTo(7));
        }

        [Test]
        public void GetArmorClassFromGeneratorWithMaxDexterityBonus()
        {
            equipment.Armor = new Item { Name = "armor" };
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
            mockHitPointsGenerator.Setup(g => g.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race, feats)).Returns(90210);

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
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Strength = 90210;

            feats.Add(new Feat());
            feats[1].Name = "feat 2";
            feats[1].Strength = 600;

            initiativeFeats.Add(feats[1].Name);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.InitiativeBonus, Is.EqualTo(616));
        }

        [Test]
        public void InitiativeBonusIncludesNegativeDexterityBonus()
        {
            stats[StatConstants.Dexterity].Value = 1;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.InitiativeBonus, Is.EqualTo(-5));
        }

        [Test]
        public void InitiativeBonusUsesAdjustedDexterityBonus()
        {
            equipment.Armor = new Item { Name = "armor" };
            maxDexterityBonuses[equipment.Armor.Name] = 5;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass, race);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.InitiativeBonus, Is.EqualTo(5));
        }
    }
}