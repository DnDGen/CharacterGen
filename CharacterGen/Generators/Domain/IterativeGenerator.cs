using System;

namespace CharacterGen.Generators.Domain
{
    public class IterativeGenerator : Generator
    {
        private const Int32 MaxRetries = 10000;

        public T Generate<T>(Func<T> buildInstructions, Func<T, Boolean> isValid)
        {
            T builtObject;
            var retries = 0;

            do builtObject = buildInstructions();
            while (retries++ < MaxRetries && isValid(builtObject) == false);

            if (isValid(builtObject))
                return builtObject;

            return default(T);
        }
    }
}
