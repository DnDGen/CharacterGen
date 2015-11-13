using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;

namespace CharacterGen.Generators.Domain
{
    public class AlignmentGenerator : IterativeGenerator, IAlignmentGenerator
    {
        public Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }
    }
}