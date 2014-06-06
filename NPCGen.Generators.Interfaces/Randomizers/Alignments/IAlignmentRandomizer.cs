using System.Collections.Generic;
using NPCGen.Common.Alignments;

namespace NPCGen.Generators.Interfaces.Randomizers.Alignments
{
    public interface IAlignmentRandomizer
    {
        Alignment Randomize();
        IEnumerable<Alignment> GetAllPossibleResults();
    }
}