using System;

namespace CharacterGen.Domain.Generators
{
    internal interface Generator
    {
        T Generate<T>(Func<T> buildInstructions, string description, Func<T, bool> isValid, Func<T> buildDefault, string defaultDescription = "");
    }
}
