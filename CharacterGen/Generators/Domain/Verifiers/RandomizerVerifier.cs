using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Verifiers
{
    public class RandomizerVerifier : IRandomizerVerifier
    {
        private IAdjustmentsSelector adjustmentsSelector;
        private ICollectionsSelector collectionsSelector;

        public RandomizerVerifier(IAdjustmentsSelector adjustmentsSelector, ICollectionsSelector collectionsSelector)
        {
            this.adjustmentsSelector = adjustmentsSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public Boolean VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var alignments = alignmentRandomizer.GetAllPossibleResults();
            return alignments.Any() && alignments.Any(a => VerifyAlignmentCompatibility(a, classNameRandomizer, levelRandomizer,
                baseRaceRandomizer, metaraceRandomizer));
        }

        public Boolean VerifyAlignmentCompatibility(Alignment alignment, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var classNames = classNameRandomizer.GetAllPossibleResults(alignment);
            var levels = levelRandomizer.GetAllPossibleResults();

            var characterClasses = GetAllCharacterClassPrototypes(classNames, levels);
            return characterClasses.Any() && characterClasses.Any(c => VerifyCharacterClassCompatibility(alignment, c, baseRaceRandomizer, metaraceRandomizer));
        }

        private IEnumerable<CharacterClass> GetAllCharacterClassPrototypes(IEnumerable<String> classNames, IEnumerable<Int32> levels)
        {
            var characterClasses = new List<CharacterClass>();

            foreach (var className in classNames)
                foreach (var level in levels)
                    characterClasses.Add(new CharacterClass { ClassName = className, Level = level });

            return characterClasses;
        }

        public Boolean VerifyCharacterClassCompatibility(Alignment alignment, CharacterClass characterClass, RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer)
        {
            var baseRaces = baseRaceRandomizer.GetAllPossible(alignment, characterClass);
            var metaraces = metaraceRandomizer.GetAllPossible(alignment, characterClass);

            if (metaraces.Count() == 1 && metaraces.First() == RaceConstants.Metaraces.Lich)
            {
                var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters);
                if (spellcasters.Contains(characterClass.ClassName) == false)
                    return false;
            }

            return baseRaces.Any() && metaraces.Any() && LevelAdjustmentsAreAllowed(baseRaces, metaraces, characterClass.Level);
        }

        private Boolean LevelAdjustmentsAreAllowed(IEnumerable<String> baseRaces, IEnumerable<String> metaraces, Int32 level)
        {
            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            var minBaseRaceLevelAdjustment = levelAdjustments.Where(kvp => baseRaces.Contains(kvp.Key)).Min(kvp => kvp.Value);
            var minMetaraceLevelAdjustment = levelAdjustments.Where(kvp => metaraces.Contains(kvp.Key)).Min(kvp => kvp.Value);

            return minBaseRaceLevelAdjustment + minMetaraceLevelAdjustment < level;
        }
    }
}