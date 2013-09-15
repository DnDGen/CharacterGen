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
                case ClassConstants.Barbarian: return !alignment.IsLawful();
                case ClassConstants.Monk: return alignment.IsLawful();
                case ClassConstants.Paladin: return alignment.IsLawful() && alignment.IsGood();
                case ClassConstants.Fighter:
                case ClassConstants.Ranger: return true;
                case ClassConstants.Cleric:
                case ClassConstants.Sorcerer:
                case ClassConstants.Bard:
                case ClassConstants.Druid:
                case ClassConstants.Rogue:
                case ClassConstants.Wizard: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}