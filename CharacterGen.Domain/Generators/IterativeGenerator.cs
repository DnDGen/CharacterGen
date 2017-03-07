using System;

namespace CharacterGen.Domain.Generators
{
    internal class IterativeGenerator : Generator
    {
        private const int MaxRetries = 1000;

        public T Generate<T>(Func<T> buildInstructions, Func<T, bool> isValid, Func<T> buildDefault, string defaultDescription)
        {
            T builtObject;
            var retries = 1;

            do builtObject = buildInstructions();
            while (isValid(builtObject) == false && retries++ < MaxRetries);

            if (isValid(builtObject))
                return builtObject;

            builtObject = buildDefault();

            //HACK: Replace this with EventGen eventually
            Console.WriteLine($"Generating {defaultDescription} by default");

            return builtObject;
        }
    }
}
