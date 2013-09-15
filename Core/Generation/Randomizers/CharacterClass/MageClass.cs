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
                case ClassConstants.Bard: return !alignment.IsLawful();
                case ClassConstants.Druid:
                case ClassConstants.Paladin:
                case ClassConstants.Fighter:
                case ClassConstants.Rogue:
                case ClassConstants.Barbarian:
                case ClassConstants.Cleric:
                case ClassConstants.Monk: return false;
                case ClassConstants.Ranger:
                case ClassConstants.Sorcerer:
                case ClassConstants.Wizard: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}