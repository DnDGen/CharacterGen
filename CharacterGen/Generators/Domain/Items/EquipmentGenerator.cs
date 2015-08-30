using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;
using TreasureGen.Generators;

namespace CharacterGen.Generators.Domain.Items
{
    public class EquipmentGenerator : IEquipmentGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private GearGenerator armorGenerator;
        private GearGenerator weaponGenerator;
        private ITreasureGenerator treasureGenerator;

        public EquipmentGenerator(ICollectionsSelector collectionsSelector, GearGenerator weaponGenerator,
            ITreasureGenerator treasureGenerator, GearGenerator armorGenerator)
        {
            this.collectionsSelector = collectionsSelector;
            this.armorGenerator = armorGenerator;
            this.weaponGenerator = weaponGenerator;
            this.treasureGenerator = treasureGenerator;
        }

        public Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var equipment = new Equipment();
            equipment.Treasure = treasureGenerator.GenerateAtLevel(characterClass.Level);
            equipment.PrimaryHand = weaponGenerator.GenerateFrom(feats, characterClass, race);

            var baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, equipment.PrimaryHand.Name);

            if (WeaponRequiresAmmunition(baseWeaponTypes))
            {
                var ammunition = GenerateAmmunition(feats, characterClass, race, baseWeaponTypes);
                equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { ammunition });
            }

            if (equipment.PrimaryHand.Attributes.Contains(AttributeConstants.Ammunition))
            {
                equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { equipment.PrimaryHand });
                var baseAmmunitionType = baseWeaponTypes.First();
                equipment.PrimaryHand = GenerateWeaponForAmmunition(feats, characterClass, race, baseAmmunitionType);
                baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, equipment.PrimaryHand.Name);
            }

            var twoHandedWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.TwoHanded);

            if (twoHandedWeapons.Contains(baseWeaponTypes.First()))
                equipment.OffHand = equipment.PrimaryHand;

            var twoHandedFeats = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded);
            var featNames = feats.Select(f => f.Name);

            if (twoHandedFeats.Intersect(featNames).Any() && equipment.OffHand == null)
                equipment.OffHand = GenerateOffHandWeapon(feats, characterClass, race, twoHandedWeapons);

            if (equipment.PrimaryHand.Attributes.Contains(AttributeConstants.Melee) == false)
            {
                var meleeWeapon = GenerateMeleeWeapon(feats, characterClass, race);
                equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { meleeWeapon });
            }

            equipment.Armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            if (equipment.Armor != null && equipment.Armor.Attributes.Contains(AttributeConstants.Shield))
            {
                if (equipment.OffHand == null)
                    equipment.OffHand = equipment.Armor;

                do equipment.Armor = armorGenerator.GenerateFrom(feats, characterClass, race);
                while (equipment.Armor.Attributes.Contains(AttributeConstants.Shield));
            }

            return equipment;
        }

        private Boolean WeaponRequiresAmmunition(IEnumerable<String> baseWeaponTypes)
        {
            return baseWeaponTypes.Count() > 1;
        }

        private Item GenerateAmmunition(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, IEnumerable<String> baseWeaponTypes)
        {
            Item ammunition;
            var baseAmmunitionType = String.Empty;

            do
            {
                ammunition = weaponGenerator.GenerateFrom(feats, characterClass, race);
                baseAmmunitionType = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, ammunition.Name).First();
            }
            while (ammunition.Attributes.Contains(AttributeConstants.Ammunition) == false || baseWeaponTypes.Contains(baseAmmunitionType) == false);

            return ammunition;
        }

        private Item GenerateWeaponForAmmunition(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, String baseAmmunitionType)
        {
            Item weapon;
            var baseWeaponTypes = Enumerable.Empty<String>();

            do
            {
                weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
                baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, weapon.Name);
            }
            while (weapon.Attributes.Contains(AttributeConstants.Ammunition) || baseWeaponTypes.Contains(baseAmmunitionType) == false);

            return weapon;
        }

        private Item GenerateOffHandWeapon(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, IEnumerable<String> twoHandedWeapons)
        {
            Item weapon;
            var baseWeaponTypes = Enumerable.Empty<String>();

            do
            {
                weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
                baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, weapon.Name);
            }
            while (weapon.Attributes.Contains(AttributeConstants.Ammunition) || twoHandedWeapons.Contains(baseWeaponTypes.First()));

            return weapon;
        }

        private Item GenerateMeleeWeapon(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var proficiencyFeats = feats.Where(f => f.Focus == ProficiencyConstants.All);

            if (proficiencyFeats.Any() == false)
            {
                var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency);
                proficiencyFeats = feats.Where(f => proficiencyFeatNames.Contains(f.Name));
            }

            return weaponGenerator.GenerateFrom(proficiencyFeats, characterClass, race);
        }
    }
}