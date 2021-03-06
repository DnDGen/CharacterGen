﻿using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;

namespace CharacterGen.Verifiers
{
    public interface IRandomizerVerifier
    {
        bool VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        bool VerifyAlignmentCompatibility(Alignment alignment, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        bool VerifyCharacterClassCompatibility(Alignment alignment, CharacterClassPrototype characterClass, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer);
        bool VerifyRaceCompatibility(Alignment alignment, CharacterClassPrototype characterClass, RacePrototype race);
    }
}