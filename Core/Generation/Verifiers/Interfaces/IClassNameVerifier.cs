using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Interfaces
{
    public interface IClassNameVerifier
    {
        Boolean VerifyCompatibility(Alignment alignment);
        Boolean VerifyCompatibility(IBaseRaceRandomizer baseRaceRandomizer);
        Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer);
    }
}