using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using System;

namespace CharacterGen.Generators.Verifiers
{
    public interface IRandomizerVerifier
    {
        Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        Boolean VerifyAlignmentCompatibility(Alignment alignment, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        Boolean VerifyCharacterClassCompatibility(Alignment alignment, CharacterClass characterClass, RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer);
    }
}