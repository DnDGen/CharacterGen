using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces.Randomizers.Races
{
    public interface IBaseRaceRandomizer
    {
        NameModel Randomize(String goodness, CharacterClass characterClass);
        IEnumerable<String> GetAllPossibleIds(String goodness, CharacterClass characterClass);
    }
}