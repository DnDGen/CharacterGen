using CharacterGen.Generators;
using System;

namespace CharacterGen.Tests.Unit.Generators
{
    public class ConfigurableIterationGenerator : Generator
    {
        private readonly Int32 maxRetries;

        public ConfigurableIterationGenerator(Int32 maxRetries = 1)
        {
            this.maxRetries = maxRetries;
        }

        public T Generate<T>(Func<T> buildInstructions, Func<T, Boolean> isValid)
        {
            T builtObject;
            var retries = 0;

            do builtObject = buildInstructions();
            while (retries++ < maxRetries && isValid(builtObject) == false);

            if (isValid(builtObject))
                return builtObject;

            return default(T);
        }
    }
}
