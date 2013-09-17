using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses
{
    public abstract class BaseClassRandomizer : ICharacterClassRandomizer
    {
        private IPercentileResultProvider percentileResultProvider;

        public BaseClassRandomizer(IPercentileResultProvider percentileResultProvider)
        {
            this.percentileResultProvider = percentileResultProvider;
        }

        public String Randomize(Alignment alignment)
        {
            var tableName = String.Format("{0}CharacterClasses", alignment.GetGoodnessString());
            var className = String.Empty;

            do className = percentileResultProvider.GetPercentileResult(tableName);
            while (!CharacterClassIsAllowed(className, alignment));

            return className;
        }

        protected abstract Boolean CharacterClassIsAllowed(String className, Alignment alignment);
    }
}