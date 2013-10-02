using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Verifiers.Alignments;

namespace NPCGen.Core.Generation.Verifiers.Factories.Interfaces
{
    public interface IAlignmentVerifierFactory
    {
        IAlignmentVerifier Create(IAlignmentRandomizer alignmentRandomizer);
    }
}