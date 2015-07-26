using CharacterGen.Common.Alignments;
using CharacterGen.Generators;
using CharacterGen.Generators.Randomizers.Alignments;

namespace CharacterGen.Generators.Domain
{
    public class AlignmentGenerator : IAlignmentGenerator
    {
        public Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }
    }
}