using System;
using System.Collections.Generic;
using CharacterGen.Common.CharacterClasses;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface IBaseRaceRandomizer
    {
        String Randomize(String goodness, CharacterClass characterClass);
        IEnumerable<String> GetAllPossibles(String goodness, CharacterClass characterClass);
    }
}