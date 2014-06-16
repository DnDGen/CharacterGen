using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class MageClassNameRandomizer : BaseClassNameRandomizer
    {
        public MageClassNameRandomizer(IPercentileSelector percentileResultSelector) : base(percentileResultSelector) { }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            switch (className)
            {
                case CharacterClassConstants.Bard: return !alignment.IsLawful();
                case CharacterClassConstants.Druid:
                case CharacterClassConstants.Paladin:
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Rogue:
                case CharacterClassConstants.Barbarian:
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Monk: return false;
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}