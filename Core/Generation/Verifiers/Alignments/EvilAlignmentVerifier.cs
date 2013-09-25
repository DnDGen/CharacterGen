using System;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public class EvilAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatibility(IClassNameRandomizer classRandomizer)
        {
            if (classRandomizer is SetClassNameRandomizer)
            {
                var setClass = classRandomizer as SetClassNameRandomizer;
                return setClass.ClassName != CharacterClassConstants.Paladin;
            }

            return true;
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