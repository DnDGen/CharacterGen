using System;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public abstract class BaseMetarace : IMetaraceRandomizer
    {
        protected Boolean forcedMetarace;

        private IPercentileResultProvider percentileResultProvider;

        public BaseMetarace(IPercentileResultProvider percentileResultProvider)
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

        private Boolean RaceIsAllowed(String metarace)
        {
            return NoMetaraceAllowed(metarace) && MetaraceIsAllowed(metarace);
        }

        private Boolean NoMetaraceAllowed(String metarace)
        {
            return !forcedMetarace || !String.IsNullOrEmpty(metarace);
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace);
    }
}