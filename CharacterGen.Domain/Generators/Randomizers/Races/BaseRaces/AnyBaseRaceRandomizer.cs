using CharacterGen.Domain.Selectors.Collections;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class AnyBaseRaceRandomizer : BaseRaceRandomizerBase
    {
        public AnyBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, Generator generator, ICollectionsSelector collectionSelector)
            : base(percentileResultSelector, levelAdjustmentSelector, generator, collectionSelector) { }

        protected override bool BaseRaceIsAllowedByRandomizer(string baseRace)
        {
            return true;
        }
    }
}