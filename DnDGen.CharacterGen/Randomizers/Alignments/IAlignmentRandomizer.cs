using System.Collections.Generic;
using DnDGen.CharacterGen.Alignments;

namespace DnDGen.CharacterGen.Randomizers.Alignments
{
    public interface IAlignmentRandomizer
    {
        Alignment Randomize();
        IEnumerable<Alignment> GetAllPossibleResults();
    }
}