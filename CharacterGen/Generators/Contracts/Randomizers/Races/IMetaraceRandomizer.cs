using System;
using System.Collections.Generic;
using CharacterGen.Common.CharacterClasses;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface IMetaraceRandomizer
    {
        String Randomize(String goodness, CharacterClass characterClass);
        IEnumerable<String> GetAllPossible(String goodness, CharacterClass characterClass);
    }
}