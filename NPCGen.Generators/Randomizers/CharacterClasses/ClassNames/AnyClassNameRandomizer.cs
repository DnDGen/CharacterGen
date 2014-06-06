using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames
{
    public class AnyClassNameRandomizer : BaseClassNameRandomizer
    {
        public AnyClassNameRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            switch (className)
            {
                case CharacterClassConstants.Barbarian:
                case CharacterClassConstants.Bard: return !alignment.IsLawful();
                case CharacterClassConstants.Druid: return alignment.IsNeutral();
                case CharacterClassConstants.Monk: return alignment.IsLawful();
                case CharacterClassConstants.Paladin: return alignment.IsLawful() && alignment.IsGood();
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Rogue:
                case CharacterClassConstants.Wizard: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}