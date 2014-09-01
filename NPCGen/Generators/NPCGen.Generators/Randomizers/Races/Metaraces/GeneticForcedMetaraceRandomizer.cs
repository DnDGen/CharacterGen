using System;
using System.Linq;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class GeneticForcedMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return true; }
        }

        private ICollectionsSelector collectionsSelector;

        public GeneticForcedMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var metaraces = collectionsSelector.SelectFrom("MetaraceGroups", "Genetic");
            return metaraces.Contains(metarace);
        }
    }
}