using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface IMetaraceRandomizer
    {
        String Randomize(String goodness, CharacterClass characterClass);
        IEnumerable<String> GetAllPossible(String goodness, CharacterClass characterClass);
    }
}