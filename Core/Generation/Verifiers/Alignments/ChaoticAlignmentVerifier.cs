using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using System;
using NPCGen.Core.Generation.Randomizers.CharacterClasses;
using NPCGen.Core.Data.CharacterClasses;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public class ChaoticAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatiblity(ICharacterClassRandomizer classRandomizer)
        {
            if (classRandomizer is SetClass)
            {
                var setClass = classRandomizer as SetClass;
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