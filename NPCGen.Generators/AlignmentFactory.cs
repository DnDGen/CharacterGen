using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;

namespace NPCGen.Generators
{
    public class AlignmentFactory : IAlignmentFactory
    {
        public Alignment CreateWith(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }
    }
}