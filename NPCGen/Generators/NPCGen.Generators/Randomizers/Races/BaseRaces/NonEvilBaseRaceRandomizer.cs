using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class NonEvilBaseRaceRandomizer : BaseBaseRace
    {
        private ICollectionsSelector collectionsSelector;

        public NonEvilBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, INameSelector nameSelector,
            ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentSelector, nameSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRaceId)
        {
            var evilBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Evil);
            var goodBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Good);
            var neutralBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Neutral);
            var forbiddenBaseRaceIds = evilBaseRaceIds.Except(neutralBaseRaceIds).Except(goodBaseRaceIds);

            return !forbiddenBaseRaceIds.Contains(baseRaceId);
        }
    }
}