using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;

namespace NPCGen.Core.Generation.Randomizers.Races.BaseRaces
{
    public abstract class BaseBaseRace : IBaseRaceRandomizer
    {
        private IPercentileResultProvider percentileResultProvider;

        public BaseBaseRace(IPercentileResultProvider percentileResultProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
        }

        public String Randomize(String goodness, String className)
        {
            var results = GetAllPossibleResults(goodness, className);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}{1}BaseRaces", goodness, className);
            var baseRace = String.Empty;

            do baseRace = percentileResultProvider.GetPercentileResult(tableName);
            while (!results.Contains(baseRace));

            return baseRace;
        }

        private Boolean RaceIsAllowed(String baseRace)
        {
            return !String.IsNullOrEmpty(baseRace) && BaseRaceIsAllowed(baseRace);
        }

        protected abstract Boolean BaseRaceIsAllowed(String baseRace);

        public IEnumerable<String> GetAllPossibleResults(String goodness, String className)
        {
            var tableName = String.Format("{0}{1}BaseRaces", goodness, className);
            var baseRaces = percentileResultProvider.GetAllResults(tableName);
            return baseRaces.Where(r => RaceIsAllowed(r));
        }
    }
}