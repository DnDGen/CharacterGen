using NPCGen.Common.Races;
using System;
using NPCGen.Selectors.Interfaces;
using System.Linq;
using NPCGen.Common.Alignments;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class NonNeutralForcedMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return true; }
        }

        private ICollectionsSelector collectionsSelector;

        public NonNeutralForcedMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var evilMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Evil);
            var neutralMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Neutral);
            var goodMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Good);
            var metaraces = neutralMetaraces.Except(goodMetaraces).Except(evilMetaraces);

            return !metaraces.Contains(metarace);
        }
    }
}