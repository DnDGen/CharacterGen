using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Verifiers.CharacterClasses;

namespace NPCGen.Core.Generation.Verifiers.Factories.Interfaces
{
    public interface ICharacterClassVerifierFactory
    {
        ICharacterClassVerifier Create(IClassNameRandomizer alignmentRandomizer);
    }
}