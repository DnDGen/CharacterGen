using System;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
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
                && VerifyClassName(classNameRandomizer);
        }

        private Boolean VerifyAlignment(IAlignmentVerifier alignmentVerifier, IClassNameRandomizer classRandomizer, 
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            return alignmentVerifier.VerifyCompatibility(classRandomizer)
                && alignmentVerifier.VerifyCompatibility(baseRaceRandomizer)
                && alignmentVerifier.VerifyCompatibility(metaraceRandomizer);
        }

        private Boolean VerifyClassName(IClassNameRandomizer classRandomizer)
        {
            if (classRandomizer is HealerClassNameRandomizer)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is MageClassNameRandomizer)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is NonSpellcasterClassNameRandomizer)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is SpellcasterClassNameRandomizer)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is StealthClassNameRandomizer)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is WarriorClassNameRandomizer)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is SetClassNameRandomizer)
            {
                throw new NotImplementedException();
            }

            return true;
        }
    }
}