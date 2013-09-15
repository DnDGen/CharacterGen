using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using System;

namespace NPCGen.Core.Generation.Verifiers
{
    public interface IRandomizerVerifier
    {
        Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassRandomizer classRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer);
    }
}