using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators.Combats;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class CombatGeneratorTests
    {
        private const Int32 Good = 20;
        private const Int32 Average = 15;
        private const Int32 Poor = 10;

        private ICombatGenerator combatGenerator;
        private CharacterClass characterClass;
        private List<String> feats;
        private Dictionary<String, Stat> stats;
        private Equipment equipment;
        private Mock<IArmorClassGenerator> mockArmorClassGenerator;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Race race;
        private Mock<ISavingThrowsGenerator> mockSavingThrowsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Dictionary<String, Int32> maxDexterityBonuses;

        [SetUp]
        public void Setup()
        {
            mockArmorClassGenerator = new Mock<IArmorClassGenerator>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockSavingThrowsGenerator = new Mock<ISavingThrowsGenerator>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            combatGenerator = new CombatGenerator(mockArmorClassGenerator.Object, mockHitPointsGenerator.Object, mockSavingThrowsGenerator.Object,
                mockAdjustmentsSelector.Object);
            characterClass = new CharacterClass();
            feats = new List<String>();
            stats = new Dictionary<String, Stat>();
            equipment = new Equipment();
            race = new Race();
            maxDexterityBonuses = new Dictionary<String, Int32>();

            characterClass.ClassName = CharacterClassConstants.Fighter;
            stats[StatConstants.Constitution] = new Stat { Value = 9266 };
            stats[StatConstants.Dexterity] = new Stat { Value = 42 };
            mockAdjustmentsSelector.Setup(s => s.SelectFrom("MaxDexterityBonuses")).Returns(maxDexterityBonuses);
            maxDexterityBonuses[String.Empty] = 42;
        }

        [Test]
        public void InvalidClassNameThrowsError()
        {
            characterClass.ClassName = "invalid name";
            Assert.That(() => combatGenerator.GenerateBaseAttackWith(characterClass), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase(CharacterClassConstants.Fighter, Good)]
        [TestCase(CharacterClassConstants.Paladin, Good)]
        [TestCase(CharacterClassConstants.Ranger, Good)]
        [TestCase(CharacterClassConstants.Barbarian, Good)]
        [TestCase(CharacterClassConstants.Bard, Average)]
        [TestCase(CharacterClassConstants.Cleric, Average)]
        [TestCase(CharacterClassConstants.Monk, Average)]
        [TestCase(CharacterClassConstants.Rogue, Average)]
        [TestCase(CharacterClassConstants.Druid, Average)]
        [TestCase(CharacterClassConstants.Sorcerer, Poor)]
        [TestCase(CharacterClassConstants.Wizard, Poor)]
        public void FullAttackBonus(String className, Int32 attackBonus)
        {
            characterClass.ClassName = className;
            characterClass.Level = 20;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            Assert.That(baseAttack.Bonus, Is.EqualTo(attackBonus));
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
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = level;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
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
            characterClass.ClassName = CharacterClassConstants.Cleric;
            characterClass.Level = level;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
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
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = level;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            Assert.That(baseAttack.Bonus, Is.EqualTo(bonus));
        }

        [Test]
        public void ReturnCombatWithBaseAttack()
        {
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.BaseAttack, Is.EqualTo(baseAttack));
        }

        [Test]
        public void AdjustedDexterityBonusIsBonus()
        {
            equipment.Armor.Name = "armor";
            maxDexterityBonuses[equipment.Armor.Name] = 17;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.AdjustedDexterityBonus, Is.EqualTo(16));
        }

        [Test]
        public void AdjustedDexterityBonusIsMaxBonusOfArmor()
        {
            equipment.Armor.Name = "armor";
            maxDexterityBonuses[equipment.Armor.Name] = 5;

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.AdjustedDexterityBonus, Is.EqualTo(5));
        }

        [Test]
        public void GetArmorClassFromGeneratorWithMaxDexterityBonus()
        {
            equipment.Armor.Name = "armor";
            maxDexterityBonuses[equipment.Armor.Name] = 5;
            var armorClass = new ArmorClass();
            mockArmorClassGenerator.Setup(g => g.GenerateWith(equipment, 5, feats)).Returns(armorClass);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GetArmorClassFromGeneratorWithDexterityBonus()
        {
            var armorClass = new ArmorClass();
            mockArmorClassGenerator.Setup(g => g.GenerateWith(equipment, 16, feats)).Returns(armorClass);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.ArmorClass, Is.EqualTo(armorClass));
        }

        [Test]
        public void GetHitPointsFromGenerator()
        {
            mockHitPointsGenerator.Setup(g => g.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race)).Returns(90210);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.HitPoints, Is.EqualTo(90210));
        }

        [Test]
        public void GetSavingThrowsFromGenerator()
        {
            var savingThrows = new SavingThrows();
            mockSavingThrowsGenerator.Setup(g => g.GenerateWith(characterClass, feats, stats)).Returns(savingThrows);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);
            var combat = combatGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);

            Assert.That(combat.SavingThrows, Is.EqualTo(savingThrows));
        }
    }
}