using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class AnyBaseRaceRandomizer : BaseRaceRandomizer
    {
        public AnyBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator) { }

        protected override bool BaseRaceIsAllowed(string baseRace)
        {
            return true;
        }
    }
}