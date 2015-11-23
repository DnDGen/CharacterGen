using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public class UndeadMetaraceRandomizer : BaseForcableMetarace
    {
        private ICollectionsSelector collectionsSelector;

        public UndeadMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var metaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead);
            return metaraces.Contains(metarace);
        }
    }
}
