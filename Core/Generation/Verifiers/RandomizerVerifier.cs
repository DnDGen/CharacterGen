using System;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers.Factories;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        public static Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            if (!VerifyAlignment(alignmentRandomizer, classNameRandomizer, baseRaceRandomizer, metaraceRandomizer))
                return false;

            if (!VerifyClass(classNameRandomizer))
                return false;

            return true;
        }

        private static Boolean VerifyAlignment(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var alignmentVerifier = AlignmentVerifierFactory.CreateUsing(alignmentRandomizer);

            if (!alignmentVerifier.VerifyCompatibility(classRandomizer))
                return false;

            if (!alignmentVerifier.VerifyCompatibility(baseRaceRandomizer))
                return false;

            if (!alignmentVerifier.VerifyCompatibility(metaraceRandomizer))
                return false;

            return true;
        }

        private static Boolean VerifyClass(IClassNameRandomizer classRandomizer)
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