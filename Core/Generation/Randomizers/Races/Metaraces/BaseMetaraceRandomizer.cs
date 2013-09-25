using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public abstract class BaseMetaraceRandomizer : IMetaraceRandomizer
    {
        private IPercentileResultProvider percentileResultProvider;

        public BaseMetaraceRandomizer(IPercentileResultProvider percentileResultProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
        }

        public String Randomize(Alignment alignment, String className)
        {
            var tableName = String.Format("{0}{1}Metaraces", alignment.GetGoodnessString(), className);
            var metarace = String.Empty;

            do metarace = percentileResultProvider.GetPercentileResult(tableName);
            while (!RaceIsAllowed(metarace, alignment));

            return metarace;
        }

        protected abstract Boolean RaceIsAllowed(String metarace, Alignment alignment);
    }
}