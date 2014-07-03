﻿using System;
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
        private IAdjustmentsSelector adjustmentsSelector;

        public BaseMetarace(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            var results = GetAllPossibleResults(goodness, characterClass);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}{1}Metaraces", goodness, characterClass.ClassName);
            var metarace = String.Empty;

            do metarace = percentileResultSelector.GetPercentileFrom(tableName);
            while (!results.Contains(metarace));

            return metarace;
        }

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClass characterClass)
        {
            var tableName = String.Format("{0}{1}Metaraces", goodness, characterClass.ClassName);
            var results = percentileResultSelector.GetAllResults(tableName);
            return results.Where(r => RaceIsAllowed(r, characterClass.Level));
        }

        private Boolean RaceIsAllowed(String metarace, Int32 level)
        {
            if (String.IsNullOrEmpty(metarace))
                return allowNoMetarace;

            return LevelAdjustmentIsAllowed(metarace, level) && MetaraceIsAllowed(metarace);
        }

        private Boolean LevelAdjustmentIsAllowed(String metarace, Int32 level)
        {
            var adjustments = adjustmentsSelector.GetAdjustmentsFrom("LevelAdjustments");
            return adjustments[metarace] < level;
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace);
    }
}