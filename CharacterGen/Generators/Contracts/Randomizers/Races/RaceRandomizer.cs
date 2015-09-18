using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Randomizers.Races
{
    public interface RaceRandomizer
    {
        String Randomize(Alignment alignment, CharacterClass characterClass);
        IEnumerable<String> GetAllPossible(Alignment alignment, CharacterClass characterClass);
    }
}