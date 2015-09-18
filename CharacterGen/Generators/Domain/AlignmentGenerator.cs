using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;

namespace CharacterGen.Generators.Domain
{
    public class AlignmentGenerator : IterativeBuilder, IAlignmentGenerator
    {
        public Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }
    }
}