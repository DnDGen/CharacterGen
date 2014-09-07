using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class HealerClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public HealerClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var healers = collectionsSelector.SelectFrom("ClassNameGroups", "Healers");
            var alignmentClasses = collectionsSelector.SelectFrom("ClassNameGroups", alignment.ToString());

            var classes = healers.Intersect(alignmentClasses);
            return classes.Contains(className);
        }
    }
}