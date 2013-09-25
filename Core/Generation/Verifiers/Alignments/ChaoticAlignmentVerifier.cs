using System;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public class ChaoticAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatibility(IClassNameRandomizer classRandomizer)
        {
            if (classRandomizer is SetClassNameRandomizer)
            {
                var setClass = classRandomizer as SetClassNameRandomizer;
                var className = setClass.ClassName;
                return className != CharacterClassConstants.Monk && className != CharacterClassConstants.Paladin;
            }

            return true;
        }

        public Boolean VerifyCompatibility(IBaseRaceRandomizer baseRaceRandomizer)
        {
            return true;
        }

        public Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer)
        {
            return true;
        }
    }
}