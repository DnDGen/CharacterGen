using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IAlignmentFactory
    {
        Alignment CreateWith(IAlignmentRandomizer alignmentRandomizer);
    }
}