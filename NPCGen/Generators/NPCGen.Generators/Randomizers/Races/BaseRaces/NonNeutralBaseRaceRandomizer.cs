using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class NonNeutralBaseRaceRandomizer : BaseBaseRace
    {
        private ICollectionsSelector collectionsSelector;

        public NonNeutralBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            var evilBaseRaces = collectionsSelector.SelectFrom(INVALID"BaseRaceGroups", AlignmentConstants.Evil);
            var goodBaseRaces = collectionsSelector.SelectFrom(INVALID"BaseRaceGroups", AlignmentConstants.Good);
            var neutralBaseRaces = collectionsSelector.SelectFrom(INVALID"BaseRaceGroups", AlignmentConstants.Neutral);
            var baseRaces = neutralBaseRaces.Except(goodBaseRaces).Except(evilBaseRaces);

            return !baseRaces.Contains(baseRace);
        }
    }
}