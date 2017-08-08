using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Verifiers
{
    internal class RandomizerVerifier : IRandomizerVerifier
    {
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly ICollectionsSelector collectionsSelector;

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
            var characterClassPrototypes = GetAllCharacterClassPrototypes(classNames, levels);

            return characterClassPrototypes.Any(c => VerifyCharacterClassCompatibility(alignment, c, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));
        }

        private IEnumerable<CharacterClass> GetAllCharacterClassPrototypes(IEnumerable<string> classNames, IEnumerable<int> levels)
        {
            var characterClassPrototypes = new List<CharacterClass>();

            foreach (var className in classNames)
                foreach (var level in levels)
                    characterClassPrototypes.Add(new CharacterClass { Name = className, Level = level });

            return characterClassPrototypes;
        }

        public bool VerifyCharacterClassCompatibility(Alignment alignment, CharacterClass characterClass, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var baseRaces = baseRaceRandomizer.GetAllPossible(alignment, characterClass);
            var metaraces = metaraceRandomizer.GetAllPossible(alignment, characterClass);
            var racePrototypes = GetAllRacePrototypes(baseRaces, metaraces);

            return racePrototypes.Any(r => VerifyRaceCompatibility(r, characterClass, levelRandomizer));
        }

        private IEnumerable<Race> GetAllRacePrototypes(IEnumerable<string> baseRaces, IEnumerable<string> metaraces)
        {
            var racePrototypes = new List<Race>();

            foreach (var baseRace in baseRaces)
                foreach (var metarace in metaraces)
                    racePrototypes.Add(new Race { BaseRace = baseRace, Metarace = metarace });

            return racePrototypes;
        }

        public bool VerifyRaceCompatibility(Race race, CharacterClass characterClass, ILevelRandomizer levelRandomizer)
        {
            var classPrototype = new CharacterClass();
            classPrototype.Name = characterClass.Name;
            classPrototype.Level = characterClass.Level;
            classPrototype.LevelAdjustment += adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, race.BaseRace);
            classPrototype.LevelAdjustment += adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, race.Metarace);

            var npcs = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);
            classPrototype.IsNPC = npcs.Contains(classPrototype.Name);

            if (!(levelRandomizer is ISetLevelRandomizer) || (levelRandomizer as ISetLevelRandomizer).AllowAdjustments)
            {
                classPrototype.Level -= classPrototype.LevelAdjustment;
            }

            return classPrototype.Level > 0 && classPrototype.EffectiveLevel <= 30;
        }
    }
}