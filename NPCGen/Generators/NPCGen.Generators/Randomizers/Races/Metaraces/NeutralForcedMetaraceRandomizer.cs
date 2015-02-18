using NPCGen.Common.Races;
using System;
using NPCGen.Selectors.Interfaces;
using System.Linq;
using NPCGen.Common.Alignments;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class NeutralForcedMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return true; }
        }

        private ICollectionsSelector collectionsSelector;

        public NeutralForcedMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var metaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Neutral);
            return metaraces.Contains(metarace);
        }
    }
}