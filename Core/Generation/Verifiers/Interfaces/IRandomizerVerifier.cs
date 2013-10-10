using System;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Interfaces
{
    public interface IRandomizerVerifier
    {
        Boolean VerifyCompatibility(VerifierCollection verifierCollection, IClassNameRandomizer classNameRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer);
    }
}