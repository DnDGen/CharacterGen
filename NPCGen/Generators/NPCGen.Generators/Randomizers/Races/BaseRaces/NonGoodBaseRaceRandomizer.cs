using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class NonGoodBaseRaceRandomizer : BaseBaseRace
    {
        private ICollectionsSelector collectionsSelector;

        public NonGoodBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            var evilBaseRaces = collectionsSelector.SelectFrom("BaseRaceGroups", AlignmentConstants.Evil);
            var goodBaseRaces = collectionsSelector.SelectFrom("BaseRaceGroups", AlignmentConstants.Good);
            var neutralBaseRaces = collectionsSelector.SelectFrom("BaseRaceGroups", AlignmentConstants.Neutral);
            var baseRaces = goodBaseRaces.Except(neutralBaseRaces).Except(evilBaseRaces);

            return !baseRaces.Contains(baseRace);
        }
    }
}