using CharacterGen.Alignments;
using CharacterGen.Randomizers.Alignments;

namespace CharacterGen.Domain.Generators.Alignments
{
    internal interface IAlignmentGenerator
    {
        Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer);
    }
}