using System;
using D20Dice.Dice;
using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Data.Classes;

namespace NPCGen.Core.Characters.Generation.Randomizers.CharacterClass
{
    public class AnyClass : BaseClassRandomizer
    {
        public AnyClass(IDice dice) : base(dice) { }

        protected override Boolean ClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case ClassConstants.BARBARIAN:
                case ClassConstants.BARD: return !alignment.IsLawful();
                case ClassConstants.DRUID: return alignment.IsNeutral();
                case ClassConstants.MONK: return alignment.IsLawful();
                case ClassConstants.PALADIN: return alignment.IsLawful() && alignment.IsGood();
                case ClassConstants.FIGHTER:
                case ClassConstants.CLERIC:
                case ClassConstants.RANGER:
                case ClassConstants.SORCERER:
                case ClassConstants.ROGUE:
                case ClassConstants.WIZARD: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}