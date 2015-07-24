using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;

namespace NPCGen.Generators.Interfaces
{
    public interface IAlignmentGenerator
    {
        Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer);
    }
}