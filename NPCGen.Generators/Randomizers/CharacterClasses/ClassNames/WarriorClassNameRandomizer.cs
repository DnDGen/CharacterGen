using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class WarriorClassNameRandomizer : BaseClassNameRandomizer
    {
        public WarriorClassNameRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean CharacterClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case CharacterClassConstants.Barbarian: return !alignment.IsLawful();
                case CharacterClassConstants.Monk: return alignment.IsLawful();
                case CharacterClassConstants.Paladin: return alignment.IsLawful() && alignment.IsGood();
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Ranger: return true;
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Bard:
                case CharacterClassConstants.Druid:
                case CharacterClassConstants.Rogue:
                case CharacterClassConstants.Wizard: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}