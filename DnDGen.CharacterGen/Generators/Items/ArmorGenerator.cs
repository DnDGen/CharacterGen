using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using DnDGen.TreasureGen.Items.Magical;
using DnDGen.TreasureGen.Items.Mundane;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Items
{
    internal class ArmorGenerator : IArmorGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly JustInTimeFactory justInTimeFactory;
        private readonly Dice dice;

        public ArmorGenerator(ICollectionSelector collectionsSelector, IPercentileSelector percentileSelector, JustInTimeFactory justInTimeFactory, Dice dice)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.justInTimeFactory = justInTimeFactory;
            this.dice = dice;
        }

        public Armor GenerateArmorFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var effectiveLevel = GetEffectiveLevel(characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            var power = percentileSelector.SelectFrom(tableName);
            var proficientArmors = GetProficientArmors(feats, ItemTypeConstants.Armor, characterClass);

            if (proficientArmors.Any() == false)
                return null;

            var item = GenerateArmor(power, proficientArmors, race);

            return item as Armor;
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
            var proficientShields = GetProficientArmors(feats, AttributeConstants.Shield, characterClass);

            if (proficientShields.Any() == false)
                return null;

            var item = GenerateArmor(power, proficientShields, race);

            return item as Armor;
        }

        private Item GenerateArmor(string power, IEnumerable<string> proficientArmors, Race race)
        {


            var armorName = collectionsSelector.SelectRandomFrom(proficientArmors);

            if (power == PowerConstants.Mundane)
            {
                var mundaneArmorGenerator = justInTimeFactory.Build<MundaneItemGenerator>(ItemTypeConstants.Armor);
                return mundaneArmorGenerator.Generate(armorName, race.Size);
            }

            var magicalArmorGenerator = justInTimeFactory.Build<MagicalItemGenerator>(ItemTypeConstants.Armor);
            return magicalArmorGenerator.Generate(power, armorName, race.Size);
        }

        private IEnumerable<string> GetProficientArmors(IEnumerable<Feat> feats, string armorType, CharacterClass characterClass)
        {
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, armorType + GroupConstants.Proficiency);
            var proficiencyFeats = feats.Where(f => proficiencyFeatNames.Contains(f.Name));
            var proficientArmors = new List<string>();

            foreach (var feat in proficiencyFeats)
            {
                var featArmors = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feat.Name);
                proficientArmors.AddRange(featArmors);
            }

            //INFO: The feat proficiencies include specifics such as elven chainmail, so we need to filter just to basics
            var mundaneArmors = ArmorConstants.GetAllArmorsAndShields(false);
            var metalArmor = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Metal);

            if (characterClass.Name == CharacterClassConstants.Druid)
            {
                //INFO: Armor has a 5% chance to be made of a special material
                //There are 6 special materials: Adamantine, mithral, darkwood, dragonhide, cold iron, and alchemical silver
                //So the chance of a dragonhide armor is 0.83%
                var isSpecialMaterial = dice.Roll().Percentile().AsTrueOrFalse(.95);
                var isDragonhide = isSpecialMaterial && dice.Roll().d6().AsTrueOrFalse(6);

                if (!isSpecialMaterial || !isDragonhide)
                {
                    proficientArmors = proficientArmors.Except(metalArmor).ToList();
                }
            }

            return proficientArmors.Intersect(mundaneArmors);
        }
    }
}
