using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal abstract class BaseRaceRandomizerBase : RaceRandomizer
    {
        private IPercentileSelector percentileResultSelector;
        private IAdjustmentsSelector adjustmentSelector;
        private Generator generator;

        protected ICollectionsSelector collectionSelector;

        public BaseRaceRandomizerBase(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector, Generator generator, ICollectionsSelector collectionSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentSelector = adjustmentSelector;
            this.generator = generator;
            this.collectionSelector = collectionSelector;
        }

        public string Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var allowedBaseRaces = GetAllPossible(alignment, characterClass);
            if (allowedBaseRaces.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.Name);

            return generator.Generate(
                () => percentileResultSelector.SelectFrom(tableName),
                b => allowedBaseRaces.Contains(b),
                () => collectionSelector.SelectRandomFrom(allowedBaseRaces),
                $"base race from [{string.Join(",", allowedBaseRaces)}]");
        }

        private bool RaceIsAllowed(string baseRace, Alignment alignment, CharacterClass characterClass)
        {
            return !string.IsNullOrEmpty(baseRace)
                && LevelAdjustmentIsAllowed(baseRace, characterClass.Level)
                && BaseRaceCanBeAlignment(baseRace, alignment)
                && BaseRaceIsAllowed(baseRace);
        }

        private bool BaseRaceCanBeAlignment(string baseRace, Alignment alignment)
        {
            var alignmentRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full);
            return alignmentRaces.Contains(baseRace);
        }

        private bool LevelAdjustmentIsAllowed(string baseRace, int level)
        {
            var levelAdjustment = adjustmentSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, baseRace);

            return levelAdjustment < level;
        }

        protected abstract bool BaseRaceIsAllowed(string baseRace);

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.Name);
            var baseRaces = percentileResultSelector.SelectAllFrom(tableName);

            return baseRaces.Where(r => RaceIsAllowed(r, alignment, characterClass));
        }
    }
}