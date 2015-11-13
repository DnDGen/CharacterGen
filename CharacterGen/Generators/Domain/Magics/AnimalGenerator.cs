using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Magics
{
    public class AnimalGenerator : IAnimalGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IRaceGenerator raceGenerator;
        private RaceRandomizer animalBaseRaceRandomizer;
        private RaceRandomizer noMetaraceRandomizer;
        private IAdjustmentsSelector adjustmentsSelector;
        private IAbilitiesGenerator animalAbilitiesGenerator;
        private ISetStatsRandomizer setStatsRandomizer;
        private ICombatGenerator animalCombatGenerator;
        private Generator generator;

        public AnimalGenerator(ICollectionsSelector collectionsSelector, IRaceGenerator raceGenerator, RaceRandomizer animalBaseRaceRandomizer, RaceRandomizer noMetaraceRandomizer,
            IAdjustmentsSelector adjustmentsSelector, IAbilitiesGenerator animalAbilitiesGenerator, ISetStatsRandomizer setStatsRandomizer, ICombatGenerator animalCombatGenerator,
            Generator generator)
        {
            this.collectionsSelector = collectionsSelector;
            this.raceGenerator = raceGenerator;
            this.animalBaseRaceRandomizer = animalBaseRaceRandomizer;
            this.noMetaraceRandomizer = noMetaraceRandomizer;
            this.adjustmentsSelector = adjustmentsSelector;
            this.animalAbilitiesGenerator = animalAbilitiesGenerator;
            this.setStatsRandomizer = setStatsRandomizer;
            this.animalCombatGenerator = animalCombatGenerator;
            this.generator = generator;
        }

        public String GenerateFrom(Alignment alignment, CharacterClass characterClass, Race race, IEnumerable<Feat> feats)
        {
            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            var effectiveCharacterClass = GetEffectiveCharacterClass(characterClass);
            var animals = AnimalsForCharacter(effectiveCharacterClass, race, levelAdjustments);

            if (animals.Any() == false)
                return String.Empty;

            var improvedFamiliars = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, FeatConstants.ImprovedFamiliar);
            var characterHasImprovedFamiliarFeat = feats.Any(f => f.Name == FeatConstants.ImprovedFamiliar);

            var animal = generator.Generate(() => collectionsSelector.SelectRandomFrom(animals),
                a => characterHasImprovedFamiliarFeat || improvedFamiliars.Contains(a) == false);

            return animal;
        }

        private CharacterClass GetEffectiveCharacterClass(CharacterClass characterClass)
        {
            if (characterClass.ClassName != CharacterClassConstants.Ranger)
                return characterClass;

            var effectiveCharacterClass = new CharacterClass();
            effectiveCharacterClass.ClassName = CharacterClassConstants.Druid;
            effectiveCharacterClass.Level = characterClass.Level / 2;

            return effectiveCharacterClass;
        }

        private IEnumerable<String> AnimalsForCharacter(CharacterClass characterClass, Race race, Dictionary<String, Int32> levelAdjustments)
        {
            var animals = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName);
            var animalsForSize = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, race.Size);
            var animalsWithinLevel = animals.Where(a => characterClass.Level + levelAdjustments[a] > 0);

            return animals.Intersect(animalsForSize).Intersect(animalsWithinLevel);
        }
    }
}
