using NPCGen.Core.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using System;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public interface IAlignmentVerifier
    {
        Boolean VerifyCompatiblity(ICharacterClassRandomizer classRandomizer);
        Boolean VerifyCompatiblity(IBaseRaceRandomizer baseRaceRandomizer);
        Boolean VerifyCompatiblity(IMetaraceRandomizer metaraceRandomizer);
    }
}