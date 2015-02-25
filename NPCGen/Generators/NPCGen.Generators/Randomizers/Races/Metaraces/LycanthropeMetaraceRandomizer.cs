using System;
using System.Linq;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class LycanthropeMetaraceRandomizer : BaseForcableMetarace
    {
        private ICollectionsSelector collectionsSelector;

        public LycanthropeMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var metaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups,
                TableNameConstants.Set.Collection.Groups.Lycanthrope);
            return metaraces.Contains(metarace);
        }
    }
}