using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using System.Collections.Generic;
using System.Linq;

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

        public string Randomize(Alignment alignment)
        {
            var possibleClassNames = GetAllPossibleResults(alignment);
            if (possibleClassNames.Any() == false)
                throw new IncompatibleRandomizersException();

            var tableName = string.Format("{0}CharacterClasses", alignment.Goodness);

            return generator.Generate(() => percentileResultSelector.SelectFrom(tableName),
                c => possibleClassNames.Contains(c));
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