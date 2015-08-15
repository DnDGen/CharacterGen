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
    public class ArmorClassGenerator : IArmorClassGenerator
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
            var dodgeBonus = GetDodgeBonus(feats);

            var armorClass = new ArmorClass();
            armorClass.Full = 10 + armorBonuses + sizeModifier + deflectionBonus + naturalArmorBonus + adjustedDexterityBonus + dodgeBonus;
            armorClass.Touch = armorClass.Full - armorBonuses - naturalArmorBonus;
            armorClass.FlatFooted = armorClass.Full - adjustedDexterityBonus - dodgeBonus;

            return armorClass;
        }

        private Int32 GetArmorBonuses(Equipment equipment)
        {
            var armorBonus = GetArmorBonus(equipment.Armor);
            var shieldBonus = GetShieldBonus(equipment.OffHand);
            return armorBonus + shieldBonus;
        }

        private Int32 GetArmorBonus(Item armor)
        {
            if (armor == null)
                return 0;

            var armorBonuses = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorBonuses);
            return armorBonuses[armor.Name] + armor.Magic.Bonus;
        }

        private Int32 GetShieldBonus(Item offHand)
        {
            if (offHand == null || offHand.ItemType != ItemTypeConstants.Armor || !offHand.Attributes.Contains(AttributeConstants.Shield))
                return 0;

            var armorBonuses = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorBonuses);
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
            var deflectionBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection);
            var itemsWithDeflectionBonuses = items.Where(i => deflectionBonuses.Contains(i.Name));

            if (!itemsWithDeflectionBonuses.Any())
                return 0;

            return itemsWithDeflectionBonuses.Max(i => i.Magic.Bonus);
        }

        private Int32 GetNaturalArmorBonus(IEnumerable<Item> items, IEnumerable<Feat> feats, Race race)
        {
            var thingsThatGrantNaturalArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers,
                GroupConstants.NaturalArmor);
            var itemsWithNaturalArmorBonuses = items.Where(i => thingsThatGrantNaturalArmorBonuses.Contains(i.Name));
            var itemNaturalArmorBonuses = itemsWithNaturalArmorBonuses.Select(i => i.Magic.Bonus);

            var featsWithNaturalArmorBonuses = feats.Where(f => thingsThatGrantNaturalArmorBonuses.Contains(f.Name));
            var featNaturalArmorBonuses = featsWithNaturalArmorBonuses.Select(f => f.Strength);
            var featNaturalArmorBonus = featNaturalArmorBonuses.Sum();

            var naturalArmorBonuses = itemNaturalArmorBonuses.Union(new[] { featNaturalArmorBonus });
            if (naturalArmorBonuses.Any() == false)
                return 0;

            return naturalArmorBonuses.Max();
        }

        private Int32 GetDodgeBonus(IEnumerable<Feat> feats)
        {
            var dodgeBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers,
                GroupConstants.Dodge);
            var dodgeFeats = feats.Where(f => dodgeBonuses.Contains(f.Name));

            return dodgeFeats.Sum(f => f.Strength);
        }
    }
}