using NPCGen.Core.Generation.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class AnyBaseRaceRandomizer : BaseBaseRace
    {
        public AnyBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean BaseRaceIsAllowed(String baseRace)
        {
            return true;
        }
    }
}