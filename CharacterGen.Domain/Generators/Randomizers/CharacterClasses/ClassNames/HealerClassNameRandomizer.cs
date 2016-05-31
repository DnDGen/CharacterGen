using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal class HealerClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public HealerClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool CharacterClassIsAllowed(string className, Alignment alignment)
        {
            var healers = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Healers);
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.ToString());
            var allowedClasses = healers.Intersect(alignmentClasses);

            return allowedClasses.Contains(className);
        }
    }
}