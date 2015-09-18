using System;
using CharacterGen.Selectors;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class AnyBaseRaceRandomizer : BaseRaceRandomizer
    {
        public AnyBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector)
            : base(percentileResultSelector, levelAdjustmentSelector) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            return true;
        }
    }
}