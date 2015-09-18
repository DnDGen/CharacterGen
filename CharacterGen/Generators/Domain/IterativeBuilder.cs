﻿using System;

namespace CharacterGen.Generators.Domain
{
    public abstract class IterativeBuilder
    {
        private const Int32 MaxRetries = 10000;

        protected T Build<T>(Func<T> buildInstructions, Func<T, Boolean> isValid)
        {
            T builtObject;
            var retries = 0;

            do builtObject = buildInstructions();
            while (retries++ < MaxRetries && isValid(builtObject) == false);

            if (retries > MaxRetries)
                throw new Exception("Exceeded max retries to build");

            return builtObject;
        }
    }
}