using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers;
using CharacterGen.Selectors;
using CharacterGen.Tables;
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

        public bool VerifyCompatibility(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var alignments = alignmentRandomizer.GetAllPossibleResults();
            return alignments.Any() && alignments.Any(a => VerifyAlignmentCompatibility(a, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));
        }

        public bool VerifyAlignmentCompatibility(Alignment alignment, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var classNames = classNameRandomizer.GetAllPossibleResults(alignment);
            var levels = levelRandomizer.GetAllPossibleResults();

            var characterClasses = GetAllCharacterClassPrototypes(classNames, levels);

            return characterClasses.Any() && characterClasses.Any(c => VerifyCharacterClassCompatibility(alignment, c, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));
        }

        private IEnumerable<CharacterClass> GetAllCharacterClassPrototypes(IEnumerable<string> classNames, IEnumerable<int> levels)
        {
            var characterClasses = new List<CharacterClass>();

            foreach (var className in classNames)
                foreach (var level in levels)
                    characterClasses.Add(new CharacterClass { ClassName = className, Level = level });

            return characterClasses;
        }

        public bool VerifyCharacterClassCompatibility(Alignment alignment, CharacterClass characterClass, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var baseRaces = baseRaceRandomizer.GetAllPossible(alignment, characterClass);
            var metaraces = metaraceRandomizer.GetAllPossible(alignment, characterClass);

            var verified = baseRaces.Any() && metaraces.Any();

            if (levelRandomizer is ISetLevelRandomizer && (levelRandomizer as ISetLevelRandomizer).AllowAdjustments == false)
                return verified;

            return verified && LevelAdjustmentsAreAllowed(baseRaces, metaraces, characterClass.Level);
        }

        private bool LevelAdjustmentsAreAllowed(IEnumerable<string> baseRaces, IEnumerable<string> metaraces, int level)
        {
            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            var maxBaseRaceLevelAdjustment = levelAdjustments.Where(kvp => baseRaces.Contains(kvp.Key)).Max(kvp => kvp.Value);
            var maxMetaraceLevelAdjustment = levelAdjustments.Where(kvp => metaraces.Contains(kvp.Key)).Max(kvp => kvp.Value);

            return level + maxBaseRaceLevelAdjustment + maxMetaraceLevelAdjustment > 0;
        }
    }
}