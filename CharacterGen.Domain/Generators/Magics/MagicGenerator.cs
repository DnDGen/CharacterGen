using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Magics
{
    internal class MagicGenerator : IMagicGenerator
    {
        private ISpellsGenerator spellsGenerator;
        private IAnimalGenerator animalGenerator;
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public MagicGenerator(ISpellsGenerator spellsGenerator, IAnimalGenerator animalGenerator, ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.spellsGenerator = spellsGenerator;
            this.animalGenerator = animalGenerator;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public Magic GenerateWith(Alignment alignment, CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Feat> feats, Equipment equipment)
        {
            var magic = new Magic();
            magic.SpellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);
            magic.Animal = animalGenerator.GenerateFrom(alignment, characterClass, race, feats);

            if (equipment.Armor == null && equipment.OffHand == null)
                return magic;

            var arcaneSpellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane);
            if (arcaneSpellcasters.Contains(characterClass.ClassName) == false)
                return magic;

            var lightArmor = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency);

            if (equipment.Armor != null && (characterClass.ClassName != CharacterClassConstants.Bard || lightArmor.Contains(equipment.Armor.Name) == false))
                magic.ArcaneSpellFailure += GetArcaneSpellFailure(equipment.Armor);

            if (equipment.OffHand != null && equipment.OffHand.ItemType == TreasureGen.Common.Items.ItemTypeConstants.Armor && equipment.OffHand.Attributes.Contains(TreasureGen.Common.Items.AttributeConstants.Shield))
                magic.ArcaneSpellFailure += GetArcaneSpellFailure(equipment.OffHand);

            return magic;
        }

        private int GetArcaneSpellFailure(TreasureGen.Common.Items.Item item)
        {
            var arcaneSpellFailures = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArcaneSpellFailures);
            var arcaneSpellFailure = arcaneSpellFailures[item.Name];

            if (item.Traits.Contains(TreasureGen.Common.Items.TraitConstants.Mithral))
                arcaneSpellFailure -= 10;

            return Math.Max(0, arcaneSpellFailure);
        }
    }
}
