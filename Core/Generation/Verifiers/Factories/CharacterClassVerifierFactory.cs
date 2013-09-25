using System;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Verifiers.CharacterClasses;
using NPCGen.Core.Generation.Verifiers.Factories.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Factories
{
    public class CharacterClassVerifierFactory : ICharacterClassVerifierFactory
    {
        public ICharacterClassVerifier Create(IClassNameRandomizer alignmentRandomizer)
        {
            throw new NotImplementedException();
        }
    }
}