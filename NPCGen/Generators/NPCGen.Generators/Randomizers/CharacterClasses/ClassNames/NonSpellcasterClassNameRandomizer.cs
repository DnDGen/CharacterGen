using System;
using System.Linq;
using NPCGen.Common.Alignments;
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
            var spellcasters = collectionsSelector.SelectFrom(INVALID"ClassNameGroups", "Spellcasters");
            var alignmentClasses = collectionsSelector.SelectFrom(INVALID"ClassNameGroups", alignment.ToString());

            return alignmentClasses.Contains(className) && !spellcasters.Contains(className);
        }
    }
}