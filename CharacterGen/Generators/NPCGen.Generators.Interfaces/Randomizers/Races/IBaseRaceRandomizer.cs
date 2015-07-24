using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface IBaseRaceRandomizer
    {
        String Randomize(String goodness, CharacterClass characterClass);
        IEnumerable<String> GetAllPossibles(String goodness, CharacterClass characterClass);
    }
}