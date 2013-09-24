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
        public IAlignmentVerifierFactory AlignmentVerifierFactory { get; set; }
        public ICharacterClassVerifierFactory CharacterClassVerifierFactory { get; set; }

        public RandomizerVerifier(IAlignmentVerifierFactory alignmentVerifierFactory)
        {
            this.AlignmentVerifierFactory = alignmentVerifierFactory;
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
            var alignmentVerifier = AlignmentVerifierFactory.Create(alignmentRandomizer);

            if (!alignmentVerifier.VerifyCompatiblity(classRandomizer))
                return false;

            if (!alignmentVerifier.VerifyCompatiblity(baseRaceRandomizer))
                return false;

            if (!alignmentVerifier.VerifyCompatiblity(metaraceRandomizer))
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