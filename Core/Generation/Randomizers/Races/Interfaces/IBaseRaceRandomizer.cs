using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Races.Interfaces
{
    public interface IBaseRaceRandomizer
    {
        String Randomize(String goodness, String className);
        IEnumerable<String> GetAllPossibleResults(String goodness, String className);
    }
}