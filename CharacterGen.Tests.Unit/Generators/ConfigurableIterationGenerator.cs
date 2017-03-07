using CharacterGen.Domain.Generators;
using System;

namespace CharacterGen.Tests.Unit.Generators
{
    public class ConfigurableIterationGenerator : Generator
    {
        private readonly int maxRetries;

        public ConfigurableIterationGenerator(int maxRetries = 1)
        {
            this.maxRetries = maxRetries;
        }

        public T Generate<T>(Func<T> buildInstructions, Func<T, bool> isValid, Func<T> buildDefault, string defaultDescription)
        {
            T builtObject;
            var retries = 1;

            do builtObject = buildInstructions();
            while (retries++ < maxRetries && isValid(builtObject) == false);

            if (isValid(builtObject))
                return builtObject;

            return buildDefault();
        }
    }
}
