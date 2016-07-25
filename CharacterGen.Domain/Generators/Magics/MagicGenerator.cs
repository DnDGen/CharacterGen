﻿using CharacterGen.Abilities.Feats;
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
using TreasureGen.Items;

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

            magic.SpellsPerDay = spellsGenerator.GeneratePerDay(characterClass, stats);
            magic.KnownSpells = spellsGenerator.GenerateKnown(characterClass, stats);

            var classesThatPrepareSpells = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.PreparesSpells);
            if (classesThatPrepareSpells.Contains(characterClass.Name))
                magic.PreparedSpells = spellsGenerator.GeneratePrepared(characterClass, magic.KnownSpells, magic.SpellsPerDay);

            magic.Animal = animalGenerator.GenerateFrom(alignment, characterClass, race, feats);

            if (equipment.Armor == null && equipment.OffHand == null)
                return magic;

            var arcaneSpellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane);
            if (arcaneSpellcasters.Contains(characterClass.Name) == false)
                return magic;

            var lightArmor = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency);

            if (equipment.Armor != null && (characterClass.Name != CharacterClassConstants.Bard || lightArmor.Contains(equipment.Armor.Name) == false))
                magic.ArcaneSpellFailure += GetArcaneSpellFailure(equipment.Armor);

            if (equipment.OffHand != null && equipment.OffHand.ItemType == ItemTypeConstants.Armor && equipment.OffHand.Attributes.Contains(AttributeConstants.Shield))
                magic.ArcaneSpellFailure += GetArcaneSpellFailure(equipment.OffHand);

            return magic;
        }

        private int GetArcaneSpellFailure(Item item)
        {
            var arcaneSpellFailures = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArcaneSpellFailures);
            var arcaneSpellFailure = arcaneSpellFailures[item.Name];

            if (item.Traits.Contains(TraitConstants.Mithral))
                arcaneSpellFailure -= 10;

            return Math.Max(0, arcaneSpellFailure);
        }
    }
}