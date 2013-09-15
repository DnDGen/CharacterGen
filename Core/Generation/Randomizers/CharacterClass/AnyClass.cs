using System;
using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Classes;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public class AnyClass : BaseClassRandomizer
    {
        public AnyClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String className, Alignment alignment)
        {
            switch (className)
            {
                case ClassConstants.Barbarian:
                case ClassConstants.Bard: return !alignment.IsLawful();
                case ClassConstants.Druid: return alignment.IsNeutral();
                case ClassConstants.Monk: return alignment.IsLawful();
                case ClassConstants.Paladin: return alignment.IsLawful() && alignment.IsGood();
                case ClassConstants.Fighter:
                case ClassConstants.Cleric:
                case ClassConstants.Ranger:
                case ClassConstants.Sorcerer:
                case ClassConstants.Rogue:
                case ClassConstants.Wizard: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}