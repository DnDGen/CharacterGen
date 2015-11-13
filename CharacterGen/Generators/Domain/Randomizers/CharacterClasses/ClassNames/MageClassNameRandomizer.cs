using System;
using System.Linq;
using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using CharacterGen.Tables;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames
{
    public class MageClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public MageClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var mages = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups,
                GroupConstants.Mages);
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups,
                alignment.ToString());

            var allowedClasses = mages.Intersect(alignmentClasses);
            return allowedClasses.Contains(className);
        }
    }
}