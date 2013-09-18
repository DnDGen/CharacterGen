using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.ClassNames
{
    public class StealthClassNameRandomizer : BaseClassNameRandomizer
    {
        public StealthClassNameRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean CharacterClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Rogue: return true;
                case CharacterClassConstants.Bard: return !alignment.IsLawful();
                case CharacterClassConstants.Monk:
                case CharacterClassConstants.Barbarian:
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard:
                case CharacterClassConstants.Druid:
                case CharacterClassConstants.Paladin:
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Cleric: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}