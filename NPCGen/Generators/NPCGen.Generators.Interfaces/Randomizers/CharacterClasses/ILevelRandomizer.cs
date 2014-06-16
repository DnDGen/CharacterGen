using System;
using System.Collections.Generic;

namespace NPCGen.Generators.Interfaces.Randomizers.CharacterClasses
{
    public interface ILevelRandomizer
    {
        Int32 Randomize();
        IEnumerable<Int32> GetAllPossibleResults();
    }
}