using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class NonEvilBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public NonEvilBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool BaseRaceIsAllowed(string baseRace)
        {
            var evilBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Evil);
            var goodBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Good);
            var neutralBaseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Neutral);
            var forbiddenBaseRaceIds = evilBaseRaceIds.Except(neutralBaseRaceIds).Except(goodBaseRaceIds);

            return !forbiddenBaseRaceIds.Contains(baseRace);
        }
    }
}