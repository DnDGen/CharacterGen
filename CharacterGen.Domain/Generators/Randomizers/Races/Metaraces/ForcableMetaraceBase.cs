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
    internal abstract class ForcableMetaraceBase : IForcableMetaraceRandomizer
    {
        public bool ForceMetarace { get; set; }

        internal ICollectionsSelector collectionSelector;

        private IPercentileSelector percentileResultSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private Generator generator;

        public ForcableMetaraceBase(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentsSelector, Generator generator, ICollectionsSelector collectionSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.generator = generator;
            this.collectionSelector = collectionSelector;
        }

        public string Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var allowedMetaraces = GetAllPossible(alignment, characterClass);
            if (allowedMetaraces.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.Name);

            return generator.Generate(
                () => percentileResultSelector.SelectFrom(tableName),
                m => allowedMetaraces.Contains(m),
                () => GetDefaultMetarace(allowedMetaraces));
        }

        private string GetDefaultMetarace(IEnumerable<string> allowedMetaraces)
        {
            if (allowedMetaraces.Contains(RaceConstants.Metaraces.None))
                return RaceConstants.Metaraces.None;

            return collectionSelector.SelectRandomFrom(allowedMetaraces);
        }

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.Name);
            var metaraces = percentileResultSelector.SelectAllFrom(tableName);
            return metaraces.Where(r => RaceIsAllowed(r, alignment, characterClass));
        }

        private bool RaceIsAllowed(string metarace, Alignment alignment, CharacterClass characterClass)
        {
            if (metarace == RaceConstants.Metaraces.None)
                return !ForceMetarace;

            return LevelAdjustmentIsAllowed(metarace, characterClass.Level)
                && MetaraceCanBeAlignment(metarace, alignment)
                && MetaraceCanBeClass(metarace, characterClass)
                && MetaraceIsAllowed(metarace);
        }

        private bool MetaraceCanBeClass(string metarace, CharacterClass characterClass)
        {
            var classRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.Name);
            return classRaces.Contains(metarace);
        }

        private bool MetaraceCanBeAlignment(string metarace, Alignment alignment)
        {
            var alignmentRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Full);
            return alignmentRaces.Contains(metarace);
        }

        private bool LevelAdjustmentIsAllowed(string metarace, int level)
        {
            var levelAdjustment = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, metarace);
            return levelAdjustment < level;
        }

        protected abstract bool MetaraceIsAllowed(string metarace);
    }
}