using NPCGen.Common.Races;
using System;
using NPCGen.Selectors.Interfaces;
using System.Linq;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class LycanthropeForcedMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return true; }
        }

        private ICollectionsSelector collectionsSelector;

        public LycanthropeForcedMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var metaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", "Lycanthrope");
            return metaraces.Contains(metarace);
        }
    }
}