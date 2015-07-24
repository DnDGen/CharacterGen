using NPCGen.Common.Alignments;

namespace NPCGen.Generators.Interfaces.Randomizers.Alignments
{
    public interface ISetAlignmentRandomizer : IAlignmentRandomizer
    {
        Alignment SetAlignment { get; set; }
    }
}