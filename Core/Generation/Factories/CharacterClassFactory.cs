using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class CharacterClassFactory
    {
        public static CharacterClass CreateUsing(Alignment alignment, ILevelRandomizer levelRandomizer, 
            IClassNameRandomizer classNameRandomizer)
        {
            var characterClass = new CharacterClass();

            characterClass.Level = levelRandomizer.Randomize();
            characterClass.ClassName = classNameRandomizer.Randomize(alignment);
            characterClass.BaseAttack = GetBaseAttack(characterClass);

            return characterClass;
        }

        private static BaseAttack GetBaseAttack(CharacterClass characterClass)
        {
            var baseAttack = new BaseAttack();
            baseAttack.BaseAttackBonus = GetBaseAttackBonus(characterClass);
            return baseAttack;
        }

        private static Int32 GetBaseAttackBonus(CharacterClass characterClass)
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

        private static Int32 GetGoodBaseAttackBonus(Int32 level)
        {
            return level;
        }

        private static Int32 GetAverageBaseAttackBonus(Int32 level)
        {
            return level * 3 / 4;
        }

        private static Int32 GetPoorBaseAttackBonus(Int32 level)
        {
            return level / 2;
        }
    }
}