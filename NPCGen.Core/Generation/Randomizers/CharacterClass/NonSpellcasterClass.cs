using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Classes;
using System;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public class NonSpellcasterClass : BaseClassRandomizer
    {
        public NonSpellcasterClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case ClassConstants.FIGHTER:
                case ClassConstants.ROGUE: return true;
                case ClassConstants.MONK: return alignment.IsLawful();
                case ClassConstants.BARBARIAN: return !alignment.IsLawful();
                case ClassConstants.SORCERER:
                case ClassConstants.WIZARD:
                case ClassConstants.RANGER:
                case ClassConstants.BARD:
                case ClassConstants.DRUID:
                case ClassConstants.PALADIN:
                case ClassConstants.CLERIC: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}