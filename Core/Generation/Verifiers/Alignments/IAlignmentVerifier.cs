using System;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public interface IAlignmentVerifier
    {
        Boolean VerifyCompatiblity(IClassNameRandomizer classRandomizer);
        Boolean VerifyCompatiblity(IBaseRaceRandomizer baseRaceRandomizer);
        Boolean VerifyCompatiblity(IMetaraceRandomizer metaraceRandomizer);
    }
}