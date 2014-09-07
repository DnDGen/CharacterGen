using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class StealthClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public StealthClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var stealthClasses = collectionsSelector.SelectFrom("ClassNameGroups", "Stealth");
            var alignmentClasses = collectionsSelector.SelectFrom("ClassNameGroups", alignment.ToString());

            var classes = stealthClasses.Intersect(alignmentClasses);
            return classes.Contains(className);
        }
    }
}