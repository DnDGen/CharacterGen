using System;
using System.Linq;
using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using CharacterGen.Tables;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class EvilBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public EvilBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            var baseRaceIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Evil);
            return baseRaceIds.Contains(baseRace);
        }
    }
}