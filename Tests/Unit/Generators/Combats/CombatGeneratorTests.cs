using System;
using System.Collections.Generic;
using EquipmentGen.Common.Items;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
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
        private List<Feat> feats;
        private Dictionary<String, Stat> stats;
        private Equipment equipment;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            combatGenerator = new CombatGenerator();
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            stats = new Dictionary<String, Stat>();
            equipment = new Equipment();

            characterClass.ClassName = CharacterClassConstants.Fighter;
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
            var combat = combatGenerator.GenerateWith(baseAttack, feats, stats, equipment);

            Assert.That(combat.BaseAttack, Is.EqualTo(baseAttack));
        }

        [Test]
        public void ArmorClassesStartsAt10()
        {
            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);

            var combat = combatGenerator.GenerateWith(baseAttack, feats, stats, equipment);
            Assert.That(combat.ArmorClass.FlatFooted, Is.EqualTo(10));
            Assert.That(combat.ArmorClass.Full, Is.EqualTo(10));
            Assert.That(combat.ArmorClass.Touch, Is.EqualTo(10));
        }

        [Test]
        public void FullArmorClassInvolvesEverything()
        {
            var armorBonuses = new Dictionary<String, Int32>();
            armorBonuses["shield"] = 1;
            armorBonuses["armor"] = 3;
            armorBonuses["other armor"] = -1;
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("ArmorBonuses")).Returns(armorBonuses);

            stats[StatConstants.Dexterity] = new Stat { Value = 24 };

            var featAdjustments = new Dictionary<String, Int32>();
            featAdjustments["feat 1"] = 2;
            featAdjustments["feat 2"] = 4;
            featAdjustments["feat 4"] = -2;
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("FeatArmorAdjustments")).Returns(featAdjustments);

            mockCollectionsSelector.Setup(s => s.SelectFrom("DeflectionBonuses")).Returns(new[] { "ring", "other ring" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("NaturalArmor")).Returns(new[] { "feat 1", "amulet", "other feat" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("DodgeBonuses")).Returns(new[] { "feat 2", "other feat" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("SizeModifier")).Returns(new[] { "feat 2", "other feat" });

            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });
            feats.Add(new Feat { Name = "feat 3" });

            equipment.Armor.Name = "armor";
            equipment.Armor.Magic.Bonus = 5;
            equipment.OffHand.Name = "shield";
            equipment.OffHand.Magic.Bonus = 6;

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 7;

            var amulet = new Item();
            amulet.Name = "amulet";
            amulet.Magic.Bonus = 8;

            equipment.Treasure.Items = new[]
                {
                    ring,
                    amulet,
                    new Item { Name = "thing" }
                };

            var baseAttack = combatGenerator.GenerateBaseAttackWith(characterClass);

            var combat = combatGenerator.GenerateWith(baseAttack, feats, stats, equipment);
            Assert.That(combat.ArmorClass.Full, Is.EqualTo(55));
        }
    }
}