using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class NonEvilBaseRaceRandomizer : BaseBaseRace
    {
        private ICollectionsSelector collectionsSelector;

        public NonEvilBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            var evilBaseRaces = collectionsSelector.SelectFrom(INVALID"BaseRaceGroups", AlignmentConstants.Evil);
            var goodBaseRaces = collectionsSelector.SelectFrom(INVALID"BaseRaceGroups", AlignmentConstants.Good);
            var neutralBaseRaces = collectionsSelector.SelectFrom(INVALID"BaseRaceGroups", AlignmentConstants.Neutral);
            var baseRaces = evilBaseRaces.Except(neutralBaseRaces).Except(goodBaseRaces);

            return !baseRaces.Contains(baseRace);
        }
    }
}