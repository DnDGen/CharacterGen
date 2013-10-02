using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments.Interfaces
{
    public interface IAlignmentRandomizer
    {
        Alignment Randomize();
    }
}