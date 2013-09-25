using System;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public class GoodAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatibility(IClassNameRandomizer classRandomizer)
        {
            throw new NotImplementedException();
        }

        public Boolean VerifyCompatibility(IBaseRaceRandomizer baseRaceRandomizer)
        {
            throw new NotImplementedException();
        }

        public Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer)
        {
            throw new NotImplementedException();
        }
    }
}