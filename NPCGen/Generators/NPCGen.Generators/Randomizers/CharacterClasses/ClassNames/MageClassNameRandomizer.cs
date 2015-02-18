using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class MageClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public MageClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var mages = collectionsSelector.SelectFrom(INVALID"ClassNameGroups", "Mages");
            var alignmentClasses = collectionsSelector.SelectFrom(INVALID"ClassNameGroups", alignment.ToString());

            var classes = mages.Intersect(alignmentClasses);
            return classes.Contains(className);
        }
    }
}