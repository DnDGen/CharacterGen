using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.ClassNames
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