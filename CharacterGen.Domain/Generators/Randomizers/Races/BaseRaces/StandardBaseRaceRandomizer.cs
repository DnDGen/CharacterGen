using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class StandardBaseRaceRandomizer : BaseRaceRandomizerBase
    {
        public StandardBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector, ICollectionsSelector collectionSelector, Generator generator)
            : base(percentileResultSelector, adjustmentSelector, generator, collectionSelector)
        { }

        protected override bool BaseRaceIsAllowedByRandomizer(string baseRace)
        {
            var standardBaseRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Standard);
            return standardBaseRaces.Contains(baseRace);
        }
    }
}