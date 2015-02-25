using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public abstract class BaseForcableMetarace : IForcableMetaraceRandomizer
    {
        public Boolean ForceMetarace { get; set; }

        private IPercentileSelector percentileResultSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public BaseForcableMetarace(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            var results = GetAllPossibleResults(goodness, characterClass);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces,
                goodness, characterClass.ClassName);
            var metarace = String.Empty;

            do metarace = percentileResultSelector.SelectFrom(tableName);
            while (!results.Contains(metarace));

            return metarace;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces,
                goodness, characterClass.ClassName);
            var results = percentileResultSelector.SelectAllFrom(tableName);
            return results.Where(r => RaceIsAllowed(r, characterClass.Level));
        }

        private Boolean RaceIsAllowed(String metarace, Int32 level)
        {
            if (String.IsNullOrEmpty(metarace))
                return !ForceMetarace;

            return LevelAdjustmentIsAllowed(metarace, level) && MetaraceIsAllowed(metarace);
        }

        private Boolean LevelAdjustmentIsAllowed(String metarace, Int32 level)
        {
            var adjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            return adjustments[metarace] < level;
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace);
    }
}