using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class GeneticMetaraceRandomizer : BaseForcableMetarace
    {
        private ICollectionsSelector collectionsSelector;

        public GeneticMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            var metaraceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Genetic);
            return metaraceIds.Contains(metarace);
        }
    }
}