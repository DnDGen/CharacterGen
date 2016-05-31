using CharacterGen.Abilities.Feats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Magics
{
    internal class AnimalGenerator : IAnimalGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private Generator generator;

        public AnimalGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector, Generator generator)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.generator = generator;
        }

        public string GenerateFrom(Alignment alignment, CharacterClass characterClass, Race race, IEnumerable<Feat> feats)
        {
            if (characterClass.ClassName == CharacterClassConstants.Adept && characterClass.Level == 1)
                return string.Empty;

            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            var effectiveCharacterClass = GetEffectiveCharacterClass(characterClass);
            var animals = AnimalsForCharacter(effectiveCharacterClass, race, levelAdjustments);

            if (animals.Any() == false)
                return string.Empty;

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

        private IEnumerable<string> AnimalsForCharacter(CharacterClass characterClass, Race race, Dictionary<string, int> levelAdjustments)
        {
            var animals = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName);
            var animalsForSize = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, race.Size);
            var animalsWithinLevel = animals.Where(a => characterClass.Level + levelAdjustments[a] > 0);
            var filteredAnimals = animals.Intersect(animalsForSize).Intersect(animalsWithinLevel);

            var mages = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Mages);

            if (mages.Contains(characterClass.ClassName) == false)
                return filteredAnimals;

            var animalsForMetarace = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, race.Metarace);
            return filteredAnimals.Intersect(animalsForMetarace);
        }
    }
}
