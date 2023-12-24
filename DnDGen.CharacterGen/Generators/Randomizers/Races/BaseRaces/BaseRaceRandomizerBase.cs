using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Randomizers.Races;
using DnDGen.CharacterGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Randomizers.Races.BaseRaces
{
    internal abstract class BaseRaceRandomizerBase : RaceRandomizer
    {
        private readonly IPercentileSelector percentileSelector;
        private readonly Generator generator;
        private readonly ICollectionSelector collectionSelector;

        public BaseRaceRandomizerBase(IPercentileSelector percentileSelector, Generator generator, ICollectionSelector collectionSelector)
        {
            this.percentileSelector = percentileSelector;
            this.generator = generator;
            this.collectionSelector = collectionSelector;
        }

        public string Randomize(Alignment alignment, CharacterClassPrototype characterClass)
        {
            var allowedBaseRaces = GetAllPossible(alignment, characterClass);
            if (!allowedBaseRaces.Any())
                throw new IncompatibleRandomizersException();

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.Name);

            return generator.Generate(
                () => percentileSelector.SelectFrom(tableName),
                b => allowedBaseRaces.Contains(b),
                () => collectionSelector.SelectRandomFrom(allowedBaseRaces),
                b => $"{b} is not from [{string.Join(", ", allowedBaseRaces)}]",
                $"base race from [{string.Join(",", allowedBaseRaces)}]");
        }

        private bool RaceIsAllowed(string baseRace, Alignment alignment)
        {
            return !string.IsNullOrEmpty(baseRace)
                && BaseRaceCanBeAlignment(baseRace, alignment)
                && BaseRaceIsAllowedByRandomizer(baseRace);
        }

        private bool BaseRaceCanBeAlignment(string baseRace, Alignment alignment)
        {
            var alignmentRaces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full);
            return alignmentRaces.Contains(baseRace);
        }

        protected abstract bool BaseRaceIsAllowedByRandomizer(string baseRace);

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClassPrototype characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.Name);
            var baseRaces = percentileSelector.SelectAllFrom(tableName);

            return baseRaces.Where(r => RaceIsAllowed(r, alignment));
        }
    }
}