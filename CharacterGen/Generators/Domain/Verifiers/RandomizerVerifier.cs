using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
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
            return alignments.Any(a => VerifyAlignmentCompatibility(a, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));
        }

        public bool VerifyAlignmentCompatibility(Alignment alignment, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var classNames = classNameRandomizer.GetAllPossibleResults(alignment);
            var levels = levelRandomizer.GetAllPossibleResults();
            var characterClasses = GetAllCharacterClassPrototypes(classNames, levels);

            return characterClasses.Any(c => VerifyCharacterClassCompatibility(alignment, c, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));
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
            var races = GetAllRacePrototypes(baseRaces, metaraces);

            return races.Any(r => VerifyRaceCompatibility(r, characterClass, levelRandomizer));
        }

        private IEnumerable<Race> GetAllRacePrototypes(IEnumerable<string> baseRaces, IEnumerable<string> metaraces)
        {
            var races = new List<Race>();

            foreach (var baseRace in baseRaces)
                foreach (var metarace in metaraces)
                    races.Add(new Race { BaseRace = baseRace, Metarace = metarace });

            return races;
        }

        public bool VerifyRaceCompatibility(Race race, CharacterClass characterClass, ILevelRandomizer levelRandomizer)
        {
            if (levelRandomizer is ISetLevelRandomizer && (levelRandomizer as ISetLevelRandomizer).AllowAdjustments == false)
                return true;

            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);

            return characterClass.Level + levelAdjustments[race.BaseRace] + levelAdjustments[race.Metarace] > 0;
        }
    }
}