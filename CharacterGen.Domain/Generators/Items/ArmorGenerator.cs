using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
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
        private ICollectionsSelector collectionsSelector;
        private IPercentileSelector percentileSelector;
        private MundaneItemGenerator mundaneArmorGenerator;
        private MagicalItemGenerator magicalArmorGenerator;
        private Generator generator;

        public ArmorGenerator(ICollectionsSelector collectionsSelector, IPercentileSelector percentileSelector,
            MundaneItemGenerator mundaneArmorGenerator, MagicalItemGenerator magicalArmorGenerator, Generator generator)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.mundaneArmorGenerator = mundaneArmorGenerator;
            this.magicalArmorGenerator = magicalArmorGenerator;
            this.generator = generator;
        }

        public Item GenerateArmorFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var effectiveLevel = GetEffectiveLevel(characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            var power = percentileSelector.SelectFrom(tableName);
            var proficientArmors = GetProficientArmors(feats, ItemTypeConstants.Armor);

            if (proficientArmors.Any() == false)
                return null;

            return generator.Generate(() => GenerateArmor(power),
                a => ArmorIsValid(a, proficientArmors, characterClass, race),
                () => GenerateDefaultArmor(power, proficientArmors, race),
                $"{power} armor from [{string.Join(",", proficientArmors)}]");
        }

        private Item GenerateDefaultArmor(string power, IEnumerable<string> proficientArmors, Race race)
        {
            var standardArmors = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.Standard + ItemTypeConstants.Armor);
            var standardProficientArmors = standardArmors.Intersect(proficientArmors);

            var template = new Item();
            template.Name = collectionsSelector.SelectRandomFrom(standardProficientArmors);
            template.ItemType = ItemTypeConstants.Armor;

            if (power == PowerConstants.Mundane)
            {
                template.Traits.Add(race.Size);
                return mundaneArmorGenerator.Generate(template, true);
            }

            template.Magic.Bonus = 1;
            return magicalArmorGenerator.Generate(template, true);
        }

        private int GetEffectiveLevel(CharacterClass characterClass)
        {
            return (int)Math.Max(1, characterClass.EffectiveLevel);
        }

        public Item GenerateShieldFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var effectiveLevel = GetEffectiveLevel(characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            var power = percentileSelector.SelectFrom(tableName);
            var proficientShields = GetProficientArmors(feats, AttributeConstants.Shield);

            if (proficientShields.Any() == false)
                return null;

            return generator.Generate(() => GenerateArmor(power),
                s => ArmorIsValid(s, proficientShields, characterClass, race, true),
                () => GenerateDefaultArmor(power, proficientShields, race),
                $"{power} shield from [{string.Join(",", proficientShields)}]");
        }

        private bool ArmorIsValid(Item armor, IEnumerable<string> proficientArmors, CharacterClass characterClass, Race race, bool isShield = false)
        {
            if (armor == null)
                return true;

            if (armor.ItemType != ItemTypeConstants.Armor)
                return false;

            if (armor.Attributes.Contains(AttributeConstants.Shield) != isShield)
                return false;

            if (armor.Attributes.Contains(AttributeConstants.Metal) && characterClass.Name == CharacterClassConstants.Druid)
                return false;

            if (armor.IsMagical == false && armor.Traits.Contains(race.Size) == false)
                return false;

            var baseArmorType = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, armor.Name).First();
            return proficientArmors.Contains(baseArmorType);
        }

        private Item GenerateArmor(string power)
        {
            if (power == PowerConstants.Mundane)
                return mundaneArmorGenerator.Generate();

            return magicalArmorGenerator.GenerateAtPower(power);
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
