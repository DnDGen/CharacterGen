using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class HealerClassNameRandomizer : BaseClassNameRandomizer
    {
        public HealerClassNameRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            switch (className)
            {
                case CharacterClassConstants.Bard: return !alignment.IsLawful();
                case CharacterClassConstants.Druid: return alignment.IsNeutral();
                case CharacterClassConstants.Paladin: return alignment.IsLawful() && alignment.IsGood();
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Rogue:
                case CharacterClassConstants.Barbarian:
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard:
                case CharacterClassConstants.Monk: return false;
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Cleric: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}