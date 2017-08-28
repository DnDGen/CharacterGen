using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal abstract class ForcableMetaraceBase : IForcableMetaraceRandomizer
    {
        public bool ForceMetarace { get; set; }

        private readonly ICollectionsSelector collectionSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly Generator generator;

        public ForcableMetaraceBase(IPercentileSelector percentileSelector, Generator generator, ICollectionsSelector collectionSelector)
        {
            this.percentileSelector = percentileSelector;
            this.generator = generator;
            this.collectionSelector = collectionSelector;
        }

        public string Randomize(Alignment alignment, CharacterClassPrototype characterClass)
        {
            var allowedMetaraces = GetAllPossible(alignment, characterClass);
            if (!allowedMetaraces.Any())
                throw new IncompatibleRandomizersException();

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.Name);

            return generator.Generate(
                () => percentileSelector.SelectFrom(tableName),
                m => allowedMetaraces.Contains(m),
                () => GetDefaultMetarace(allowedMetaraces),
                m => $"{m} is not from [{string.Join(",", allowedMetaraces)}]",
                $"metarace from [{string.Join(",", allowedMetaraces)}]");
        }

        private string GetDefaultMetarace(IEnumerable<string> allowedMetaraces)
        {
            if (allowedMetaraces.Contains(RaceConstants.Metaraces.None))
                return RaceConstants.Metaraces.None;

            return collectionSelector.SelectRandomFrom(allowedMetaraces);
        }

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClassPrototype characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.Name);
            var metaraces = percentileSelector.SelectAllFrom(tableName);
            return metaraces.Where(r => RaceIsAllowed(r, alignment, characterClass));
        }

        private bool RaceIsAllowed(string metarace, Alignment alignment, CharacterClassPrototype characterClass)
        {
            if (metarace == RaceConstants.Metaraces.None)
                return !ForceMetarace;

            return MetaraceCanBeAlignment(metarace, alignment)
                && MetaraceCanBeClass(metarace, characterClass)
                && MetaraceIsAllowed(metarace);
        }

        private bool MetaraceCanBeClass(string metarace, CharacterClassPrototype characterClass)
        {
            var classRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.Name);
            return classRaces.Contains(metarace);
        }

        private bool MetaraceCanBeAlignment(string metarace, Alignment alignment)
        {
            var alignmentRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Full);
            return alignmentRaces.Contains(metarace);
        }

        protected abstract bool MetaraceIsAllowed(string metarace);
    }
}