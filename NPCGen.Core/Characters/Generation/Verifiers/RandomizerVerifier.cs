using NPCGen.Core.Characters.Generation.Randomizers.Alignments;
using NPCGen.Core.Characters.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Characters.Generation.Randomizers.Races;
using System;

namespace NPCGen.Core.Characters.Generation.Verifiers
{
    public class RandomizerVerifier
    {
        public Boolean Verify(IAlignmentRandomizer alignmentRandomizer, IClassRandomizer classRandomizer,
            IRaceRandomizer raceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            if (alignmentRandomizer is ChaoticAlignment)
            {

            }
        }
    }
}