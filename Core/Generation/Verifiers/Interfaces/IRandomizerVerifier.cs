using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;

namespace NPCGen.Core.Generation.Verifiers.Interfaces
{
    public interface IRandomizerVerifier
    {
        Boolean VerifyCompatibility();
        Boolean VerifyAlignmentCompatibility(Alignment alignment);
        Boolean VerifyCharacterClassCompatibility(String goodness, CharacterClassPrototype prototype);
    }
}