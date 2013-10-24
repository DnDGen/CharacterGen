using System;
using System.Linq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private IClassNameRandomizer classNameRandomizer;
        private IBaseRaceRandomizer baseRaceRandomizer;
        private IMetaraceRandomizer metaraceRandomizer;

        public RandomizerVerifier(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            this.alignmentRandomizer = alignmentRandomizer;
            this.classNameRandomizer = classNameRandomizer;
            this.baseRaceRandomizer = baseRaceRandomizer;
            this.metaraceRandomizer = metaraceRandomizer;
        }

        public Boolean VerifyCompatibility()
        {
            var alignments = alignmentRandomizer.GetAllPossibleResults();
            return alignments.Any() && alignments.Any(a => VerifyAlignmentCompatibility(a));
        }

        public Boolean VerifyAlignmentCompatibility(Alignment alignment)
        {
            var classNames = classNameRandomizer.GetAllPossibleResults(alignment);
            return classNames.Any() && classNames.Any(c => VerifyClassNameCompatibility(alignment.Goodness, c));
        }

        public Boolean VerifyClassNameCompatibility(String goodness, String className)
        {
            var baseRaces = baseRaceRandomizer.GetAllPossibleResults(goodness, className);
            var metaraces = metaraceRandomizer.GetAllPossibleResults(goodness, className);

            return baseRaces.Any() && metaraces.Any();
        }
    }
}