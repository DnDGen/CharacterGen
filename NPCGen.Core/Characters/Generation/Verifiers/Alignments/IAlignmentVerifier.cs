using NPCGen.Core.Characters.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Characters.Generation.Randomizers.Races.Metaraces;
using System;

namespace NPCGen.Core.Characters.Generation.Verifiers.Alignments
{
    public interface IAlignmentVerifier
    {
        Boolean VerifyCompatiblity(IClassRandomizer classRandomizer);
        Boolean VerifyCompatiblity(IBaseRaceRandomizer baseRaceRandomizer);
        Boolean VerifyCompatiblity(IMetaraceRandomizer metaraceRandomizer);
    }
}