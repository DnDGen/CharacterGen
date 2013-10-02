using System;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Verifiers.Factories.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Factories
{
    public class CharacterClassVerifierFactory : ICharacterClassVerifierFactory
    {
        public IClassNameVerifier Create(IClassNameRandomizer alignmentRandomizer)
        {
            throw new NotImplementedException();
        }
    }
}