using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
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

        public ArmorClass GenerateWith(Equipment equipment, int adjustedDexterityBonus, IEnumerable<Feat> feats, Race race)
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

        private int GetArmorBonuses(Equipment equipment)
        {
            var armorBonuses = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorBonuses);

            var armorBonus = GetArmorBonus(equipment.Armor, armorBonuses);
            var shieldBonus = GetShieldBonus(equipment.OffHand, armorBonuses);
            return armorBonus + shieldBonus;
        }

        private int GetArmorBonus(Item armor, Dictionary<string, int> armorBonuses)
        {
            if (armor == null)
                return 0;

            if (armor.Magic.Curse == CurseConstants.OppositeEffect)
                return armorBonuses[armor.Name] - armor.Magic.Bonus;
            else if (armor.Magic.Curse == CurseConstants.Delusion)
                return armorBonuses[armor.Name];

            return armorBonuses[armor.Name] + armor.Magic.Bonus;
        }

        private int GetShieldBonus(Item shield, Dictionary<string, int> armorBonuses)
        {
            if (shield == null || shield.ItemType != ItemTypeConstants.Armor || !shield.Attributes.Contains(AttributeConstants.Shield))
                return 0;

            if (shield.Magic.Curse == CurseConstants.OppositeEffect)
                return armorBonuses[shield.Name] - shield.Magic.Bonus;
            else if (shield.Magic.Curse == CurseConstants.Delusion)
                return armorBonuses[shield.Name];

            return armorBonuses[shield.Name] + shield.Magic.Bonus;
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