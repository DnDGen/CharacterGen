using NPCGen.Core.Data.Alignments;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Alignments.Interfaces
{
    public interface IAlignmentRandomizer
    {
        Alignment Randomize();
        IEnumerable<Alignment> GetAllPossibleResults();
    }
}