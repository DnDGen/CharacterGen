using NPCGen.Core.Characters.Data.Classes;
using NPCGen.Core.Characters.Generation.Factories.Interfaces;
using System;

namespace NPCGen.Core.Characters.Generation.Factories
{
    public class BaseAttackFactory : IBaseAttackFactory
    {
        public BaseAttack Generate(CharacterClass characterClass)
        {
            var baseAttack = new BaseAttack();

            return baseAttack;
        }

        private BaseAttack GetBaseAttack(CharacterClass characterClass)
        {
            var baseAttack = new BaseAttack();
            var rootAttack = GetRootAttackByLevel(characterClass);

            while (rootAttack > 0)
            {
                baseAttack.Attacks.Add(rootAttack);
                rootAttack -= 5;
            }

            return baseAttack;
        }

        private Int32 GetRootAttackByLevel(CharacterClass characterClass)
        {
            switch (characterClass.ClassName)
            {
                case ClassConstants.FIGHTER:
                case ClassConstants.PALADIN:
                case ClassConstants.RANGER:
                case ClassConstants.BARBARIAN: return GetGoodRootAttack(characterClass.Level);
                case ClassConstants.BARD:
                case ClassConstants.CLERIC:
                case ClassConstants.MONK:
                case ClassConstants.ROGUE:
                case ClassConstants.DRUID: return GetAverageRootAttack(characterClass.Level);
                case ClassConstants.SORCERER:
                case ClassConstants.WIZARD: return GetPoorRootAttack(characterClass.Level);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private Int32 GetGoodRootAttack(Int32 level)
        {
            return level;
        }

        private Int32 GetAverageRootAttack(Int32 level)
        {
            var rootAttack = 0;

            for (var i = level; i > 1; i--)
                if ((i - 1) % 4 != 0)
                    rootAttack++;

            return rootAttack;
        }

        private Int32 GetPoorRootAttack(Int32 level)
        {
            return level / 2;
        }
    }
}