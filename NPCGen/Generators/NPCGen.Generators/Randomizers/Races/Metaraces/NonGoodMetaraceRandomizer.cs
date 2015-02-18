using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class NonGoodMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean forceMetarace
        {
            get { return false; }
        }

        private ICollectionsSelector collectionsSelector;

        public NonGoodMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentsSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var evilMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Evil);
            var neutralMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Neutral);
            var goodMetaraces = collectionsSelector.SelectFrom(INVALID"MetaraceGroups", AlignmentConstants.Good);
            var metaraces = goodMetaraces.Except(neutralMetaraces).Except(evilMetaraces);

            return !metaraces.Contains(metarace);
        }
    }
}