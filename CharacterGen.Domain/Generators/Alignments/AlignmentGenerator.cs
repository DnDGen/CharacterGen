using CharacterGen.Alignments;
using CharacterGen.Randomizers.Alignments;

namespace CharacterGen.Domain.Generators.Alignments
{
    internal class AlignmentGenerator : IAlignmentGenerator
    {
        public Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }
    }
}