using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface IBaseRaceRandomizer
    {
        String Randomize(String goodness, CharacterClassPrototype prototype);
        IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClassPrototype prototype);
    }
}