using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames
{
    public class AnyPlayerClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public AnyPlayerClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool CharacterClassIsAllowed(String className, Alignment alignment)
        {
            var players = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Players);
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.ToString());
            var allowedClasses = players.Intersect(alignmentClasses);

            return allowedClasses.Contains(className);
        }
    }
}