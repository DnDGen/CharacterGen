using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces
{
    public interface ILevelRandomizer
    {
        Int32 Randomize();
        IEnumerable<Int32> GetAllPossibleResults();
    }
}