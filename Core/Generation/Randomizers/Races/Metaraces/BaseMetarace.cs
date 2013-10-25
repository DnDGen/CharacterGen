using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;

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

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            var results = GetAllPossibleResults(goodness, characterClass);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}{1}Metaraces", goodness, characterClass.ClassName);
            var metarace = String.Empty;

            do metarace = percentileResultProvider.GetPercentileResult(tableName);
            while (!results.Contains(metarace));

            return metarace;
        }

        private Boolean RaceIsAllowed(String metarace, Int32 level)
        {
            if (String.IsNullOrEmpty(metarace))
                return AllowNoMetarace;

            return MetaraceIsAllowed(metarace);
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace);

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClass characterClass)
        {
            var tableName = String.Format("{0}{1}Metaraces", goodness, characterClass.ClassName);
            var results = percentileResultProvider.GetAllResults(tableName);
            return results.Where(r => RaceIsAllowed(r, characterClass.Level));
        }
    }
}