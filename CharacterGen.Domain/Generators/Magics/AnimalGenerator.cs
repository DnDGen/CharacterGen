using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using CharacterGen.Magics;
using CharacterGen.Races;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Magics
{
    internal class AnimalGenerator : IAnimalGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;

        public AnimalGenerator(ICollectionSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public string GenerateFrom(Alignment alignment, CharacterClass characterClass, Race race, IEnumerable<Feat> feats)
        {
            var effectiveCharacterClass = GetEffectiveCharacterClass(characterClass);
            if (effectiveCharacterClass.EffectiveLevel < 1)
                return string.Empty;

            var animals = AnimalsForCharacter(effectiveCharacterClass, race, feats);

            if (animals.Any() == false)
                return string.Empty;

            var animal = collectionsSelector.SelectRandomFrom(animals);
            return animal;
        }

        private CharacterClass GetEffectiveCharacterClass(CharacterClass characterClass)
        {
            if (characterClass.Name != CharacterClassConstants.Ranger)
                return characterClass;

            var effectiveCharacterClass = new CharacterClass();
            effectiveCharacterClass.Name = CharacterClassConstants.Druid;
            effectiveCharacterClass.IsNPC = true;
            effectiveCharacterClass.Level = characterClass.Level;

            return effectiveCharacterClass;
        }

        private IEnumerable<string> AnimalsForCharacter(CharacterClass characterClass, Race race, IEnumerable<Feat> feats)
        {
            var animals = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, characterClass.Name);
            var animalsForSize = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, race.Size);
            var filteredAnimals = animals.Intersect(animalsForSize);

            var minimumLevels = adjustmentsSelector.SelectAllFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            filteredAnimals = filteredAnimals.Where(a => characterClass.Level > minimumLevels[a]);

            var arcaneSpellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane);

            if (arcaneSpellcasters.Contains(characterClass.Name) == false)
                return filteredAnimals;

            var improvedFamiliars = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, FeatConstants.ImprovedFamiliar);
            var characterHasImprovedFamiliarFeat = feats.Any(f => f.Name == FeatConstants.ImprovedFamiliar);

            var animalsForMetarace = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, race.Metarace);
            filteredAnimals = filteredAnimals.Where(a => characterHasImprovedFamiliarFeat || improvedFamiliars.Contains(a) == false);

            return filteredAnimals.Intersect(animalsForMetarace);
        }
    }
}
