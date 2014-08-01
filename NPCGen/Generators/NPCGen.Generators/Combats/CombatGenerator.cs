using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Generators.Interfaces.Combats;

namespace NPCGen.Generators.Combats
{
    public class CombatGenerator : ICombatGenerator
    {
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

        public Combat GenerateWith(BaseAttack baseAttack, IEnumerable<Feat> feats, Dictionary<String, Stat> stats, Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}