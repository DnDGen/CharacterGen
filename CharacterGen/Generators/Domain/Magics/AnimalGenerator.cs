using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
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
    public class AnimalGenerator : Generator, IAnimalGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IRaceGenerator raceGenerator;
        private IBaseRaceRandomizer animalBaseRaceRandomizer;
        private IMetaraceRandomizer noMetaraceRandomizer;
        private IAdjustmentsSelector adjustmentsSelector;
        private IAbilitiesGenerator animalAbilitiesGenerator;
        private ISetStatsRandomizer setStatsRandomizer;
        private ICombatGenerator animalCombatGenerator;

        public AnimalGenerator(ICollectionsSelector collectionsSelector, IRaceGenerator raceGenerator, IBaseRaceRandomizer animalBaseRaceRandomizer, IMetaraceRandomizer noMetaraceRandomizer,
            IAdjustmentsSelector adjustmentsSelector, IAbilitiesGenerator animalAbilitiesGenerator, ISetStatsRandomizer setStatsRandomizer, ICombatGenerator animalCombatGenerator)
        {
            this.collectionsSelector = collectionsSelector;
            this.raceGenerator = raceGenerator;
            this.animalBaseRaceRandomizer = animalBaseRaceRandomizer;
            this.noMetaraceRandomizer = noMetaraceRandomizer;
            this.adjustmentsSelector = adjustmentsSelector;
            this.animalAbilitiesGenerator = animalAbilitiesGenerator;
            this.setStatsRandomizer = setStatsRandomizer;
            this.animalCombatGenerator = animalCombatGenerator;
        }

        public Animal GenerateFrom(Alignment alignment, CharacterClass characterClass, IEnumerable<Feat> feats)
        {
            var animals = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName);

            if (animals.Any() == false)
                return null;

            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);

            var animal = new Animal();
            animal.Race = Generate<Race>(() => raceGenerator.GenerateWith(alignment, characterClass, animalBaseRaceRandomizer, noMetaraceRandomizer),
                a => characterClass.Level + levelAdjustments[a.BaseRace] > 0);

            var baseAttack = animalCombatGenerator.GenerateBaseAttackWith(characterClass, animal.Race);
            animal.Ability = animalAbilitiesGenerator.GenerateWith(characterClass, animal.Race, setStatsRandomizer, baseAttack);

            var emptyEquipment = new Equipment();
            animal.Combat = animalCombatGenerator.GenerateWith(baseAttack, characterClass, animal.Race, animal.Ability.Feats, animal.Ability.Stats, emptyEquipment);
            
            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, characterClass.Level);
            var tricks = adjustmentsSelector.SelectFrom(tableName);
            animal.Tricks = tricks[characterClass.ClassName];

            return animal;
        }
    }
}
