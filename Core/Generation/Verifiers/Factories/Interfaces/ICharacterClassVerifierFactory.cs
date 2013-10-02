using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Factories.Interfaces
{
    public interface ICharacterClassVerifierFactory
    {
        IClassNameVerifier Create(IClassNameRandomizer alignmentRandomizer);
    }
}