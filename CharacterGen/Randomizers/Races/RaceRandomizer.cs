using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using System.Collections.Generic;

namespace CharacterGen.Randomizers.Races
{
    public interface RaceRandomizer
    {
        string Randomize(Alignment alignment, CharacterClass characterClass);
        IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass);
    }
}