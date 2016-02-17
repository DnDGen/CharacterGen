using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public abstract class BaseRaceRandomizer : RaceRandomizer
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

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.ClassName);

            return generator.Generate(() => percentileResultSelector.SelectFrom(tableName),
                b => results.Contains(b));
        }

        private bool RaceIsAllowed(string baseRace, int level)
        {
            return !string.IsNullOrEmpty(baseRace) && LevelAdjustmentIsAllowed(baseRace, level) && BaseRaceIsAllowed(baseRace);
        }

        private bool LevelAdjustmentIsAllowed(string baseRace, int level)
        {
            var levelAdjustments = adjustmentSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            return levelAdjustments[baseRace] < level;
        }

        protected abstract bool BaseRaceIsAllowed(string baseRace);

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.ClassName);
            var baseRaces = percentileResultSelector.SelectAllFrom(tableName);

            return baseRaces.Where(r => RaceIsAllowed(r, characterClass.Level));
        }
    }
}