using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames
{
    public class SpellcasterClassNameRandomizer : BaseClassNameRandomizer
    {
        public SpellcasterClassNameRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean CharacterClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case CharacterClassConstants.Bard: return !alignment.IsLawful();
                case CharacterClassConstants.Druid: return alignment.IsNeutral();
                case CharacterClassConstants.Paladin: return alignment.IsLawful() && alignment.IsGood();
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Rogue:
                case CharacterClassConstants.Barbarian:
                case CharacterClassConstants.Monk: return false;
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}