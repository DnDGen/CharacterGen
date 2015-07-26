using System.Collections.Generic;
using CharacterGen.Common.Alignments;

namespace CharacterGen.Generators.Randomizers.Alignments
{
    public interface IAlignmentRandomizer
    {
        Alignment Randomize();
        IEnumerable<Alignment> GetAllPossibleResults();
    }
}