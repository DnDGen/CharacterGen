using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Factories.Interfaces;
using System;
using NPCGen.Core.Generation.Randomizers.ClassNames;

namespace NPCGen.Core.Generation.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        private IAlignmentVerifierFactory alignmentVerifierFactory;

        public RandomizerVerifier(IAlignmentVerifierFactory alignmentVerifierFactory)
        {
            this.alignmentVerifierFactory = alignmentVerifierFactory;
        }

        public Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var verified = true;

            var alignmentVerifier = alignmentVerifierFactory.Create(alignmentRandomizer);
            verified &= alignmentVerifier.VerifyCompatiblity(classRandomizer);
            verified &= alignmentVerifier.VerifyCompatiblity(baseRaceRandomizer);
            verified &= alignmentVerifier.VerifyCompatiblity(metaraceRandomizer);

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

            return verified;
        }
    }
}