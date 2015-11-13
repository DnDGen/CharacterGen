using System;
using System.Linq;
using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using CharacterGen.Tables;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class NeutralBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public NeutralBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            var allowedBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Neutral);
            return allowedBaseRaces.Contains(baseRace);
        }
    }
}