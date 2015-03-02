using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
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
        private INameSelector nameSelector;

        public BaseBaseRace(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector, INameSelector nameSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentSelector = adjustmentSelector;
            this.nameSelector = nameSelector;
        }

        public NameModel Randomize(String goodness, CharacterClass characterClass)
        {
            var results = GetAllPossibleIds(goodness, characterClass);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, goodness, characterClass.ClassName);
            var baseRace = new NameModel();

            do baseRace.Id = percentileResultSelector.SelectFrom(tableName);
            while (!results.Contains(baseRace.Id));

            baseRace.Name = nameSelector.Select(baseRace.Id);

            return baseRace;
        }

        private Boolean RaceIsAllowed(String baseRaceId, Int32 level)
        {
            return !String.IsNullOrEmpty(baseRaceId) && LevelAdjustmentIsAllowed(baseRaceId, level) && BaseRaceIsAllowed(baseRaceId);
        }

        private Boolean LevelAdjustmentIsAllowed(String baseRaceId, Int32 level)
        {
            var levelAdjustments = adjustmentSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            return levelAdjustments[baseRaceId] < level;
        }

        protected abstract Boolean BaseRaceIsAllowed(String baseRaceId);

        public IEnumerable<String> GetAllPossibleIds(String goodness, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces,
                goodness, characterClass.ClassName);
            var baseRaces = percentileResultSelector.SelectAllFrom(tableName);
            return baseRaces.Where(r => RaceIsAllowed(r, characterClass.Level));
        }
    }
}