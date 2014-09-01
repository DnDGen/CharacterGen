using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class NonGoodForcedMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return true; }
        }

        private ICollectionsSelector collectionsSelector;

        public NonGoodForcedMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var evilMetaraces = collectionsSelector.SelectFrom("MetaraceGroups", AlignmentConstants.Evil);
            var neutralMetaraces = collectionsSelector.SelectFrom("MetaraceGroups", AlignmentConstants.Neutral);
            var goodMetaraces = collectionsSelector.SelectFrom("MetaraceGroups", AlignmentConstants.Good);
            var metaraces = goodMetaraces.Except(neutralMetaraces).Except(evilMetaraces);

            return !metaraces.Contains(metarace);
        }
    }
}