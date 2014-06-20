using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public abstract class BaseMetarace : IMetaraceRandomizer
    {
        protected abstract Boolean allowNoMetarace { get; }

        private IPercentileSelector percentileResultSelector;
        private ILevelAdjustmentsSelector levelAdjustmentsSelector;

        public BaseMetarace(IPercentileSelector percentileResultSelector, ILevelAdjustmentsSelector levelAdjustmentsSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.levelAdjustmentsSelector = levelAdjustmentsSelector;
        }

        public String Randomize(String goodness, CharacterClassPrototype prototype)
        {
            var results = GetAllPossibleResults(goodness, prototype);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}{1}Metaraces", goodness, prototype.ClassName);
            var metarace = String.Empty;

            do metarace = percentileResultSelector.GetPercentileResult(tableName);
            while (!results.Contains(metarace));

            return metarace;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClassPrototype prototype)
        {
            var tableName = String.Format("{0}{1}Metaraces", goodness, prototype.ClassName);
            var results = percentileResultSelector.GetAllResults(tableName);
            return results.Where(r => RaceIsAllowed(r, prototype.Level));
        }

        private Boolean RaceIsAllowed(String metarace, Int32 level)
        {
            if (String.IsNullOrEmpty(metarace))
                return allowNoMetarace;

            return LevelAdjustmentIsAllowed(metarace, level) && MetaraceIsAllowed(metarace);
        }

        private Boolean LevelAdjustmentIsAllowed(String metarace, Int32 level)
        {
            var adjustments = levelAdjustmentsSelector.GetLevelAdjustments();
            return adjustments[metarace] < level;
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace);
    }
}