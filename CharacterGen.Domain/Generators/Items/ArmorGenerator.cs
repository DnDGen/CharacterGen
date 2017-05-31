using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
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

        public ArmorGenerator(ICollectionsSelector collectionsSelector, IPercentileSelector percentileSelector, MundaneItemGenerator mundaneArmorGenerator, MagicalItemGenerator magicalArmorGenerator, Generator generator)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.mundaneArmorGenerator = mundaneArmorGenerator;
            this.magicalArmorGenerator = magicalArmorGenerator;
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
                $"{power} armor from [{string.Join(", ", proficientArmors)}]",
                a => ArmorIsValid(a, characterClass, race),
                () => GenerateDefaultArmor(power, proficientArmors, race));

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
                return mundaneArmorGenerator.GenerateFrom(template, true);
            }

            template.Magic.Bonus = 1;
            return magicalArmorGenerator.Generate(template, true);
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
                $"{power} shield from [{string.Join(",", proficientShields)}]",
                s => ArmorIsValid(s, characterClass, race, true),
                () => GenerateDefaultArmor(power, proficientShields, race));

            return item as Armor;
        }

        private bool ArmorIsValid(Item item, CharacterClass characterClass, Race race, bool isShield = false)
        {
            if (item == null)
                return true;

            if (!(item is Armor))
                return false;

            var armor = item as Armor;

            if (armor.Attributes.Contains(AttributeConstants.Shield) != isShield)
                return false;

            if (armor.Attributes.Contains(AttributeConstants.Metal) && characterClass.Name == CharacterClassConstants.Druid)
                return false;

            return armor.Size == race.Size;
        }

        private Item GenerateArmor(string power, IEnumerable<string> proficientArmors)
        {
            if (power == PowerConstants.Mundane)
                return mundaneArmorGenerator.GenerateFrom(proficientArmors);

            return magicalArmorGenerator.GenerateFromSubset(power, proficientArmors);
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
