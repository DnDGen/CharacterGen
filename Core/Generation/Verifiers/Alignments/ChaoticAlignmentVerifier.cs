using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using System;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Data.CharacterClasses;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public class ChaoticAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatiblity(IClassNameRandomizer classRandomizer)
        {
            if (classRandomizer is SetClassNameRandomizer)
            {
                var setClass = classRandomizer as SetClassNameRandomizer;
                var className = setClass.ClassName;
                return className != CharacterClassConstants.Monk && className != CharacterClassConstants.Paladin;
            }

            return true;
        }

        public Boolean VerifyCompatiblity(IBaseRaceRandomizer baseRaceRandomizer)
        {
            return true;
        }

        public Boolean VerifyCompatiblity(IMetaraceRandomizer metaraceRandomizer)
        {
            return true;
        }
    }
}