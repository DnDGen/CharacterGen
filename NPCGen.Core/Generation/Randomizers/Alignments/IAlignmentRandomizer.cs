using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public interface IAlignmentRandomizer
    {
        Alignment Randomize();
    }
}