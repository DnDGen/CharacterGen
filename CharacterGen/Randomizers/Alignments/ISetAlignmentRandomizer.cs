using CharacterGen.Alignments;

namespace CharacterGen.Randomizers.Alignments
{
    public interface ISetAlignmentRandomizer : IAlignmentRandomizer
    {
        Alignment SetAlignment { get; set; }
    }
}