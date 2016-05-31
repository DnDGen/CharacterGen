using System.Collections.Generic;
using CharacterGen.Alignments;

namespace CharacterGen.Randomizers.Alignments
{
    public interface IAlignmentRandomizer
    {
        Alignment Randomize();
        IEnumerable<Alignment> GetAllPossibleResults();
    }
}