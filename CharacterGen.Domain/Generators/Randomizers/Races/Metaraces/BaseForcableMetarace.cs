using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal abstract class BaseForcableMetarace : IForcableMetaraceRandomizer
    {
        public bool ForceMetarace { get; set; }

        private IPercentileSelector percentileResultSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private Generator generator;

        public BaseForcableMetarace(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentsSelector, Generator generator)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.generator = generator;
        }

        public string Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var allowedMetaraces = GetAllPossible(alignment, characterClass);
            if (allowedMetaraces.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.Name);

            return generator.Generate(() => percentileResultSelector.SelectFrom(tableName), m => allowedMetaraces.Contains(m));
        }

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.Name);
            var metaraces = percentileResultSelector.SelectAllFrom(tableName);
            return metaraces.Where(r => RaceIsAllowed(r, characterClass.Level));
        }

        private bool RaceIsAllowed(string metarace, int level)
        {
            if (metarace == RaceConstants.Metaraces.None)
                return !ForceMetarace;

            return LevelAdjustmentIsAllowed(metarace, level) && MetaraceIsAllowed(metarace);
        }

        private bool LevelAdjustmentIsAllowed(string metarace, int level)
        {
            var adjustments = adjustmentsSelector.SelectAllFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            return adjustments[metarace] < level;
        }

        protected abstract bool MetaraceIsAllowed(string metarace);
    }
}