using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        private IAdjustmentsSelector adjustmentsSelector;

        public RandomizerVerifier(IAdjustmentsSelector adjustmentsSelector)
        {
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var alignments = alignmentRandomizer.GetAllPossibleResults();
            return alignments.Any() && alignments.Any(a => VerifyAlignmentCompatibility(a, classNameRandomizer, levelRandomizer,
                baseRaceRandomizer, metaraceRandomizer));
        }

        public Boolean VerifyAlignmentCompatibility(Alignment alignment, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var classNames = classNameRandomizer.GetAllPossibleResults(alignment);
            var levels = levelRandomizer.GetAllPossibleResults();

            var characterClasses = GetAllCharacterClassPrototypes(classNames, levels);
            return characterClasses.Any() && characterClasses.Any(c => VerifyCharacterClassCompatibility(alignment.Goodness, c,
                baseRaceRandomizer, metaraceRandomizer));
        }

        private IEnumerable<CharacterClass> GetAllCharacterClassPrototypes(IEnumerable<String> classNames, IEnumerable<Int32> levels)
        {
            var characterClasses = new List<CharacterClass>();

            foreach (var className in classNames)
                foreach (var level in levels)
                    characterClasses.Add(new CharacterClass { ClassName = className, Level = level });

            return characterClasses;
        }

        public Boolean VerifyCharacterClassCompatibility(String goodness, CharacterClass characterClass, IBaseRaceRandomizer baseRaceRandomizer,
            IMetaraceRandomizer metaraceRandomizer)
        {
            var baseRaces = baseRaceRandomizer.GetAllPossibleResults(goodness, characterClass);
            var metaraces = metaraceRandomizer.GetAllPossibleResults(goodness, characterClass);

            return baseRaces.Any() && metaraces.Any() && LevelAdjustmentsAreAllowed(baseRaces, metaraces, characterClass.Level);
        }

        private Boolean LevelAdjustmentsAreAllowed(IEnumerable<String> baseRaces, IEnumerable<String> metaraces, Int32 level)
        {
            var levelAdjustments = adjustmentsSelector.SelectAdjustmentsFrom("LevelAdjustments");
            var minBaseRaceLevelAdjustment = levelAdjustments.Where(kvp => baseRaces.Contains(kvp.Key)).Min(kvp => kvp.Value);
            var minMetaraceLevelAdjustment = levelAdjustments.Where(kvp => metaraces.Contains(kvp.Key)).Min(kvp => kvp.Value);

            return minBaseRaceLevelAdjustment + minMetaraceLevelAdjustment < level;
        }
    }
}