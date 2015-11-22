using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;
using TreasureGen.Generators.Items.Magical;
using TreasureGen.Generators.Items.Mundane;

namespace CharacterGen.Generators.Domain.Items
{
    public class ArmorGenerator : IArmorGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IPercentileSelector percentileSelector;
        private IMundaneItemGenerator mundaneArmorGenerator;
        private IMagicalItemGenerator magicalArmorGenerator;
        private Generator generator;

        public ArmorGenerator(ICollectionsSelector collectionsSelector, IPercentileSelector percentileSelector,
            IMundaneItemGenerator mundaneArmorGenerator, IMagicalItemGenerator magicalArmorGenerator, Generator generator)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.mundaneArmorGenerator = mundaneArmorGenerator;
            this.magicalArmorGenerator = magicalArmorGenerator;
            this.generator = generator;
        }

        public Item GenerateArmorFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            var power = percentileSelector.SelectFrom(tableName);
            var proficientArmors = GetProficientArmors(feats, ItemTypeConstants.Armor);

            if (proficientArmors.Any() == false)
                return null;

            return generator.Generate(() => GenerateArmor(power),
                a => ArmorIsValid(a, proficientArmors, characterClass, race));
        }

        public Item GenerateShieldFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            var power = percentileSelector.SelectFrom(tableName);
            var proficientShields = GetProficientArmors(feats, AttributeConstants.Shield);

            if (proficientShields.Any() == false)
                return null;

            return generator.Generate(() => GenerateArmor(power),
                s => ShieldIsValid(s, proficientShields, characterClass, race));
        }

        private Boolean ArmorIsValid(Item armor, IEnumerable<String> proficientArmors, CharacterClass characterClass, Race race)
        {
            if (armor.Attributes.Contains(AttributeConstants.Shield))
                return false;

            return IsValid(armor, proficientArmors, characterClass, race);
        }

        private Boolean ShieldIsValid(Item shield, IEnumerable<String> proficientArmors, CharacterClass characterClass, Race race)
        {
            if (shield.Attributes.Contains(AttributeConstants.Shield) == false)
                return false;

            return IsValid(shield, proficientArmors, characterClass, race);
        }

        private Boolean IsValid(Item armor, IEnumerable<String> proficientArmors, CharacterClass characterClass, Race race)
        {
            if (armor.ItemType != ItemTypeConstants.Armor)
                return false;

            if (armor.Attributes.Contains(AttributeConstants.Metal) && characterClass.ClassName == CharacterClassConstants.Druid)
                return false;

            if (armor.IsMagical == false && armor.Traits.Contains(race.Size) == false)
                return false;

            var baseArmorType = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, armor.Name).First();
            return proficientArmors.Contains(baseArmorType);
        }

        private Item GenerateArmor(String power)
        {
            if (power == PowerConstants.Mundane)
                return mundaneArmorGenerator.Generate();

            return magicalArmorGenerator.GenerateAtPower(power);
        }

        private IEnumerable<String> GetProficientArmors(IEnumerable<Feat> feats, String armorType)
        {
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, armorType + GroupConstants.Proficiency);
            var proficiencyFeats = feats.Where(f => proficiencyFeatNames.Contains(f.Name));
            var proficientArmors = new List<String>();

            foreach (var feat in proficiencyFeats)
            {
                var featArmors = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feat.Name);
                proficientArmors.AddRange(featArmors);
            }

            return proficientArmors;
        }
    }
}
