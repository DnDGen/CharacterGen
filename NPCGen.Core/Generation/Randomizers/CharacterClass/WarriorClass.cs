using System;
using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Classes;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public class WarriorClass : BaseClassRandomizer
    {
        public WarriorClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case ClassConstants.BARBARIAN: return !alignment.IsLawful();
                case ClassConstants.MONK: return alignment.IsLawful();
                case ClassConstants.PALADIN: return alignment.IsLawful() && alignment.IsGood();
                case ClassConstants.FIGHTER:
                case ClassConstants.RANGER: return true;
                case ClassConstants.CLERIC:
                case ClassConstants.SORCERER:
                case ClassConstants.BARD:
                case ClassConstants.DRUID:
                case ClassConstants.ROGUE:
                case ClassConstants.WIZARD: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}