using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using System;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Data.CharacterClasses;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public class EvilAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatiblity(IClassNameRandomizer classRandomizer)
        {
            if (classRandomizer is SetClass)
            {
                var setClass = classRandomizer as SetClass;
                return setClass.ClassName != CharacterClassConstants.Paladin;
            }

            return true;
        }

        public Boolean VerifyCompatiblity(IBaseRaceRandomizer baseRaceRandomizer)
        {
            throw new NotImplementedException();
        }

        public Boolean VerifyCompatiblity(IMetaraceRandomizer metaraceRandomizer)
        {
            throw new NotImplementedException();
        }
    }
}