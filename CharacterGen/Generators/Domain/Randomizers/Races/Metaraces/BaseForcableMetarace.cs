﻿using System;
using System.Collections.Generic;
using System.Linq;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
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
            var results = GetAllPossible(goodness, characterClass);
            if (results.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, goodness, characterClass.ClassName);
            var metarace = String.Empty;

            do metarace = percentileResultSelector.SelectFrom(tableName);
            while (results.Contains(metarace) == false);

            return metarace;
        }

        public IEnumerable<String> GetAllPossible(String goodness, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, goodness, characterClass.ClassName);
            var metaraces = percentileResultSelector.SelectAllFrom(tableName);
            return metaraces.Where(r => RaceIsAllowed(r, characterClass.Level));
        }

        private Boolean RaceIsAllowed(String metarace, Int32 level)
        {
            if (metarace == RaceConstants.Metaraces.None)
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