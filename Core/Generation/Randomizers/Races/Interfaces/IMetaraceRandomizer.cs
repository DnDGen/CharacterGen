using System;
using System.Collections.Generic;
using NPCGen.Core.Data.CharacterClasses;

namespace NPCGen.Core.Generation.Randomizers.Races.Interfaces
{
    public interface IMetaraceRandomizer
    {
        String Randomize(String goodness, CharacterClass characterClass);
        IEnumerable<String> GetAllPossibleResults(String goodness, CharacterClass characterClass);
    }
}