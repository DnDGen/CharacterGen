﻿using EventGen;
using System;

namespace CharacterGen.Domain.Generators
{
    internal class IterativeGenerator : Generator
    {
        private const int MaxRetries = 1000;
        private const int RetryDivisorForEvents = 2;

        private readonly GenEventQueue eventQueue;
        private readonly int numberOfRetriesUntilEventShouldFire;

        public IterativeGenerator(GenEventQueue eventQueue)
        {
            this.eventQueue = eventQueue;

            numberOfRetriesUntilEventShouldFire = MaxRetries / RetryDivisorForEvents;
        }

        public T Generate<T>(Func<T> buildInstructions, string description, Func<T, bool> isValid, Func<T> buildDefault, string defaultDescription = "")
        {
            T builtObject;
            var retries = 1;

            if (string.IsNullOrEmpty(defaultDescription))
                defaultDescription = description;

            eventQueue.Enqueue("CharacterGen", $"Beginning iterative generation of {description}");

            do
            {
                builtObject = buildInstructions();

                if (retries % numberOfRetriesUntilEventShouldFire == 0)
                    eventQueue.Enqueue("CharacterGen", $"Iterative generation of {description} has retried {retries} times");
            }
            while (!isValid(builtObject) && retries++ < MaxRetries);

            if (!isValid(builtObject))
            {
                retries--; //INFO: This is to compensate for the extra ++ in the loop
                eventQueue.Enqueue("CharacterGen", $"Generating {defaultDescription} by default");
                builtObject = buildDefault();
            }

            eventQueue.Enqueue("CharacterGen", $"Finished iterative generation of {description} after {retries} iterations");

            return builtObject;
        }
    }
}
