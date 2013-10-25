using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
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
        private ILevelRandomizer levelRandomizer;

        public RandomizerVerifier(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            this.alignmentRandomizer = alignmentRandomizer;
            this.classNameRandomizer = classNameRandomizer;
            this.levelRandomizer = levelRandomizer;
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
            var levels = levelRandomizer.GetAllPossibleResults();

            var characterClasses = GetAllCharacterClasses(classNames, levels);
            return characterClasses.Any() && characterClasses.Any(c => VerifyCharacterClassCompatibility(alignment.Goodness, c));
        }

        private IEnumerable<CharacterClass> GetAllCharacterClasses(IEnumerable<String> classNames, IEnumerable<Int32> levels)
        {
            var characterClasses = new List<CharacterClass>();

            foreach (var className in classNames)
                foreach (var level in levels)
                    characterClasses.Add(new CharacterClass() { ClassName = className, Level = level });

            return characterClasses;
        }

        public Boolean VerifyCharacterClassCompatibility(String goodness, CharacterClass characterClass)
        {
            var baseRaces = baseRaceRandomizer.GetAllPossibleResults(goodness, characterClass);
            var metaraces = metaraceRandomizer.GetAllPossibleResults(goodness, characterClass);

            return baseRaces.Any() && metaraces.Any();
        }
    }
}