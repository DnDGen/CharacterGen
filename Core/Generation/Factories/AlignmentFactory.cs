using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class AlignmentFactory
    {
        public static Alignment CreateUsing(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }
    }
}