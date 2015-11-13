using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Generators.Domain.Combats
{
    public class ArmorClassGenerator : IterativeGenerator, IArmorClassGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public ArmorClassGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public ArmorClass GenerateWith(Equipment equipment, Int32 adjustedDexterityBonus, IEnumerable<Feat> feats, Race race)
        {
            var armorBonuses = GetArmorBonuses(equipment);
            var sizeModifier = GetSizeModifier(race);
            var deflectionBonus = GetDeflectionBonus(equipment.Treasure.Items);
            var naturalArmorBonus = GetNaturalArmorBonus(equipment.Treasure.Items, feats, race);

            var armorClass = new ArmorClass();
            armorClass.Full = 10 + armorBonuses + sizeModifier + deflectionBonus + naturalArmorBonus + adjustedDexterityBonus;
            armorClass.Touch = armorClass.Full - armorBonuses - naturalArmorBonus;
            armorClass.FlatFooted = armorClass.Full - adjustedDexterityBonus;

            return armorClass;
        }

        private Int32 GetArmorBonuses(Equipment equipment)
        {
            var armorBonuses = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorBonuses);

            var armorBonus = GetArmorBonus(equipment.Armor, armorBonuses);
            var shieldBonus = GetShieldBonus(equipment.OffHand, armorBonuses);
            return armorBonus + shieldBonus;
        }

        private Int32 GetArmorBonus(Item armor, Dictionary<String, Int32> armorBonuses)
        {
            if (armor == null)
                return 0;

            return armorBonuses[armor.Name] + armor.Magic.Bonus;
        }

        private Int32 GetShieldBonus(Item offHand, Dictionary<String, Int32> armorBonuses)
        {
            if (offHand == null || offHand.ItemType != ItemTypeConstants.Armor || !offHand.Attributes.Contains(AttributeConstants.Shield))
                return 0;

            return armorBonuses[offHand.Name] + offHand.Magic.Bonus;
        }

        private Int32 GetSizeModifier(Race race)
        {
            if (race.Size == RaceConstants.Sizes.Large)
                return -1;
            else if (race.Size == RaceConstants.Sizes.Small)
                return 1;

            return 0;
        }

        private Int32 GetDeflectionBonus(IEnumerable<Item> items)
        {
            if (items.Any() == false)
                return 0;

            var deflectionBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection);
            var itemsWithDeflectionBonuses = items.Where(i => deflectionBonuses.Contains(i.Name));

            if (itemsWithDeflectionBonuses.Any() == false)
                return 0;

            return itemsWithDeflectionBonuses.Max(i => i.Magic.Bonus);
        }

        private Int32 GetNaturalArmorBonus(IEnumerable<Item> items, IEnumerable<Feat> feats, Race race)
        {
            var thingsThatGrantNaturalArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers,
                GroupConstants.NaturalArmor);
            var itemsWithNaturalArmorBonuses = items.Where(i => thingsThatGrantNaturalArmorBonuses.Contains(i.Name));
            var itemNaturalArmorBonuses = itemsWithNaturalArmorBonuses.Select(i => i.Magic.Bonus);

            var featsWithNaturalArmorBonuses = feats.Where(f => thingsThatGrantNaturalArmorBonuses.Contains(f.Name) && String.IsNullOrEmpty(f.Focus));
            var featNaturalArmorBonuses = featsWithNaturalArmorBonuses.Select(f => f.Strength);
            var featNaturalArmorBonus = featNaturalArmorBonuses.Sum();

            if (itemNaturalArmorBonuses.Any() == false)
                return featNaturalArmorBonus;

            return featNaturalArmorBonus + itemNaturalArmorBonuses.Max();
        }
    }
}