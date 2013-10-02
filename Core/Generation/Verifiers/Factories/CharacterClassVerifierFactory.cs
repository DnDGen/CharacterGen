using System;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Factories
{
    public static class CharacterClassVerifierFactory
    {
        public static IClassNameVerifier CreateUsing(IClassNameRandomizer classNameRandomizer)
        {
            throw new NotImplementedException();
        }
    }
}