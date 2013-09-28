﻿using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Generation.Verifiers.CharacterClasses
{
    public interface ICharacterClassVerifier
    {
        Boolean VerifyCompatibility(Alignment alignment);
        Boolean VerifyCompatibility(IBaseRaceRandomizer baseRaceRandomizer);
        Boolean VerifyCompatibility(IMetaraceRandomizer metaraceRandomizer);
    }
}