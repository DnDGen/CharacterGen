using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.BaseRaces
{
    public abstract class BaseBaseRace : IBaseRaceRandomizer
    {
        private IPercentileSelector percentileResultSelector;
        private IAdjustmentsSelector adjustmentSelector;

        public BaseBaseRace(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentSelector = adjustmentSelector;
        }

        public String Randomize(String goodness, CharacterClass characterClass)
        {
            var results = GetAllPossibleResults(goodness, characterClass);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}{1}BaseRaces", goodness, characterClass.ClassName);
            var baseRace = String.Empty;

            do baseRace = percentileResultSelector.SelectFrom(tableName);
            while (!results.Contains(baseRace));

            return baseRace;
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

        public IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces,
                goodness, characterClass.ClassName);
            var baseRaces = percentileResultSelector.SelectAllFrom(tableName);
            return baseRaces.Where(r => RaceIsAllowed(r, characterClass.Level));
        }
    }
}