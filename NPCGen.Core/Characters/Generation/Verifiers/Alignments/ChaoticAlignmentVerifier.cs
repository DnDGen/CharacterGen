using NPCGen.Core.Characters.Data.Classes;
using NPCGen.Core.Characters.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Characters.Generation.Randomizers.Races.Metaraces;
using System;

namespace NPCGen.Core.Characters.Generation.Verifiers.Alignments
{
    public class ChaoticAlignmentVerifier : IAlignmentVerifier
    {
        public Boolean VerifyCompatiblity(IClassRandomizer classRandomizer)
        {
            if (classRandomizer is SetClass)
            {
                var setClass = classRandomizer as SetClass;
                var className = setClass.ClassName;
                return className != ClassConstants.MONK && className != ClassConstants.PALADIN;
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