using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.ClassNames
{
    public class NonSpellcasterClass : BaseClassRandomizer
    {
        public NonSpellcasterClass(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

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