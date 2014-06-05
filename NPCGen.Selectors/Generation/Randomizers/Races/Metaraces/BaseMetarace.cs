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
        private ILevelAdjustmentsProvider levelAdjustmentsProvider;

        public BaseMetarace(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentsProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
            this.levelAdjustmentsProvider = levelAdjustmentsProvider;
        }

        public String Randomize(String goodness, CharacterClassPrototype prototype)
        {
            var results = GetAllPossibleResults(goodness, prototype);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}{1}Metaraces", goodness, prototype.ClassName);
            var metarace = String.Empty;

            do metarace = percentileResultProvider.GetPercentileResult(tableName);
            while (!results.Contains(metarace));

            return metarace;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClassPrototype prototype)
        {
            var tableName = String.Format("{0}{1}Metaraces", goodness, prototype.ClassName);
            var results = percentileResultProvider.GetAllResults(tableName);
            return results.Where(r => RaceIsAllowed(r, prototype.Level));
        }

        private Boolean RaceIsAllowed(String metarace, Int32 level)
        {
            if (String.IsNullOrEmpty(metarace))
                return AllowNoMetarace;

            return LevelAdjustmentIsAllowed(metarace,level) && MetaraceIsAllowed(metarace);
        }

        private Boolean LevelAdjustmentIsAllowed(String metarace, Int32 level)
        {
            var adjustments = levelAdjustmentsProvider.GetLevelAdjustments();
            return adjustments[metarace] < level;
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace);
    }
}