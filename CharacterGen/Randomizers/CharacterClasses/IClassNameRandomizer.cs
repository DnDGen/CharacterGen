using CharacterGen.Alignments;
using System.Collections.Generic;

namespace CharacterGen.Randomizers.CharacterClasses
{
    public interface IClassNameRandomizer
    {
        string Randomize(Alignment alignment);
        IEnumerable<string> GetAllPossibleResults(Alignment alignment);
    }
}