using System;
using System.Linq;
using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using CharacterGen.Tables;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames
{
    public class HealerClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public HealerClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var healers = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups,
                GroupConstants.Healers);
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups,
                alignment.ToString());

            var allowedClasses = healers.Intersect(alignmentClasses);
            return allowedClasses.Contains(className);
        }
    }
}