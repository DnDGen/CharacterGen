using System;

namespace CharacterGen.Domain.Generators
{
    internal class IterativeGenerator : Generator
    {
        private const int MaxRetries = 20000;

        public T Generate<T>(Func<T> buildInstructions, Func<T, bool> isValid)
        {
            T builtObject;
            var retries = 1;

            do builtObject = buildInstructions();
            while (isValid(builtObject) == false && retries++ < MaxRetries);

            if (isValid(builtObject))
                return builtObject;

            throw new Exception($"Failed to generate {typeof(T).Name} after {retries} iterations");
        }
    }
}
