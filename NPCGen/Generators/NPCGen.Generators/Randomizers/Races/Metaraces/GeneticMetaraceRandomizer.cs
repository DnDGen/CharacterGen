using System;
using System.Linq;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class GeneticMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return false; }
        }

        private ICollectionsSelector collectionsSelector;

        public GeneticMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
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