using CharacterGen.Common.Alignments;

namespace CharacterGen.Generators.Randomizers.Alignments
{
    public interface ISetAlignmentRandomizer : IAlignmentRandomizer
    {
        Alignment SetAlignment { get; set; }
    }
}