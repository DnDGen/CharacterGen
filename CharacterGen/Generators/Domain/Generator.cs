using System;

namespace CharacterGen.Generators.Domain
{
    public abstract class Generator
    {
        private const Int32 MaxRetries = 10000;

        protected T Generate<T>(Func<T> generate, Func<T, Boolean> isValid)
        {
            T generatedObject;
            var retries = 0;

            do generatedObject = generate();
            while (retries++ < MaxRetries && isValid(generatedObject) == false);

            if (retries > MaxRetries)
                throw new Exception("Exceeded max retries to generate");

            return generatedObject;
        }
    }
}
