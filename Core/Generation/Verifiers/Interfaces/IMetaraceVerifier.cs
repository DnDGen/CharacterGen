using System;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Verifiers.Interfaces
{
    public interface IMetaraceVerifier
    {
        Boolean VerifyCompatibility(Alignment alignment);
        Boolean VerifyClassNameCompatibility(String className);
        Boolean VerifyBaseRaceCompatibility(String baseRace);
    }
}