using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Providers.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class NonSpellcasterClassNameRandomizer : BaseClassNameRandomizer
    {
        public NonSpellcasterClassNameRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean CharacterClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Rogue: return true;
                case CharacterClassConstants.Monk: return alignment.IsLawful();
                case CharacterClassConstants.Barbarian: return !alignment.IsLawful();
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard:
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Bard:
                case CharacterClassConstants.Druid:
                case CharacterClassConstants.Paladin:
                case CharacterClassConstants.Cleric: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}