using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Randomizers.CharacterClasses
{
    public interface ILevelRandomizer
    {
        Int32 Randomize();
        IEnumerable<Int32> GetAllPossibleResults();
    }
}