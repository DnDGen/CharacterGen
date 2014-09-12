using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentGen.Common.Items;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Combats
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
            var armorEnhancementBonus = equipment.Armor.Magic.Bonus;
            var shieldEnhancementBonus = GetShieldEnhancementBonus(equipment.OffHand);
            return armorBonus + armorEnhancementBonus + shieldBonus + shieldEnhancementBonus;
        }

        private Int32 GetArmorBonus(Item armor)
        {
            var armorBonuses = adjustmentsSelector.SelectFrom("ArmorBonuses");
            return armorBonuses[armor.Name];
        }

        private Int32 GetShieldBonus(Item offHand)
        {
            if (offHand.ItemType != ItemTypeConstants.Armor || !offHand.Attributes.Contains(AttributeConstants.Shield))
                return 0;

            var armorBonuses = adjustmentsSelector.SelectFrom("ArmorBonuses");
            return armorBonuses[offHand.Name];
        }

        private Int32 GetShieldEnhancementBonus(Item offHand)
        {
            if (offHand.ItemType != ItemTypeConstants.Armor || !offHand.Attributes.Contains(AttributeConstants.Shield))
                return 0;

            return offHand.Magic.Bonus;
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
            var deflectionBonuses = collectionsSelector.SelectFrom("ArmorClassModifiers", "Deflection");
            var itemsWithDeflectionBonuses = items.Where(i => deflectionBonuses.Contains(i.Name));

            if (!itemsWithDeflectionBonuses.Any())
                return 0;

            return itemsWithDeflectionBonuses.Max(i => i.Magic.Bonus);
        }

        private Int32 GetNaturalArmorBonus(IEnumerable<Item> items, IEnumerable<Feat> feats, Race race)
        {
            var thingsThatGrantNaturalArmorBonuses = collectionsSelector.SelectFrom("ArmorClassModifiers", "NaturalArmor");
            var itemsWithNaturalArmorBonuses = items.Where(i => thingsThatGrantNaturalArmorBonuses.Contains(i.Name));
            var itemNaturalArmorBonuses = itemsWithNaturalArmorBonuses.Select(i => i.Magic.Bonus);

            var featAdjustments = adjustmentsSelector.SelectFrom("FeatArmorAdjustments");
            var featsWithNaturalArmorBonuses = feats.Select(f => f.Name).Intersect(thingsThatGrantNaturalArmorBonuses);
            var featNaturalArmorAdjustments = featAdjustments.Where(kvp => featsWithNaturalArmorBonuses.Contains(kvp.Key));
            var featNaturalArmorBonuses = featNaturalArmorAdjustments.Select(kvp => kvp.Value);

            var racialBonuses = adjustmentsSelector.SelectFrom("RacialNaturalArmorBonuses");
            var racialBonus = racialBonuses[race.BaseRace] + racialBonuses[race.Metarace];
            var racialNaturalArmorBonuses = new[] { racialBonus };

            var naturalArmorBonuses = featNaturalArmorBonuses.Union(itemNaturalArmorBonuses)
                                                             .Union(racialNaturalArmorBonuses);
            if (!naturalArmorBonuses.Any())
                return 0;

            return naturalArmorBonuses.Max();
        }

        private Int32 GetDodgeBonus(IEnumerable<Feat> feats)
        {
            var featAdjustments = adjustmentsSelector.SelectFrom("FeatArmorAdjustments");
            var dodgeBonuses = collectionsSelector.SelectFrom("ArmorClassModifiers", "Dodge");
            var bonus = 0;

            foreach (var feat in feats.Select(f => f.Name))
                if (dodgeBonuses.Contains(feat))
                    bonus += featAdjustments[feat];

            return bonus;
        }
    }
}