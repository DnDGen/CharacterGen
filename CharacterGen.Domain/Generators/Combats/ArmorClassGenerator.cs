using CharacterGen.Abilities.Feats;
using CharacterGen.Combats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Items;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;
using TreasureGen.Items.Magical;

namespace CharacterGen.Domain.Generators.Combats
{
    internal class ArmorClassGenerator : IArmorClassGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public ArmorClassGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public ArmorClass GenerateWith(Equipment equipment, int adjustedDexterityBonus, IEnumerable<Feat> feats, Race race)
        {
            var armorBonuses = GetArmorBonus(equipment);
            var sizeModifier = GetSizeModifier(race);
            var deflectionBonus = GetDeflectionBonus(equipment.Treasure.Items);
            var naturalArmorBonus = GetNaturalArmorBonus(equipment.Treasure.Items, feats, race);
            var dodgeBonus = GetDodgeBonus(feats);

            var armorClass = new ArmorClass();
            armorClass.Full = 10 + armorBonuses + sizeModifier + deflectionBonus + naturalArmorBonus + adjustedDexterityBonus + dodgeBonus;
            armorClass.Touch = armorClass.Full - armorBonuses - naturalArmorBonus;
            armorClass.FlatFooted = armorClass.Full - adjustedDexterityBonus - dodgeBonus;

            var circumstantialDodgeBonus = IsDodgeBonusCircumstantial(feats);
            var circumstantialNaturalArmorBonus = IsNaturalArmorBonusCircumstantial(feats);

            armorClass.CircumstantialBonus = circumstantialDodgeBonus || circumstantialNaturalArmorBonus;

            return armorClass;
        }

        private bool IsNaturalArmorBonusCircumstantial(IEnumerable<Feat> feats)
        {
            var thingsThatGrantNaturalArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor);
            var featsWithNaturalArmorBonuses = feats.Where(f => thingsThatGrantNaturalArmorBonuses.Contains(f.Name));

            return featsWithNaturalArmorBonuses.Any(f => f.Foci.Any());
        }

        private bool IsDodgeBonusCircumstantial(IEnumerable<Feat> feats)
        {
            var deflectionBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.DodgeBonus);
            var featsWithDeflectionBonuses = feats.Where(f => deflectionBonuses.Contains(f.Name));

            return featsWithDeflectionBonuses.Any(f => f.Foci.Any());
        }

        private int GetDodgeBonus(IEnumerable<Feat> feats)
        {
            var deflectionBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.DodgeBonus);
            var featsWithDeflectionBonuses = feats.Where(f => deflectionBonuses.Contains(f.Name) && f.Foci.Any() == false);

            if (featsWithDeflectionBonuses.Any() == false)
                return 0;

            return featsWithDeflectionBonuses.Sum(i => i.Power);
        }

        private int GetArmorBonus(Equipment equipment)
        {
            var armorBonuses = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorBonuses);

            var armorBonus = GetArmorBonus(equipment, armorBonuses);
            var shieldBonus = 0;

            if (equipment.OffHand != null && equipment.OffHand.ItemType == ItemTypeConstants.Armor && equipment.OffHand.Attributes.Contains(AttributeConstants.Shield))
                shieldBonus += GetTotalBonus(equipment.OffHand, armorBonuses);

            return armorBonus + shieldBonus;
        }

        private int GetArmorBonus(Equipment equipment, Dictionary<string, int> armorBonuses)
        {
            var armorItemBonus = 0;

            if (equipment.Armor != null)
                armorItemBonus += GetTotalBonus(equipment.Armor, armorBonuses);

            var itemNamesGrantingArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus);
            var itemsGrantingArmorBonus = equipment.Treasure.Items.Where(i => itemNamesGrantingArmorBonuses.Contains(i.Name));

            if (itemsGrantingArmorBonus.Any() == false)
                return armorItemBonus;

            var maxItemArmorBonus = itemsGrantingArmorBonus.Max(i => i.Magic.Bonus);

            return Math.Max(armorItemBonus, maxItemArmorBonus);
        }

        private int GetTotalBonus(Item item, Dictionary<string, int> armorBonuses)
        {
            if (item.Magic.Curse == CurseConstants.OppositeEffect)
                return armorBonuses[item.Name] - item.Magic.Bonus;
            else if (item.Magic.Curse == CurseConstants.Delusion)
                return armorBonuses[item.Name];

            return armorBonuses[item.Name] + item.Magic.Bonus;
        }

        private int GetSizeModifier(Race race)
        {
            if (race.Size == RaceConstants.Sizes.Large)
                return -1;
            else if (race.Size == RaceConstants.Sizes.Small)
                return 1;

            return 0;
        }

        private int GetDeflectionBonus(IEnumerable<Item> items)
        {
            if (items.Any() == false)
                return 0;

            var deflectionBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection);
            var itemsWithDeflectionBonuses = items.Where(i => deflectionBonuses.Contains(i.Name));

            if (itemsWithDeflectionBonuses.Any() == false)
                return 0;

            return itemsWithDeflectionBonuses.Max(i => i.Magic.Bonus);
        }

        private int GetNaturalArmorBonus(IEnumerable<Item> items, IEnumerable<Feat> feats, Race race)
        {
            var thingsThatGrantNaturalArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor);
            var itemsWithNaturalArmorBonuses = items.Where(i => thingsThatGrantNaturalArmorBonuses.Contains(i.Name));
            var itemNaturalArmorBonuses = itemsWithNaturalArmorBonuses.Select(i => i.Magic.Bonus);

            var featsWithNaturalArmorBonuses = feats.Where(f => thingsThatGrantNaturalArmorBonuses.Contains(f.Name) && f.Foci.Any() == false);
            var featNaturalArmorBonuses = featsWithNaturalArmorBonuses.Select(f => f.Power);
            var featNaturalArmorBonus = featNaturalArmorBonuses.Sum();

            if (itemNaturalArmorBonuses.Any() == false)
                return featNaturalArmorBonus;

            return featNaturalArmorBonus + itemNaturalArmorBonuses.Max();
        }
    }
}