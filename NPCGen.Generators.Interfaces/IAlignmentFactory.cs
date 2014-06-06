using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;

namespace NPCGen.Generators.Interfaces
{
    public interface IAlignmentFactory
    {
        Alignment CreateWith(IAlignmentRandomizer alignmentRandomizer);
    }
}