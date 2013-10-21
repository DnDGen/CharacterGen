using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames
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

            var tableName = String.Format("{0}CharacterClasses", alignment.GetGoodnessString());
            var className = String.Empty;

            do className = percentileResultProvider.GetPercentileResult(tableName);
            while (!possibleClassNames.Contains(className));

            return className;
        }

        protected abstract Boolean CharacterClassIsAllowed(String className, Alignment alignment);

        public IEnumerable<String> GetAllPossibleResults(Alignment alignment)
        {
            throw new NotImplementedException();
        }
    }
}