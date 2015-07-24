using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;

namespace NPCGen.Generators
{
    public class AlignmentGenerator : IAlignmentGenerator
    {
        public Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }
    }
}