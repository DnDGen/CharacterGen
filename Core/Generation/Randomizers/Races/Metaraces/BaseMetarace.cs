using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public abstract class BaseMetarace : IMetaraceRandomizer
    {
        public Boolean AllowNoMetarace { get; set; }

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
            if (String.IsNullOrEmpty(metarace))
                return AllowNoMetarace;

            return MetaraceIsAllowed(metarace);
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace);
    }
}