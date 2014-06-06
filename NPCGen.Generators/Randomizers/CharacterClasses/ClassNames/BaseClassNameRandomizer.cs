using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Providers.Interfaces;
using NPCGen.Generators.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Generators.Verifiers.Exceptions;

namespace NPCGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    public abstract class BaseClassNameRandomizer : IClassNameRandomizer
    {
        private IPercentileResultProvider percentileResultProvider;

        public BaseClassNameRandomizer(IPercentileResultProvider percentileResultProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
        }

        public String Randomize(Alignment alignment)
        {
            var possibleClassNames = GetAllPossibleResults(alignment);
            if (!possibleClassNames.Any())
                throw new IncompatibleRandomizersException();

            var tableName = String.Format("{0}CharacterClasses", alignment.Goodness);
            var className = String.Empty;

            do className = percentileResultProvider.GetPercentileResult(tableName);
            while (!possibleClassNames.Contains(className));

            return className;
        }

        protected abstract Boolean CharacterClassIsAllowed(String className, Alignment alignment);

        public IEnumerable<String> GetAllPossibleResults(Alignment alignment)
        {
            var tableName = String.Format("{0}CharacterClasses", alignment.Goodness);
            var classNames = percentileResultProvider.GetAllResults(tableName);
            return classNames.Where(c => CharacterClassIsAllowed(c, alignment));
        }
    }
}