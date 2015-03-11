using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
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
        private INameSelector nameSelector;

        public BaseForcableMetarace(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentsSelector, INameSelector nameSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.nameSelector = nameSelector;
        }

        public NameModel Randomize(String goodness, CharacterClass characterClass)
        {
            var results = GetAllPossibleIds(goodness, characterClass);
            if (!results.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, goodness, characterClass.ClassName);
            var metarace = new NameModel();

            do metarace.Id = percentileResultSelector.SelectFrom(tableName);
            while (!results.Contains(metarace.Id));

            metarace.Name = nameSelector.Select(metarace.Id);

            return metarace;
        }

        public IEnumerable<String> GetAllPossibleIds(String goodness, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, goodness, characterClass.ClassName);
            var results = percentileResultSelector.SelectAllFrom(tableName);
            return results.Where(r => RaceIsAllowed(r, characterClass.Level));
        }

        private Boolean RaceIsAllowed(String metaraceId, Int32 level)
        {
            if (metaraceId == RaceConstants.Metaraces.NoneId)
                return !ForceMetarace;

            return LevelAdjustmentIsAllowed(metaraceId, level) && MetaraceIsAllowed(metaraceId);
        }

        private Boolean LevelAdjustmentIsAllowed(String metaraceId, Int32 level)
        {
            var adjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            return adjustments[metaraceId] < level;
        }

        protected abstract Boolean MetaraceIsAllowed(String metaraceId);
    }
}