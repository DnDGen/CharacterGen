using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Verifiers.CharacterClasses;

namespace NPCGen.Core.Generation.Verifiers.Factories.Interfaces
{
    public interface ICharacterClassVerifierFactory
    {
        ICharacterClassVerifier Create(IClassNameRandomizer alignmentRandomizer);
    }
}