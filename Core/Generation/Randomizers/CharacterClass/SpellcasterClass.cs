using System;
using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Classes;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public class SpellcasterClass : BaseClassRandomizer
    {
        public SpellcasterClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case ClassConstants.Bard: return !alignment.IsLawful();
                case ClassConstants.Druid: return alignment.IsNeutral();
                case ClassConstants.Paladin: return alignment.IsLawful() && alignment.IsGood();
                case ClassConstants.Fighter:
                case ClassConstants.Rogue:
                case ClassConstants.Barbarian:
                case ClassConstants.Monk: return false;
                case ClassConstants.Cleric:
                case ClassConstants.Ranger:
                case ClassConstants.Sorcerer:
                case ClassConstants.Wizard: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}