using System;
using System.Linq;
using CharacterGen.Selectors;
using CharacterGen.Tables;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class NonStandardBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public NonStandardBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            var baseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Standard);
            return !baseRaces.Contains(baseRace);
        }
    }
}