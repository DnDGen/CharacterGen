using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using CharacterGen.Items;
using CharacterGen.Races;
using DnDGen.Core.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Generators;
using TreasureGen.Items;

namespace CharacterGen.Domain.Generators.Items
{
    internal class EquipmentGenerator : IEquipmentGenerator
    {
        private readonly ICollectionsSelector collectionsSelector;
        private readonly IArmorGenerator armorGenerator;
        private readonly IWeaponGenerator weaponGenerator;
        private readonly ITreasureGenerator treasureGenerator;

        public EquipmentGenerator(ICollectionsSelector collectionsSelector, IWeaponGenerator weaponGenerator, ITreasureGenerator treasureGenerator, IArmorGenerator armorGenerator)
        {
            this.collectionsSelector = collectionsSelector;
            this.armorGenerator = armorGenerator;
            this.weaponGenerator = weaponGenerator;
            this.treasureGenerator = treasureGenerator;
        }

        public Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var equipment = new Equipment();
            var effectiveLevel = (int)Math.Max(1, characterClass.EffectiveLevel);

            equipment.Treasure = treasureGenerator.GenerateAtLevel(effectiveLevel);
            equipment.Armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);

            var twoWeaponFeats = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded);
            var hasTwoWeaponFeats = feats.Any(f => twoWeaponFeats.Contains(f.Name));

            if (hasTwoWeaponFeats)
            {
                equipment.PrimaryHand = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
                equipment.OffHand = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            }

            if (equipment.PrimaryHand == null)
            {
                equipment.PrimaryHand = weaponGenerator.GenerateFrom(feats, characterClass, race);
            }

            if (equipment.PrimaryHand != null)
            {
                if (!string.IsNullOrEmpty(equipment.PrimaryHand.Ammunition))
                {
                    var ammunition = weaponGenerator.GenerateAmmunition(characterClass, race, equipment.PrimaryHand.Ammunition);
                    equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { ammunition });
                }

                if (equipment.PrimaryHand.Attributes.Contains(AttributeConstants.TwoHanded))
                    equipment.OffHand = equipment.PrimaryHand;

                if (equipment.PrimaryHand.Attributes.Contains(AttributeConstants.Melee) == false)
                {
                    var meleeWeapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
                    if (meleeWeapon != null)
                    {
                        equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { meleeWeapon });

                        if (meleeWeapon.Attributes.Contains(AttributeConstants.TwoHanded) == false)
                        {
                            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
                            if (shield != null)
                                equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { shield });
                        }
                    }
                }
                else
                {
                    var rangedWeapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
                    if (rangedWeapon != null)
                    {
                        equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { rangedWeapon });

                        if (!string.IsNullOrEmpty(rangedWeapon.Ammunition))
                        {
                            var ammunition = weaponGenerator.GenerateAmmunition(characterClass, race, rangedWeapon.Ammunition);
                            equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { ammunition });
                        }
                    }
                }
            }

            if (equipment.OffHand == null && (equipment.PrimaryHand == null || equipment.PrimaryHand.Attributes.Contains(AttributeConstants.Melee)))
                equipment.OffHand = armorGenerator.GenerateShieldFrom(feats, characterClass, race);

            return equipment;
        }

        private string GetRequiredAmmunition(Weapon weapon)
        {
            var arrowWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, WeaponConstants.Arrow);
            if (arrowWeapons.Any(w => weapon.NameMatches(w)))
                return WeaponConstants.Arrow;

            var crossbowWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, WeaponConstants.CrossbowBolt);
            if (crossbowWeapons.Any(w => weapon.NameMatches(w)))
                return WeaponConstants.CrossbowBolt;

            var slingWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, WeaponConstants.SlingBullet);
            if (slingWeapons.Any(w => weapon.NameMatches(w)))
                return WeaponConstants.SlingBullet;

            return string.Empty;
        }
    }
}