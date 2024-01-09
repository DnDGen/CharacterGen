using DnDGen.CharacterGen.Alignments;

namespace DnDGen.CharacterGen.Randomizers.Alignments
{
    public interface ISetAlignmentRandomizer : IAlignmentRandomizer
    {
        Alignment SetAlignment { get; set; }
    }
}