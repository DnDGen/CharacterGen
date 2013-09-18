using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public class AnyBaseRaceRandomizer : BaseBaseRaceRandomizer
    {
        public AnyBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean RaceIsAllowed(String baseRace, Alignment alignment, String className)
        {
            throw new NotImplementedException();
        }
    }
}