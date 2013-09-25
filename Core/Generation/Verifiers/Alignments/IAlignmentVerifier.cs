using System;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public interface IAlignmentVerifier
    {
        Boolean VerifyCompatibility(IClassNameRandomizer classRandomizer);
        Boolean VerifyCompatibility(IBaseRaceRandomizer baseRaceRandomizer);
        Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer);
    }
}