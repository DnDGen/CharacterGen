using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Verifiers.Interfaces
{
    public interface IRandomizerVerifier
    {
        Boolean VerifyCompatibility();
        Boolean VerifyAlignmentCompatibility(Alignment alignment);
        Boolean VerifyClassNameCompatibility(String goodness, String className);
    }
}