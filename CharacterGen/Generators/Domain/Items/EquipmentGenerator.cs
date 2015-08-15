using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using CharacterGen.Tables;
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

        public Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass)
        {
            var equipment = new Equipment();
            equipment.Treasure = treasureGenerator.GenerateAtLevel(characterClass.Level);
            equipment.PrimaryHand = weaponGenerator.GenerateFrom(feats, characterClass);

            var baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, equipment.PrimaryHand.Name);

            if (equipment.PrimaryHand.Attributes.Contains(AttributeConstants.Ammunition))
            {
                equipment.Treasure.Items = equipment.Treasure.Items.Union(new[] { equipment.PrimaryHand });
                var baseAmmunitionType = baseWeaponTypes.First();

                do
                {
                    equipment.PrimaryHand = weaponGenerator.GenerateFrom(feats, characterClass);
                    baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, equipment.PrimaryHand.Name);
                }
                while (equipment.PrimaryHand.Attributes.Contains(AttributeConstants.Ammunition) || baseWeaponTypes.Contains(baseAmmunitionType) == false);
            }

            var twoHandedWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.TwoHanded);

            if (twoHandedWeapons.Contains(baseWeaponTypes.First()))
                equipment.OffHand = equipment.PrimaryHand;

            var twoHandedFeats = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded);
            var featNames = feats.Select(f => f.Name);

            if (twoHandedFeats.Intersect(featNames).Any() && equipment.OffHand == null)
            {

            }

            equipment.Armor = armorGenerator.GenerateFrom(feats, characterClass);
            if (equipment.Armor != null && equipment.Armor.Attributes.Contains(AttributeConstants.Shield))
            {
                if (equipment.OffHand == null)
                    equipment.OffHand = equipment.Armor;

                do equipment.Armor = armorGenerator.GenerateFrom(feats, characterClass);
                while (equipment.Armor.Attributes.Contains(AttributeConstants.Shield));
            }

            return equipment;
        }
    }
}