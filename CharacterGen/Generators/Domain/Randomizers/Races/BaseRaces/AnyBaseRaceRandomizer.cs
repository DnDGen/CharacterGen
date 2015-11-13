using System;
using CharacterGen.Selectors;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class AnyBaseRaceRandomizer : BaseRaceRandomizer
    {
        public AnyBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            return true;
        }
    }
}