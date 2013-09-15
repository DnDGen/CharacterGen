using System;
using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Classes;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public class HealerClass : BaseClassRandomizer
    {
        public HealerClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String className, Alignment alignment)
        {
            switch (className)
            {
                case ClassConstants.Bard: return !alignment.IsLawful();
                case ClassConstants.Druid: return alignment.IsNeutral();
                case ClassConstants.Paladin: return alignment.IsLawful() && alignment.IsGood();
                case ClassConstants.Fighter:
                case ClassConstants.Rogue:
                case ClassConstants.Barbarian:
                case ClassConstants.Sorcerer:
                case ClassConstants.Wizard:
                case ClassConstants.Monk: return false;
                case ClassConstants.Ranger:
                case ClassConstants.Cleric: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}