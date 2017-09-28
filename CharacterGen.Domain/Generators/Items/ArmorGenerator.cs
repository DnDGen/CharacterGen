using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using CharacterGen.Races;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;
using TreasureGen.Items.Magical;
using TreasureGen.Items.Mundane;

namespace CharacterGen.Domain.Generators.Items
{
    internal class ArmorGenerator : IArmorGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly Generator generator;
        private readonly JustInTimeFactory justInTimeFactory;

        public ArmorGenerator(ICollectionSelector collectionsSelector, IPercentileSelector percentileSelector, Generator generator, JustInTimeFactory justInTimeFactory)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.justInTimeFactory = justInTimeFactory;
            this.generator = generator;
        }

        public Armor GenerateArmorFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var effectiveLevel = GetEffectiveLevel(characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            var power = percentileSelector.SelectFrom(tableName);
            var proficientArmors = GetProficientArmors(feats, ItemTypeConstants.Armor);

            if (proficientArmors.Any() == false)
                return null;

            var item = generator.Generate(
                () => GenerateArmor(power, proficientArmors),
                a => ArmorIsValid(a, characterClass, race),
                () => GenerateDefaultArmor(power, proficientArmors, race),
                a => ArmorInvalidMessage(a, characterClass, race),
                $"{power} armor from [{string.Join(", ", proficientArmors)}]");

            return item as Armor;
        }

        private Item GenerateDefaultArmor(string power, IEnumerable<string> proficientArmors, Race race)
        {
            var standardArmors = ArmorConstants.GetBaseNames();
            var standardProficientArmors = standardArmors.Intersect(proficientArmors);

            var template = new Armor();
            template.Name = collectionsSelector.SelectRandomFrom(standardProficientArmors);
            template.ItemType = ItemTypeConstants.Armor;
            template.Size = race.Size;

            if (power == PowerConstants.Mundane)
            {
                var mundaneArmorGenerator = justInTimeFactory.Build<MundaneItemGenerator>(ItemTypeConstants.Armor);
                return mundaneArmorGenerator.GenerateFrom(template, true);
            }

            template.Magic.Bonus = 1;
            var magicalArmorGenerator = justInTimeFactory.Build<MagicalItemGenerator>(ItemTypeConstants.Armor);
            return magicalArmorGenerator.GenerateFrom(template, true);
        }

        private int GetEffectiveLevel(CharacterClass characterClass)
        {
            return (int)Math.Max(1, characterClass.EffectiveLevel);
        }

        public Armor GenerateShieldFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var effectiveLevel = GetEffectiveLevel(characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            var power = percentileSelector.SelectFrom(tableName);
            var proficientShields = GetProficientArmors(feats, AttributeConstants.Shield);

            if (proficientShields.Any() == false)
                return null;

            var item = generator.Generate(
                () => GenerateArmor(power, proficientShields),
                s => ArmorIsValid(s, characterClass, race, true),
                () => GenerateDefaultArmor(power, proficientShields, race),
                s => ArmorInvalidMessage(s, characterClass, race, true),
                $"{power} shield from [{string.Join(",", proficientShields)}]");

            return item as Armor;
        }

        private bool ArmorIsValid(Item item, CharacterClass characterClass, Race race, bool isShield = false)
        {
            var invalidMessage = ArmorInvalidMessage(item, characterClass, race, isShield);

            return string.IsNullOrWhiteSpace(invalidMessage);
        }

        private string ArmorInvalidMessage(Item item, CharacterClass characterClass, Race race, bool isShield = false)
        {
            if (item == null)
                return string.Empty;

            if (!(item is Armor))
                return $"Invalid: {item.Name} is not armor";

            var armor = item as Armor;

            if (armor.Attributes.Contains(AttributeConstants.Shield) != isShield)
            {
                var negative = isShield ? string.Empty : "not ";
                return $"Invalid: {armor.Name} needs to {negative}be a shield";
            }

            if (armor.Attributes.Contains(AttributeConstants.Metal) && characterClass.Name == CharacterClassConstants.Druid)
                return $"Invalid: {armor.Name} is metal, and Druids cannot wear or use metal armor";

            if (armor.Size != race.Size)
                return $"Invalid: {armor.Name} is {armor.Size}, but {race.Size} is needed";

            return string.Empty;
        }

        private Item GenerateArmor(string power, IEnumerable<string> proficientArmors)
        {
            if (power == PowerConstants.Mundane)
            {
                var mundaneArmorGenerator = justInTimeFactory.Build<MundaneItemGenerator>(ItemTypeConstants.Armor);
                return mundaneArmorGenerator.GenerateFrom(proficientArmors);
            }

            var magicalArmorGenerator = justInTimeFactory.Build<MagicalItemGenerator>(ItemTypeConstants.Armor);
            return magicalArmorGenerator.GenerateFrom(power, proficientArmors);
        }

        private IEnumerable<string> GetProficientArmors(IEnumerable<Feat> feats, string armorType)
        {
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, armorType + GroupConstants.Proficiency);
            var proficiencyFeats = feats.Where(f => proficiencyFeatNames.Contains(f.Name));
            var proficientArmors = new List<string>();

            foreach (var feat in proficiencyFeats)
            {
                var featArmors = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feat.Name);
                proficientArmors.AddRange(featArmors);
            }

            return proficientArmors;
        }
    }
}
