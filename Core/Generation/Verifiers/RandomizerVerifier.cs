using System;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Factories.Interfaces;

namespace NPCGen.Core.Generation.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        private IAlignmentVerifierFactory alignmentVerifierFactory;
        private ICharacterClassVerifierFactory characterClassVerifierFactory;

        public RandomizerVerifier(IAlignmentVerifierFactory alignmentVerifierFactory, ICharacterClassVerifierFactory characterClassVerifierFactory)
        {
            this.alignmentVerifierFactory = alignmentVerifierFactory;
            this.characterClassVerifierFactory = characterClassVerifierFactory;
        }

        public Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            if (!VerifyAlignment(alignmentRandomizer, classRandomizer, baseRaceRandomizer, metaraceRandomizer))
                return false;

            if (!VerifyClass(classRandomizer))
                return false;

            return true;
        }

        private Boolean VerifyAlignment(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var alignmentVerifier = alignmentVerifierFactory.Create(alignmentRandomizer);

            if (!alignmentVerifier.VerifyCompatibility(classRandomizer))
                return false;

            if (!alignmentVerifier.VerifyCompatibility(baseRaceRandomizer))
                return false;

            if (!alignmentVerifier.VerifyCompatibility(metaraceRandomizer))
                return false;

            return true;
        }

        private Boolean VerifyClass(IClassNameRandomizer classRandomizer)
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