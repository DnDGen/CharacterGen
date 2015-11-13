using System;

namespace CharacterGen.Generators
{
    public interface Generator
    {
        T Generate<T>(Func<T> buildInstructions, Func<T, Boolean> isValid);
    }
}
