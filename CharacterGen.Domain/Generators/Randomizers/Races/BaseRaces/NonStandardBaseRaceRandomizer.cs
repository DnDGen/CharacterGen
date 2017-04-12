using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class NonStandardBaseRaceRandomizer : BaseRaceRandomizerBase
    {
        public NonStandardBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, ICollectionsSelector collectionSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator, collectionSelector)
        { }

        protected override bool BaseRaceIsAllowedByRandomizer(string baseRace)
        {
            var standardBaseRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Standard);
            return !standardBaseRaces.Contains(baseRace);
        }
    }
}