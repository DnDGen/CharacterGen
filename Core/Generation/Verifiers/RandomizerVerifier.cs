using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Factories.Interfaces;
using System;

namespace NPCGen.Core.Generation.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        private IAlignmentVerifierFactory alignmentVerifierFactory;

        public RandomizerVerifier(IAlignmentVerifierFactory alignmentVerifierFactory)
        {
            this.alignmentVerifierFactory = alignmentVerifierFactory;
        }

        public Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassRandomizer classRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var verified = true;

            var alignmentVerifier = alignmentVerifierFactory.Create(alignmentRandomizer);
            verified &= alignmentVerifier.VerifyCompatiblity(classRandomizer);
            verified &= alignmentVerifier.VerifyCompatiblity(baseRaceRandomizer);
            verified &= alignmentVerifier.VerifyCompatiblity(metaraceRandomizer);

            if (classRandomizer is HealerClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is MageClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is NonSpellcasterClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is SpellcasterClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is StealthClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is WarriorClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is SetClass)
            {
                throw new NotImplementedException();
            }

            return verified;
        }
    }
}