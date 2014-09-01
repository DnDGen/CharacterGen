using NPCGen.Common.Races;
using System;
using NPCGen.Selectors.Interfaces;
using System.Linq;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class LycanthropeMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return false; }
        }

        private ICollectionsSelector collectionsSelector;

        public LycanthropeMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var metaraces = collectionsSelector.SelectFrom("MetaraceGroups", "Lycanthrope");
            return metaraces.Contains(metarace);
        }
    }
}