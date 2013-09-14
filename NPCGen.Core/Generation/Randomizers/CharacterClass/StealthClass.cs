using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Classes;
using System;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public class StealthClass : BaseClassRandomizer
    {
        public StealthClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case ClassConstants.RANGER:
                case ClassConstants.ROGUE: return true;
                case ClassConstants.BARD: return !alignment.IsLawful();
                case ClassConstants.MONK:
                case ClassConstants.BARBARIAN:
                case ClassConstants.SORCERER:
                case ClassConstants.WIZARD:
                case ClassConstants.DRUID:
                case ClassConstants.PALADIN:
                case ClassConstants.FIGHTER:
                case ClassConstants.CLERIC: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}