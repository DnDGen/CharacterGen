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
    public class ArmorGenerator : IterativeBuilder, GearGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IPercentileSelector percentileSelector;
        private IMundaneItemGenerator mundaneArmorGenerator;
        private IMagicalItemGenerator magicalArmorGenerator;

        public ArmorGenerator(ICollectionsSelector collectionsSelector, IPercentileSelector percentileSelector,
            IMundaneItemGenerator mundaneArmorGenerator, IMagicalItemGenerator magicalArmorGenerator)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.mundaneArmorGenerator = mundaneArmorGenerator;
            this.magicalArmorGenerator = magicalArmorGenerator;
        }

        public Item GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            var power = percentileSelector.SelectFrom(tableName);
            var proficientArmors = GetProficientArmors(feats);

            if (proficientArmors.Any() == false)
                return null;

            return Build<Item>(
                () => GenerateArmor(power),
                a => ArmorIsValid(a, proficientArmors, characterClass, race));
        }

        private Boolean ArmorIsValid(Item armor, IEnumerable<String> proficientArmors, CharacterClass characterClass, Race race)
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

        private IEnumerable<String> GetProficientArmors(IEnumerable<Feat> feats)
        {
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Armor + GroupConstants.Proficiency);
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
