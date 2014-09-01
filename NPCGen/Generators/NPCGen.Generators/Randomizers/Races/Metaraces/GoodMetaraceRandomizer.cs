using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class GoodMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return false; }
        }

        private ICollectionsSelector collectionsSelector;

        public GoodMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var metaraces = collectionsSelector.SelectFrom("MetaraceGroups", AlignmentConstants.Good);
            return metaraces.Contains(metarace);
        }
    }
}