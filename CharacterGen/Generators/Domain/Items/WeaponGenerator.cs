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
    public class WeaponGenerator : IterativeBuilder, GearGenerator
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

        public Item GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            var power = percentileSelector.SelectFrom(tableName);
            var allowedWeapons = GetAllowedWeapons(feats);

            if (allowedWeapons.Any() == false)
            {
                var message = "No weapons are allowed, which should never happen";
                message += String.Format("\nClass: {0}", characterClass.ClassName);
                message += String.Format("\nRace: {0}", race.BaseRace);
                var featNames = String.Join(", ", feats.Select(f => String.Format("{0} ({1})", f.Name, f.Focus)));
                message += String.Format("\nFeats: {0}", featNames);

                throw new ArgumentException(message);
            }

            var additionalWeapons = new List<String>();

            foreach (var allowedWeapon in allowedWeapons)
            {
                var baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, allowedWeapon);
                additionalWeapons.AddRange(baseWeaponTypes);
            }

            allowedWeapons = allowedWeapons.Union(additionalWeapons);

            var weapon = Build(() => GenerateWeapon(power), w => WeaponIsValid(w, allowedWeapons, race));

            if (weapon != null)
                return weapon;

            weapon = Build(() => GenerateWeapon(PowerConstants.Mundane), w => WeaponIsValid(w, allowedWeapons, race));

            if (weapon != null)
                return weapon;

            return BuildClub(race);
        }

        private Item BuildClub(Race race)
        {
            var club = new Item();
            club.Attributes = new[] { AttributeConstants.Bludgeoning, AttributeConstants.Common, AttributeConstants.Melee, AttributeConstants.Wood };
            club.ItemType = ItemTypeConstants.Weapon;
            club.Name = WeaponConstants.Club;
            club.Traits.Add(race.Size);

            return club;
        }

        private Boolean WeaponIsValid(Item weapon, IEnumerable<String> allowedWeapons, Race race)
        {
            if (weapon.ItemType != ItemTypeConstants.Weapon)
                return false;

            if (weapon.IsMagical == false && weapon.Traits.Contains(race.Size) == false)
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
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency);
            var proficiencyFeats = feats.Where(f => proficiencyFeatNames.Contains(f.Name));
            var allWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.Weapons);

            var nonProficiencyFoci = feats.Except(proficiencyFeats).Select(f => f.Focus);
            var specialWeaponFoci = nonProficiencyFoci.Intersect(allWeapons);

            if (specialWeaponFoci.Any())
                return specialWeaponFoci;

            var proficientWeapons = proficiencyFeats.Select(f => f.Focus).Intersect(allWeapons);

            if (proficientWeapons.Any())
                return proficientWeapons;

            var proficientWithAllInFeat = proficiencyFeats.Where(f => f.Focus == FeatConstants.Foci.All);

            foreach (var feat in proficientWithAllInFeat)
            {
                var featWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, feat.Name);
                proficientWeapons = proficientWeapons.Union(featWeapons);
            }

            return proficientWeapons;
        }
    }
}
