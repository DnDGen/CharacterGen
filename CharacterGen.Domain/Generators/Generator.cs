using System;

namespace CharacterGen.Domain.Generators
{
    internal interface Generator
    {
        T Generate<T>(Func<T> buildInstructions, Func<T, bool> isValid, Func<T> buildDefault);
    }
}
