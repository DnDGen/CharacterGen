using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class NonSpellcasterClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public NonSpellcasterClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var classNames = collectionsSelector.SelectFrom("ClassNameGroups", "Spellcasters");
            if (classNames.Contains(className))
                return false;

            switch (className)
            {
                case CharacterClassConstants.Monk: return alignment.IsLawful();
                case CharacterClassConstants.Barbarian: return !alignment.IsLawful();
                default: return true;
            }
        }
    }
}