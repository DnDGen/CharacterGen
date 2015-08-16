using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
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
    public class WeaponGenerator : GearGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IPercentileSelector percentileSelector;
        private IMundaneItemGenerator mundaneWeaponGenerator;
        private IMagicalItemGenerator magicalWeaponGenerator;

        public WeaponGenerator(ICollectionsSelector collectionsSelector, IPercentileSelector percentileSelector,
            IMundaneItemGenerator mundaneWeaponGenerator, IMagicalItemGenerator magicalWeaponGenerator)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.mundaneWeaponGenerator = mundaneWeaponGenerator;
            this.magicalWeaponGenerator = magicalWeaponGenerator;
        }

        public Item GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            var power = percentileSelector.SelectFrom(tableName);
            var allowedWeapons = GetAllowedWeapons(feats);

            if (allowedWeapons.Any() == false)
                throw new Exception("There are no valid weapons, which should never happen.");

            Item weapon;

            do weapon = GenerateWeapon(power);
            while (WeaponIsValid(weapon, allowedWeapons) == false);

            return weapon;
        }

        private Boolean WeaponIsValid(Item weapon, IEnumerable<String> allowedWeapons)
        {
            if (weapon.ItemType != ItemTypeConstants.Weapon)
                return false;

            var baseWeaponType = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, weapon.Name).First();
            return allowedWeapons.Contains(baseWeaponType);
        }

        private Item GenerateWeapon(String power)
        {
            if (power == PowerConstants.Mundane)
                return mundaneWeaponGenerator.Generate();

            return magicalWeaponGenerator.GenerateAtPower(power);
        }

        private IEnumerable<String> GetAllowedWeapons(IEnumerable<Feat> feats)
        {
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency);
            var proficiencyFeats = feats.Where(f => proficiencyFeatNames.Contains(f.Name));
            var allWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.Weapons);

            var nonProficiencyFoci = feats.Except(proficiencyFeats).Select(f => f.Focus);
            var specialWeaponFoci = nonProficiencyFoci.Intersect(allWeapons);

            if (specialWeaponFoci.Any())
                return specialWeaponFoci;

            var proficientWeapons = proficiencyFeats.Select(f => f.Focus).Intersect(allWeapons);

            if (proficientWeapons.Any())
                return proficientWeapons;

            var proficientWithAllInFeat = proficiencyFeats.Where(f => f.Focus == ProficiencyConstants.All);
            foreach (var feat in proficientWithAllInFeat)
            {
                var featWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, feat.Name);
                proficientWeapons = proficientWeapons.Union(featWeapons);
            }

            return proficientWeapons;
        }
    }
}
