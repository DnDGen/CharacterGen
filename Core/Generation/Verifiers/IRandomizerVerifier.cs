using System;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Verifiers
{
    public interface IRandomizerVerifier
    {
        Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer);
    }
}