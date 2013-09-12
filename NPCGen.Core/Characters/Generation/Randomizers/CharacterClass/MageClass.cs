using D20Dice.Dice;
using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Data.Classes;
using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.CharacterClass
{
    public class MageClass : BaseClassRandomizer
    {
        public MageClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case ClassConstants.BARD: return !alignment.IsLawful();
                case ClassConstants.DRUID:
                case ClassConstants.PALADIN:
                case ClassConstants.FIGHTER:
                case ClassConstants.ROGUE:
                case ClassConstants.BARBARIAN:
                case ClassConstants.CLERIC:
                case ClassConstants.MONK: return false;
                case ClassConstants.RANGER:
                case ClassConstants.SORCERER:
                case ClassConstants.WIZARD: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}