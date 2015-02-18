using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class NonEvilForcedMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return true; }
        }

        private ICollectionsSelector collectionsSelector;

        public NonEvilForcedMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var evilMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Evil);
            var neutralMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Neutral);
            var goodMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Good);
            var metaraces = evilMetaraces.Except(neutralMetaraces).Except(goodMetaraces);

            return !metaraces.Contains(metarace);
        }
    }
}