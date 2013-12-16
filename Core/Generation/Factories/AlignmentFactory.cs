using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public class AlignmentFactory : IAlignmentFactory
    {
        public Alignment CreateWith(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }
    }
}