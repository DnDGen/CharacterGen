using System;

namespace CharacterGen.Generators.Domain
{
    public class IterativeGenerator : Generator
    {
        private const Int32 MaxRetries = 10000;

        public T Generate<T>(Func<T> buildInstructions, Func<T, Boolean> isValid)
        {
            T builtObject;
            var retries = 1;

            do builtObject = buildInstructions();
            while (isValid(builtObject) == false && retries++ < MaxRetries);

            if (isValid(builtObject))
                return builtObject;

            return default(T);
        }
    }
}
