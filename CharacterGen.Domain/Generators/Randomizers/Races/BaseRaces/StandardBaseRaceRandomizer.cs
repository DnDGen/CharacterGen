using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class StandardBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public StandardBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool BaseRaceIsAllowed(string baseRace)
        {
            var baseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Standard);
            return baseRaces.Contains(baseRace);
        }
    }
}