using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Combats
{
    public class CombatGenerator : ICombatGenerator
    {
        private IArmorClassGenerator armorClassGenerator;
        private IHitPointsGenerator hitPointsGenerator;
        private ISavingThrowsGenerator savingThrowsGenerator;
        private IAdjustmentsSelector adjustmentsSelector;

        public CombatGenerator(IArmorClassGenerator armorClassGenerator, IHitPointsGenerator hitPointsGenerator, ISavingThrowsGenerator savingThrowsGenerator,
            IAdjustmentsSelector adjustmentsSelector)
        {
            this.armorClassGenerator = armorClassGenerator;
            this.hitPointsGenerator = hitPointsGenerator;
            this.savingThrowsGenerator = savingThrowsGenerator;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public BaseAttack GenerateBaseAttackWith(CharacterClass characterClass)
        {
            var baseAttack = new BaseAttack();
            baseAttack.Bonus = GetBaseAttackBonus(characterClass);
            return baseAttack;
        }

        private Int32 GetBaseAttackBonus(CharacterClass characterClass)
        {
            switch (characterClass.ClassName)
            {
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Paladin:
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Barbarian: return GetGoodBaseAttackBonus(characterClass.Level);
                case CharacterClassConstants.Bard:
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Monk:
                case CharacterClassConstants.Rogue:
                case CharacterClassConstants.Druid: return GetAverageBaseAttackBonus(characterClass.Level);
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard: return GetPoorBaseAttackBonus(characterClass.Level);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private Int32 GetGoodBaseAttackBonus(Int32 level)
        {
            return level;
        }

        private Int32 GetAverageBaseAttackBonus(Int32 level)
        {
            return level * 3 / 4;
        }

        private Int32 GetPoorBaseAttackBonus(Int32 level)
        {
            return level / 2;
        }

        public Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<String> feats, Dictionary<String, Stat> stats, Equipment equipment)
        {
            var combat = new Combat();

            combat.BaseAttack = baseAttack;
            combat.AdjustedDexterityBonus = GetAdjustedDexterityBonus(stats, equipment);
            combat.ArmorClass = armorClassGenerator.GenerateWith(equipment, combat.AdjustedDexterityBonus, feats);
            combat.HitPoints = hitPointsGenerator.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race);
            combat.SavingThrows = savingThrowsGenerator.GenerateWith(characterClass, feats, stats);

            return combat;
        }

        private Int32 GetAdjustedDexterityBonus(Dictionary<String, Stat> stats, Equipment equipment)
        {
            var maxDexterityBonuses = adjustmentsSelector.SelectFrom("MaxDexterityBonuses");
            var dexterityBonus = stats[StatConstants.Dexterity].Bonus;
            var maxArmorBonus = maxDexterityBonuses[equipment.Armor.Name];

            return Math.Min(dexterityBonus, maxArmorBonus);
        }
    }
}