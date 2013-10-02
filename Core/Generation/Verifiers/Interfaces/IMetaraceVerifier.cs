using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Verifiers.Interfaces
{
    public interface IMetaraceVerifier
    {
        Boolean VerifyCompatibility(Alignment alignment);
        Boolean VerifyCompatibility(String className);
        Boolean VerifyCompatibility(String baseRace);
    }
}