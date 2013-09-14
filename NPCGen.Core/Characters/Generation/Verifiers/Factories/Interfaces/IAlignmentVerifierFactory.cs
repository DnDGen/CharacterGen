using NPCGen.Core.Characters.Generation.Randomizers.Alignments;
using NPCGen.Core.Characters.Generation.Verifiers.Alignments;

namespace NPCGen.Core.Characters.Generation.Verifiers.Factories.Interfaces
{
    public interface IAlignmentVerifierFactory
    {
        IAlignmentVerifier Create(IAlignmentRandomizer alignmentRandomizer);
    }
}