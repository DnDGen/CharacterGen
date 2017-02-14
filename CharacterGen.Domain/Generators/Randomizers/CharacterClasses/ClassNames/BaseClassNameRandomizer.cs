using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Verifiers.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal abstract class BaseClassNameRandomizer : IClassNameRandomizer
    {
        private IPercentileSelector percentileResultSelector;
        private Generator generator;
        private ICollectionsSelector collectionsSelector;

        public BaseClassNameRandomizer(IPercentileSelector percentileResultSelector, Generator generator, ICollectionsSelector collectionsSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
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
                () => percentileResultSelector.SelectFrom(tableName),
                c => possibleClassNames.Contains(c),
                () => collectionsSelector.SelectRandomFrom(possibleClassNames));
        }

        protected abstract bool CharacterClassIsAllowed(string className, Alignment alignment);

        public IEnumerable<string> GetAllPossibleResults(Alignment alignment)
        {
            var tableName = string.Format("{0}CharacterClasses", alignment.Goodness);
            var classNames = percentileResultSelector.SelectAllFrom(tableName);
            return classNames.Where(c => CharacterClassIsAllowed(c, alignment));
        }
    }
}