using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public abstract class BaseForcableMetarace : IterativeGenerator, IForcableMetaraceRandomizer
    {
        public Boolean ForceMetarace { get; set; }

        private IPercentileSelector percentileResultSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private Generator generator;

        public BaseForcableMetarace(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentsSelector, Generator generator)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.generator = generator;
        }

        public String Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var results = GetAllPossible(alignment, characterClass);
            if (results.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.ClassName);

            return generator.Generate(() => percentileResultSelector.SelectFrom(tableName),
                m => results.Contains(m));
        }

        public IEnumerable<String> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.ClassName);
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