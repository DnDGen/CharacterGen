using D20Dice.Dice;
using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Data.Classes;
using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.CharacterClass
{
    public class HealerClass : BaseClassRandomizer
    {
        public HealerClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case ClassConstants.BARD: return !alignment.IsLawful();
                case ClassConstants.DRUID: return alignment.IsNeutral();
                case ClassConstants.PALADIN: return alignment.IsLawful();
                case ClassConstants.FIGHTER:
                case ClassConstants.ROGUE:
                case ClassConstants.BARBARIAN:
                case ClassConstants.SORCERER:
                case ClassConstants.WIZARD:
                case ClassConstants.MONK: return false;
                case ClassConstants.RANGER:
                case ClassConstants.CLERIC: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}