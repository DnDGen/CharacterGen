using NPCGen.Core.Characters.Generation.Randomizers.Alignments;
using NPCGen.Core.Characters.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Characters.Generation.Randomizers.Races.Metaraces;
using System;

namespace NPCGen.Core.Characters.Generation.Verifiers
{
    public interface IRandomizerVerifier
    {
        Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassRandomizer classRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer);
    }
}