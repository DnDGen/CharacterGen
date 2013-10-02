using System;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Alignments
{
    public interface IAlignmentVerifier
    {
        Boolean VerifyCompatibility(IClassNameRandomizer classRandomizer);
        Boolean VerifyCompatibility(IBaseRaceRandomizer baseRaceRandomizer);
        Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer);
    }
}