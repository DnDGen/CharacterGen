using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class GoodMetaraceRandomizer : BaseForcableMetarace
    {
        private ICollectionsSelector collectionsSelector;

        public GoodMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            var metaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Good);
            return metaraces.Contains(metarace);
        }
    }
}