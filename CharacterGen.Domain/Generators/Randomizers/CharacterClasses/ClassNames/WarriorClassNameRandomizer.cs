using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal class WarriorClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public WarriorClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool CharacterClassIsAllowed(string className, Alignment alignment)
        {
            var warriors = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Warriors);
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.ToString());

            var allowedClasses = warriors.Intersect(alignmentClasses);
            return allowedClasses.Contains(className);
        }
    }
}