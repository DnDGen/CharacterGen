using System;
using System.Collections.Generic;
using NPCGen.Core.Data.CharacterClasses;

namespace NPCGen.Core.Generation.Randomizers.Races.Interfaces
{
    public interface IBaseRaceRandomizer
    {
        String Randomize(String goodness, CharacterClassPrototype prototype);
        IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClassPrototype prototype);
    }
}