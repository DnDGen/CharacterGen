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
                case ClassConstants.Ranger:
                case ClassConstants.Rogue: return true;
                case ClassConstants.Bard: return !alignment.IsLawful();
                case ClassConstants.Monk:
                case ClassConstants.Barbarian:
                case ClassConstants.Sorcerer:
                case ClassConstants.Wizard:
                case ClassConstants.Druid:
                case ClassConstants.Paladin:
                case ClassConstants.Fighter:
                case ClassConstants.Cleric: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}