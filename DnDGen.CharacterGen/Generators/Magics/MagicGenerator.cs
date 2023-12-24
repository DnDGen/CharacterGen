using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Selectors.Collections;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Items;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Races;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using DnDGen.TreasureGen.Items;

namespace DnDGen.CharacterGen.Generators.Magics
{
    internal class MagicGenerator : IMagicGenerator
    {
        private readonly ISpellsGenerator spellsGenerator;
        private readonly IAnimalGenerator animalGenerator;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;

        public MagicGenerator(ISpellsGenerator spellsGenerator, IAnimalGenerator animalGenerator, ICollectionSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.spellsGenerator = spellsGenerator;
            this.animalGenerator = animalGenerator;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public Magic GenerateWith(Alignment alignment, CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Feat> feats, Equipment equipment)
        {
            var magic = new Magic();

            if (characterClass.Name != CharacterClassConstants.Sorcerer || race.BaseRace != RaceConstants.BaseRaces.Rakshasa)
            {
                magic = MakeSpells(magic, characterClass, abilities);
            }

            if (race.BaseRace == RaceConstants.BaseRaces.Rakshasa && characterClass.Name == CharacterClassConstants.Sorcerer)
            {
                var adjustedClass = new CharacterClass();
                adjustedClass.Name = characterClass.Name;
                adjustedClass.Level = characterClass.Level + 7;
                adjustedClass.ProhibitedFields = characterClass.ProhibitedFields;
                adjustedClass.SpecialistFields = characterClass.SpecialistFields;

                magic = MakeSpells(magic, adjustedClass, abilities);
            }
            else if (race.BaseRace == RaceConstants.BaseRaces.Rakshasa && characterClass.Name != CharacterClassConstants.Sorcerer)
            {
                var adjustedClass = new CharacterClass();
                adjustedClass.Name = CharacterClassConstants.Sorcerer;
                adjustedClass.Level = 7;

                var rakshasaMagic = new Magic();
                rakshasaMagic = MakeSpells(rakshasaMagic, adjustedClass, abilities);

                magic.SpellsPerDay = magic.SpellsPerDay.Union(rakshasaMagic.SpellsPerDay);
                magic.KnownSpells = magic.KnownSpells.Union(rakshasaMagic.KnownSpells);
                magic.PreparedSpells = magic.PreparedSpells.Union(rakshasaMagic.PreparedSpells);
            }

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

        private Magic MakeSpells(Magic magic, CharacterClass characterClass, Dictionary<string, Ability> stats)
        {
            magic.SpellsPerDay = spellsGenerator.GeneratePerDay(characterClass, stats);
            magic.KnownSpells = spellsGenerator.GenerateKnown(characterClass, stats);

            var classesThatPrepareSpells = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.PreparesSpells);
            if (classesThatPrepareSpells.Contains(characterClass.Name))
                magic.PreparedSpells = spellsGenerator.GeneratePrepared(characterClass, magic.KnownSpells, magic.SpellsPerDay);

            return magic;
        }

        private int GetArcaneSpellFailure(Item item)
        {
            var arcaneSpellFailure = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArcaneSpellFailures, item.Name);

            if (item.Traits.Contains(TraitConstants.SpecialMaterials.Mithral))
                arcaneSpellFailure -= 10;

            return Math.Max(0, arcaneSpellFailure);
        }
    }
}
