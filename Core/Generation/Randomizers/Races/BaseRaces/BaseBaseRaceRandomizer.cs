using System;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public abstract class BaseBaseRaceRandomizer : IBaseRaceRandomizer
    {
        private IPercentileResultProvider percentileResultProvider;

        public BaseBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
        }

        public String Randomize(String goodnessString, String className)
        {
            var tableName = String.Format("{0}{1}BaseRaces", goodnessString, className);
            var baseRace = String.Empty;

            do baseRace = percentileResultProvider.GetPercentileResult(tableName);
            while (!RaceIsAllowed(baseRace));

            return baseRace;
        }

        private Boolean RaceIsAllowed(String baseRace)
        {
            return !String.IsNullOrEmpty(baseRace) && BaseRaceIsAllowed(baseRace);
        }

        protected abstract Boolean BaseRaceIsAllowed(String baseRace);
    }
}