using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public interface IAlignmentRandomizer
    {
        Alignment Randomize();
    }
}