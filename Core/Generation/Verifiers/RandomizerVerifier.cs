using System;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        public Boolean VerifyCompatibility(VerifierCollection verifierCollection, IClassNameRandomizer classNameRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            return VerifyAlignment(verifierCollection.AlignmentVerifier, classNameRandomizer, baseRaceRandomizer, metaraceRandomizer)
                && VerifyClassName(verifierCollection.ClassNameVerifier, baseRaceRandomizer, metaraceRandomizer)
                && VerifyBaseRace(verifierCollection.BaseRaceVerifier, metaraceRandomizer);
        }

        private Boolean VerifyAlignment(IAlignmentVerifier alignmentVerifier, IClassNameRandomizer classNameRandomizer, 
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            return alignmentVerifier.VerifyCompatibility(classNameRandomizer)
                && alignmentVerifier.VerifyCompatibility(baseRaceRandomizer)
                && alignmentVerifier.VerifyCompatibility(metaraceRandomizer);
        }

        private Boolean VerifyClassName(IClassNameVerifier classNameVerifier, IBaseRaceRandomizer baseRaceRandomizer,
            IMetaraceRandomizer metaraceRandomizer)
        {
            return classNameVerifier.VerifyCompatibility(baseRaceRandomizer)
                && classNameVerifier.VerifyCompatibility(metaraceRandomizer);
        }

        private Boolean VerifyBaseRace(IBaseRaceVerifier baseRaceVerifier, IMetaraceRandomizer metaraceRandomizer)
        {
            return baseRaceVerifier.VerifyCompatibility(metaraceRandomizer);
        }
    }
}