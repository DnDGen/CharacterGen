using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Items;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;
using TreasureGen.Generators;

namespace CharacterGen.Domain.Generators.Items
{
    internal class EquipmentGenerator : IEquipmentGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IArmorGenerator armorGenerator;
        private IWeaponGenerator weaponGenerator;
        private ITreasureGenerator treasureGenerator;
        private Generator generator;

        public EquipmentGenerator(ICollectionsSelector collectionsSelector, IWeaponGenerator weaponGenerator,
            ITreasureGenerator treasureGenerator, IArmorGenerator armorGenerator, Generator generator)
        {
            this.collectionsSelector = collectionsSelector;
            this.armorGenerator = armorGenerator;
            this.weaponGenerator = weaponGenerator;
            this.treasureGenerator = treasureGenerator;
            this.generator = generator;
        }

        public Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var equipment = new Equipment();

            var npcs = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);

            var effectiveLevel = characterClass.Level;
            if (npcs.Contains(characterClass.Name))
                effectiveLevel = Math.Max(effectiveLevel / 2, 1);

            equipment.Treasure = treasureGenerator.GenerateAtLevel(effectiveLevel);
            equipment.Armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);

            var twoWeaponFeats = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded);
            var hasTwoWeaponFeats = feats.Any(f => twoWeaponFeats.Contains(f.Name));

            var meleeWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Melee);
            var twoHandedWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.TwoHanded);
            var oneHandedMeleeWeapons = meleeWeapons.Except(twoHandedWeapons);
            var foci = feats.Where(f => f.Name != FeatConstants.WeaponFamiliarity).SelectMany(f => f.Foci);

            if (hasTwoWeaponFeats && foci.Intersect(oneHandedMeleeWeapons).Any())
            {
                equipment.PrimaryHand = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
                equipment.OffHand = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            }
            else
            {
                equipment.PrimaryHand = weaponGenerator.GenerateFrom(feats, characterClass, race);
            }

            var hasUnarmedStrikeFocus = foci.Any(f => f == FeatConstants.Foci.UnarmedStrike);

            if (equipment.PrimaryHand != null)
            {
                var baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, equipment.PrimaryHand.Name);

                if (WeaponRequiresAmmunition(baseWeaponTypes))
                {
                    var ammunitionType = baseWeaponTypes.Last();
                    var ammunition = weaponGenerator.GenerateAmmunition(feats, characterClass, race, ammunitionType);

                    if (ammunition != null)
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

                        var baseRangedWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, rangedWeapon.Name);

                        if (WeaponRequiresAmmunition(baseRangedWeaponTypes))
                        {
                            var ammunitionType = baseRangedWeaponTypes.Last();
                            var ammunition = weaponGenerator.GenerateAmmunition(feats, characterClass, race, ammunitionType);

                            if (ammunition != null)
                                equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { ammunition });
                        }
                    }
                }
            }

            if (equipment.OffHand == null && (equipment.PrimaryHand == null || equipment.PrimaryHand.Attributes.Contains(AttributeConstants.Melee)))
                equipment.OffHand = armorGenerator.GenerateShieldFrom(feats, characterClass, race);

            return equipment;
        }

        private bool WeaponRequiresAmmunition(IEnumerable<string> baseWeaponTypes)
        {
            return baseWeaponTypes.Count() > 1;
        }
    }
}