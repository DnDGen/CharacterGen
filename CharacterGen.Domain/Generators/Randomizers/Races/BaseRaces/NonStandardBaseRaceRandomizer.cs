using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class NonStandardBaseRaceRandomizer : BaseRaceRandomizerBase
    {
        private readonly ICollectionsSelector collectionsSelector;

        public NonStandardBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator, collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool BaseRaceIsAllowedByRandomizer(string baseRace)
        {
            var standardBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Standard);
            return !standardBaseRaces.Contains(baseRace);
        }
    }
}