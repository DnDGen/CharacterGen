using System;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public abstract class BaseMetaraceRandomizer : IMetaraceRandomizer
    {
        private IPercentileResultProvider percentileResultProvider;

        public BaseMetaraceRandomizer(IPercentileResultProvider percentileResultProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
        }

        public String Randomize(String goodnessString, String className)
        {
            var tableName = String.Format("{0}{1}Metaraces", goodnessString, className);
            var metarace = String.Empty;

            do metarace = percentileResultProvider.GetPercentileResult(tableName);
            while (!RaceIsAllowed(metarace));

            return metarace;
        }

        protected abstract Boolean RaceIsAllowed(String metarace);
    }
}