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
                case ClassConstants.Fighter:
                case ClassConstants.Rogue: return true;
                case ClassConstants.Monk: return alignment.IsLawful();
                case ClassConstants.Barbarian: return !alignment.IsLawful();
                case ClassConstants.Sorcerer:
                case ClassConstants.Wizard:
                case ClassConstants.Ranger:
                case ClassConstants.Bard:
                case ClassConstants.Druid:
                case ClassConstants.Paladin:
                case ClassConstants.Cleric: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}