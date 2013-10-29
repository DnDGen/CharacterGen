using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class CharacterClassFactory
    {
        public static CharacterClassPrototype CreatePrototypeUsing(Alignment alignment, ILevelRandomizer levelRandomizer, 
            IClassNameRandomizer classNameRandomizer)
        {
            var prototype = new CharacterClassPrototype();

            prototype.Level = levelRandomizer.Randomize();
            prototype.ClassName = classNameRandomizer.Randomize(alignment);

            return prototype;
        }

        public static CharacterClass CreateUsing(CharacterClassPrototype prototype)
        {
            var characterClass = new CharacterClass();

            characterClass.ClassName = prototype.ClassName;
            characterClass.Level = prototype.Level;
            characterClass.BaseAttack = GetBaseAttack(prototype);

            return characterClass;
        }

        private static BaseAttack GetBaseAttack(CharacterClassPrototype prototype)
        {
            var baseAttack = new BaseAttack();
            baseAttack.BaseAttackBonus = GetBaseAttackBonus(prototype);
            return baseAttack;
        }

        private static Int32 GetBaseAttackBonus(CharacterClassPrototype prototype)
        {
            switch (prototype.ClassName)
            {
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Paladin:
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Barbarian: return GetGoodBaseAttackBonus(prototype.Level);
                case CharacterClassConstants.Bard:
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Monk:
                case CharacterClassConstants.Rogue:
                case CharacterClassConstants.Druid: return GetAverageBaseAttackBonus(prototype.Level);
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard: return GetPoorBaseAttackBonus(prototype.Level);
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