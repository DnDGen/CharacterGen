using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal abstract class BaseRaceRandomizer : RaceRandomizer
    {
        private IPercentileSelector percentileResultSelector;
        private IAdjustmentsSelector adjustmentSelector;
        private Generator generator;

        public BaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector, Generator generator)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentSelector = adjustmentSelector;
            this.generator = generator;
        }

        public string Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var results = GetAllPossible(alignment, characterClass);
            if (results.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.Name);

            return generator.Generate(() => percentileResultSelector.SelectFrom(tableName),
                b => results.Contains(b));
        }

        private bool RaceIsAllowed(string baseRace, int level)
        {
            return !string.IsNullOrEmpty(baseRace) && LevelAdjustmentIsAllowed(baseRace, level) && BaseRaceIsAllowed(baseRace);
        }

        private bool LevelAdjustmentIsAllowed(string baseRace, int level)
        {
            var levelAdjustments = adjustmentSelector.SelectAllFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);

            if (!levelAdjustments.ContainsKey(baseRace))
                throw new ArgumentException($"No level adjustment entry exists for {baseRace}");

            return levelAdjustments[baseRace] < level;
        }

        protected abstract bool BaseRaceIsAllowed(string baseRace);

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.Name);
            var baseRaces = percentileResultSelector.SelectAllFrom(tableName);

            return baseRaces.Where(r => RaceIsAllowed(r, characterClass.Level));
        }
    }
}