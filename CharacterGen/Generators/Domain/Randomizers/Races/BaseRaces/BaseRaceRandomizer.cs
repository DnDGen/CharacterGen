using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public abstract class BaseRaceRandomizer : IterativeBuilder, RaceRandomizer
    {
        private IPercentileSelector percentileResultSelector;
        private IAdjustmentsSelector adjustmentSelector;

        public BaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentSelector = adjustmentSelector;
        }

        public String Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var results = GetAllPossible(alignment, characterClass);
            if (results.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.ClassName);

            return Build(() => percentileResultSelector.SelectFrom(tableName),
                b => results.Contains(b));
        }

        private Boolean RaceIsAllowed(String baseRace, Int32 level)
        {
            return !String.IsNullOrEmpty(baseRace) && LevelAdjustmentIsAllowed(baseRace, level) && BaseRaceIsAllowed(baseRace);
        }

        private Boolean LevelAdjustmentIsAllowed(String baseRace, Int32 level)
        {
            var levelAdjustments = adjustmentSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            return levelAdjustments[baseRace] < level;
        }

        protected abstract Boolean BaseRaceIsAllowed(String baseRace);

        public IEnumerable<String> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.ClassName);
            var baseRaces = percentileResultSelector.SelectAllFrom(tableName);
            return baseRaces.Where(r => RaceIsAllowed(r, characterClass.Level));
        }
    }
}