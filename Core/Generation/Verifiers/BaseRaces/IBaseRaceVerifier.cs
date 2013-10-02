using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.BaseRaces
{
    public interface IBaseRaceVerifier
    {
        Boolean VerifyCompatibility(Alignment alignment);
        Boolean VerifyCompatibility(String className);
        Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer);
    }
}