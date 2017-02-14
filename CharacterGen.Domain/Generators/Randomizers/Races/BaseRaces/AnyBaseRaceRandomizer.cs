using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class AnyBaseRaceRandomizer : BaseRaceRandomizerBase
    {
        public AnyBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, Generator generator, ICollectionsSelector collectionSelector)
            : base(percentileResultSelector, levelAdjustmentSelector, generator, collectionSelector) { }

        protected override bool BaseRaceIsAllowed(string baseRace)
        {
            return true;
        }
    }
}