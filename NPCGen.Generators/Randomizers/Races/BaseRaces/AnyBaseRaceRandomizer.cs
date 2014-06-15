using System;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public class AnyBaseRaceRandomizer : BaseBaseRace
    {
        public AnyBaseRaceRandomizer(IPercentileSelector percentileResultProvider, ILevelAdjustmentsSelector levelAdjustmentProvider)
            : base(percentileResultProvider, levelAdjustmentProvider) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            return true;
        }
    }
}