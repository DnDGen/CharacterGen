using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;

namespace CharacterGen.Generators
{
    public interface IAlignmentGenerator
    {
        Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer);
    }
}