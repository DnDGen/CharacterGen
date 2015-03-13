using System;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class EvilBaseRaceRandomizer : BaseBaseRace
    {
        private ICollectionsSelector collectionsSelector;

        public EvilBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, INameSelector nameSelector,
            ICollectionsSelector collectionsSelector)
            : base(percentileResultSelector, levelAdjustmentSelector, nameSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRaceId)
        {
            var baseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Evil);
            return baseRaceIds.Contains(baseRaceId);
        }
    }
}