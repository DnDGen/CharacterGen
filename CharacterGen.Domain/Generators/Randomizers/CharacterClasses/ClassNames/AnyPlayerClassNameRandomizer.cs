using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.CharacterClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal class AnyPlayerClassNameRandomizer : BaseClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public AnyPlayerClassNameRandomizer(IPercentileSelector percentileResultSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool CharacterClassIsAllowed(string className, Alignment alignment)
        {
            var players = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Players);
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.ToString());
            var allowedClasses = players.Intersect(alignmentClasses);

            return allowedClasses.Contains(className);
        }
    }
}