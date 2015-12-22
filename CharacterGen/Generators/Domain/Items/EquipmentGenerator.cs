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
            if (npcs.Contains(characterClass.ClassName))
                effectiveLevel = Math.Max(effectiveLevel / 2, 1);

            equipment.Treasure = treasureGenerator.GenerateAtLevel(effectiveLevel);
            equipment.Armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);

            var twoWeaponFeats = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded);
            var hasTwoWeaponFeats = feats.Any(f => twoWeaponFeats.Contains(f.Name));

            if (hasTwoWeaponFeats)
            {
                equipment.PrimaryHand = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
                equipment.OffHand = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            }
            else
            {
                equipment.PrimaryHand = weaponGenerator.GenerateFrom(feats, characterClass, race);
            }

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

            if (equipment.OffHand == null && equipment.PrimaryHand.Attributes.Contains(AttributeConstants.Melee))
                equipment.OffHand = armorGenerator.GenerateShieldFrom(feats, characterClass, race);

            return equipment;
        }

        private Boolean WeaponRequiresAmmunition(IEnumerable<String> baseWeaponTypes)
        {
            return baseWeaponTypes.Count() > 1;
        }
    }
}