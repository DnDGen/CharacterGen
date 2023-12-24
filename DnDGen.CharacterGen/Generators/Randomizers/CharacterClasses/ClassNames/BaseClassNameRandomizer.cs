using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal abstract class BaseClassNameRandomizer : IClassNameRandomizer
    {
        private readonly IPercentileSelector percentileSelector;
        private readonly Generator generator;
        private readonly ICollectionSelector collectionsSelector;

        public BaseClassNameRandomizer(IPercentileSelector percentileSelector, Generator generator, ICollectionSelector collectionsSelector)
        {
            this.percentileSelector = percentileSelector;
            this.generator = generator;
            this.collectionsSelector = collectionsSelector;
        }

        public string Randomize(Alignment alignment)
        {
            var possibleClassNames = GetAllPossibleResults(alignment);
            if (possibleClassNames.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, alignment.Goodness);

            return generator.Generate(
                () => percentileSelector.SelectFrom(tableName),
                c => possibleClassNames.Contains(c),
                () => collectionsSelector.SelectRandomFrom(possibleClassNames),
                c => $"{c} is not from [{string.Join(",", possibleClassNames)}]",
                $"class name from [{string.Join(",", possibleClassNames)}]");
        }

        protected abstract bool CharacterClassIsAllowed(string className, Alignment alignment);

        public IEnumerable<string> GetAllPossibleResults(Alignment alignment)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, alignment.Goodness);
            var classNames = percentileSelector.SelectAllFrom(tableName);
            return classNames.Where(c => CharacterClassIsAllowed(c, alignment));
        }
    }
}