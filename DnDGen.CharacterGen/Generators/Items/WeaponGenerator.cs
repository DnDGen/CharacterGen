using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.TreasureGen.Items;
using DnDGen.TreasureGen.Items.Magical;
using DnDGen.TreasureGen.Items.Mundane;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Items
{
    internal class WeaponGenerator : IWeaponGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly JustInTimeFactory justInTimeFactory;

        public WeaponGenerator(ICollectionSelector collectionsSelector, IPercentileSelector percentileSelector, JustInTimeFactory justInTimeFactory)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.justInTimeFactory = justInTimeFactory;
        }

        public Weapon GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var weapon = GenerateFiltered(feats, characterClass, race, w => !WeaponIsAmmunition(w));
            return weapon;
        }

        private IEnumerable<Feat> GetNonProficiencyFeatsWithWeaponFoci(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            var allWeapons = WeaponConstants.GetAllWeapons(false, false);

            var nonProficiencyFeats = feats.Except(proficiencyFeats).Where(f => f.Name != FeatConstants.WeaponFamiliarity);
            return nonProficiencyFeats.Where(f => allWeapons.Intersect(f.Foci).Any());
        }

        private IEnumerable<Feat> GetProficiencyFeatWithSpecificWeaponFocus(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            var allWeapons = WeaponConstants.GetAllWeapons(false, false);

            return proficiencyFeats.Where(f => allWeapons.Intersect(f.Foci).Any());
        }

        private IEnumerable<Feat> GetProficiencyFeatWithFocusOfAll(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            return proficiencyFeats.Where(f => f.Foci.Contains(FeatConstants.Foci.All));
        }

        private IEnumerable<Feat> GetProficiencyFeats(IEnumerable<Feat> feats)
        {
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency);
            return feats.Where(f => proficiencyFeatNames.Contains(f.Name));
        }

        public Weapon GenerateAmmunition(CharacterClass characterClass, Race race, string ammunitionType)
        {
            var ammunitions = new[] { ammunitionType };
            var ammunition = GenerateFrom(ammunitions, characterClass, race, WeaponIsAmmunition);

            return ammunition;
        }

        private bool WeaponIsAmmunition(Weapon weapon)
        {
            return weapon.Attributes.Contains(AttributeConstants.Ammunition) && !weapon.Attributes.Contains(AttributeConstants.Thrown);
        }

        public Weapon GenerateMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var weapon = GetSequentialWeapon(feats, characterClass, race, WeaponIsMelee);
            return weapon;
        }

        private Weapon GetSequentialWeapon(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, Func<Weapon, bool> furtherValidation)
        {
            //INFO: We want to verify that the requested weapon is even possible for this given sequential generation
            if (!ValidWeaponIsPossible(feats, furtherValidation))
                return null;

            var nonProficiencyFeatsWithWeaponFoci = GetNonProficiencyFeatsWithWeaponFoci(feats);
            if (ValidWeaponIsPossible(nonProficiencyFeatsWithWeaponFoci, furtherValidation))
            {
                var weapon = GenerateFiltered(feats, characterClass, race, furtherValidation);
                if (SequentialWeaponIsValid(weapon, furtherValidation))
                    return weapon;
            }

            var featSubset = feats.Except(nonProficiencyFeatsWithWeaponFoci);
            var proficiencyFeatsWithSpecificWeaponFoci = GetProficiencyFeatWithSpecificWeaponFocus(feats);
            if (ValidWeaponIsPossible(proficiencyFeatsWithSpecificWeaponFoci, furtherValidation))
            {
                var weapon = GenerateFiltered(featSubset, characterClass, race, furtherValidation);
                if (SequentialWeaponIsValid(weapon, furtherValidation))
                    return weapon;
            }

            featSubset = featSubset.Except(proficiencyFeatsWithSpecificWeaponFoci);
            if (ValidWeaponIsPossible(featSubset, furtherValidation))
            {
                var weapon = GenerateFiltered(featSubset, characterClass, race, furtherValidation);
                if (SequentialWeaponIsValid(weapon, furtherValidation))
                    return weapon;
            }

            return null;
        }

        private bool SequentialWeaponIsValid(Weapon weapon, Func<Weapon, bool> furtherValidation)
        {
            return (weapon != null && furtherValidation(weapon));
        }

        private bool WeaponIsMelee(Weapon weapon)
        {
            return weapon.Attributes.Contains(AttributeConstants.Melee);
        }

        private Weapon GenerateFiltered(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, Func<Weapon, bool> furtherValidation)
        {
            var allowedWeapons = GetAllowedWeapons(feats);

            if (allowedWeapons.Any() == false)
                return null;

            var filteredWeapon = GenerateFrom(allowedWeapons, characterClass, race, furtherValidation);
            return filteredWeapon;
        }

        private IEnumerable<string> GetAllowedWeapons(IEnumerable<Feat> feats)
        {
            var allowedWeapons = GetWeaponsFromNonProficiencyFeats(feats);

            if (allowedWeapons.Any())
                return allowedWeapons;

            allowedWeapons = GetSpecificWeaponsFromProficiencyFeats(feats);

            if (allowedWeapons.Any())
                return allowedWeapons;

            allowedWeapons = GetWeaponsFromAllProficiencyFeats(feats);

            return allowedWeapons;
        }

        private IEnumerable<string> GetWeaponsFromNonProficiencyFeats(IEnumerable<Feat> feats)
        {
            var nonProficiencyFeatsWithWeaponFoci = GetNonProficiencyFeatsWithWeaponFoci(feats);
            var allowedWeapons = nonProficiencyFeatsWithWeaponFoci.SelectMany(f => f.Foci).Distinct();
            return GetAllowedWeapons(allowedWeapons);
        }

        private IEnumerable<string> GetSpecificWeaponsFromProficiencyFeats(IEnumerable<Feat> feats)
        {
            var proficiencyFeatsWithSpecificWeaponFoci = GetProficiencyFeatWithSpecificWeaponFocus(feats);
            var allowedWeapons = proficiencyFeatsWithSpecificWeaponFoci.SelectMany(f => f.Foci).Distinct();
            return GetAllowedWeapons(allowedWeapons);
        }

        private IEnumerable<string> GetWeaponsFromAllProficiencyFeats(IEnumerable<Feat> feats)
        {
            var proficientWithAllInFeat = GetProficiencyFeatWithFocusOfAll(feats);
            var weapons = new List<string>();

            foreach (var feat in proficientWithAllInFeat)
            {
                var featWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, feat.Name);
                weapons.AddRange(featWeapons);
            }

            return GetAllowedWeapons(weapons);
        }

        private bool ValidWeaponIsPossible(IEnumerable<Feat> feats, Func<Weapon, bool> furtherValidation)
        {
            var nonProficiencyWeapons = GetWeaponsFromNonProficiencyFeats(feats);
            var specificProficiencyWeapons = GetSpecificWeaponsFromProficiencyFeats(feats);
            var allProficiencyWeapons = GetWeaponsFromAllProficiencyFeats(feats);

            var allAllowedWeapons = nonProficiencyWeapons.Union(specificProficiencyWeapons).Union(allProficiencyWeapons);
            return ValidWeaponIsPossible(allAllowedWeapons, furtherValidation);
        }

        private bool ValidWeaponIsPossible(IEnumerable<string> allowedWeapons, Func<Weapon, bool> furtherValidation)
        {
            var mundaneWeaponGenerator = justInTimeFactory.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon);

            foreach (var allowedWeapon in allowedWeapons)
            {
                var template = new Weapon();
                template.Name = allowedWeapon;

                var templatedWeapon = mundaneWeaponGenerator.Generate(template, false) as Weapon;

                if (templatedWeapon != null && furtherValidation(templatedWeapon))
                    return true;
            }

            return false;
        }

        private IEnumerable<string> GetAllowedWeapons(IEnumerable<string> source)
        {
            var weapons = WeaponConstants.GetAllWeapons(false, false);
            var allowedWeapons = source.Intersect(weapons);

            return allowedWeapons;
        }

        private Weapon GenerateFrom(IEnumerable<string> allowedWeapons, CharacterClass characterClass, Race race, Func<Weapon, bool> furtherValidation)
        {
            var effectiveLevel = (int)Math.Max(1, characterClass.EffectiveLevel);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            var power = percentileSelector.SelectFrom(tableName);

            var weapon = GenerateWeapon(power, allowedWeapons, race);

            return weapon as Weapon;
        }

        private Item GenerateWeapon(string power, IEnumerable<string> proficientWeapons, Race race)
        {
            var weaponName = collectionsSelector.SelectRandomFrom(proficientWeapons);

            if (power == PowerConstants.Mundane)
            {
                var mundaneWeaponGenerator = justInTimeFactory.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon);
                var mundaneWeapon = mundaneWeaponGenerator.Generate(weaponName, race.Size);

                return mundaneWeapon;
            }

            var magicalWeaponGenerator = justInTimeFactory.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon);
            var magicalWeapon = magicalWeaponGenerator.Generate(power, weaponName, race.Size);

            return magicalWeapon;
        }

        public Weapon GenerateOneHandedMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var weapon = GetSequentialWeapon(feats, characterClass, race, WeaponIsOneHandedMelee);
            return weapon;
        }

        private bool WeaponIsOneHandedMelee(Weapon weapon)
        {
            return WeaponIsMelee(weapon) && !weapon.Attributes.Contains(AttributeConstants.TwoHanded);
        }

        public Weapon GenerateRangedFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var weapon = GetSequentialWeapon(feats, characterClass, race, WeaponIsRanged);
            return weapon;
        }

        private bool WeaponIsRanged(Weapon weapon)
        {
            return !WeaponIsMelee(weapon) && !WeaponIsAmmunition(weapon);
        }
    }
}
