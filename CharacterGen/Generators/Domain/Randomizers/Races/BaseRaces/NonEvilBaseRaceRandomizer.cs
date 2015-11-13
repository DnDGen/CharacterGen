using System;
using System.Linq;
using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using CharacterGen.Tables;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class NonEvilBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public NonEvilBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            var evilBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Evil);
            var goodBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Good);
            var neutralBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Neutral);
            var forbiddenBaseRaceIds = evilBaseRaceIds.Except(neutralBaseRaceIds).Except(goodBaseRaceIds);

            return !forbiddenBaseRaceIds.Contains(baseRace);
        }
    }
}