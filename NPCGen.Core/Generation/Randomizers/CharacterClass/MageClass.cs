using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Classes;
using System;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public class MageClass : BaseClassRandomizer
    {
        public MageClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String className, Alignment alignment)
        {
            switch (className)
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