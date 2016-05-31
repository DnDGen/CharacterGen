using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class NonNeutralBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public NonNeutralBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool BaseRaceIsAllowed(string baseRace)
        {
            var evilBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Evil);
            var goodBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Good);
            var neutralBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Neutral);
            var forbiddenBaseRaces = neutralBaseRaces.Except(goodBaseRaces).Except(evilBaseRaces);

            return !forbiddenBaseRaces.Contains(baseRace);
        }
    }
}