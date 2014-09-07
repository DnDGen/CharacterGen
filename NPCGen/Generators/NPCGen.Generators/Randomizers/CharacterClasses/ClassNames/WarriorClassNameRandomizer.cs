using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public class WarriorClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public WarriorClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var warriors = collectionsSelector.SelectFrom("ClassNameGroups", "Warriors");
            var alignmentClasses = collectionsSelector.SelectFrom("ClassNameGroups", alignment.ToString());

            var classes = warriors.Intersect(alignmentClasses);
            return classes.Contains(className);
        }
    }
}