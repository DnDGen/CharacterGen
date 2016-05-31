using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class NeutralBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public NeutralBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool BaseRaceIsAllowed(string baseRace)
        {
            var allowedBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Neutral);
            return allowedBaseRaces.Contains(baseRace);
        }
    }
}