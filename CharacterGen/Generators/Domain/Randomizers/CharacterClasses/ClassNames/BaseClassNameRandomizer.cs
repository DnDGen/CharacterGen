using System;
using System.Collections.Generic;
using System.Linq;
using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames
{
    public abstract class BaseClassNameRandomizer : IClassNameRandomizer
    {
        private IPercentileSelector percentileResultSelector;
        private Generator generator;

        public BaseClassNameRandomizer(IPercentileSelector percentileResultSelector, Generator generator)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.generator = generator;
        }

        public String Randomize(Alignment alignment)
        {
            var possibleClassNames = GetAllPossibleResults(alignment);
            if (possibleClassNames.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}CharacterClasses", alignment.Goodness);

            return generator.Generate(() => percentileResultSelector.SelectFrom(tableName),
                c => possibleClassNames.Contains(c));
        }

        protected abstract Boolean CharacterClassIsAllowed(String className, Alignment alignment);

        public IEnumerable<String> GetAllPossibleResults(Alignment alignment)
        {
            var tableName = String.Format("{0}CharacterClasses", alignment.Goodness);
            var classNames = percentileResultSelector.SelectAllFrom(tableName);
            return classNames.Where(c => CharacterClassIsAllowed(c, alignment));
        }
    }
}