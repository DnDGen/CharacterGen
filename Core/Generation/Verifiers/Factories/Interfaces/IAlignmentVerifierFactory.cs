using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Factories.Interfaces
{
    public interface IAlignmentVerifierFactory
    {
        IAlignmentVerifier Create(IAlignmentRandomizer alignmentRandomizer);
    }
}