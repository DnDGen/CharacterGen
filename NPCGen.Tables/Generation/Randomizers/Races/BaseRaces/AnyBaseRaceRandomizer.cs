using System;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class AnyBaseRaceRandomizer : BaseBaseRace
    {
        public AnyBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentProvider)
            : base(percentileResultProvider, levelAdjustmentProvider) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            return true;
        }
    }
}