using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Factories;
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
    internal class WeaponGenerator : IWeaponGenerator
    {
        private readonly ICollectionsSelector collectionsSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly Generator generator;
        private readonly JustInTimeFactory justInTimeFactory;

        public WeaponGenerator(ICollectionsSelector collectionsSelector, IPercentileSelector percentileSelector, Generator generator, JustInTimeFactory justInTimeFactory)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.generator = generator;
            this.justInTimeFactory = justInTimeFactory;
        }

        public Weapon GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var weapon = GenerateFiltered(feats, characterClass, race, w => !WeaponIsAmmunition(w));
            return weapon as Weapon;
        }

        private IEnumerable<Feat> GetNonProficiencyFeatsWithWeaponFoci(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            var allWeapons = WeaponConstants.GetBaseNames();

            var nonProficiencyFeats = feats.Except(proficiencyFeats).Where(f => f.Name != FeatConstants.WeaponFamiliarity);
            return nonProficiencyFeats.Where(f => allWeapons.Intersect(f.Foci).Any());
        }

        private IEnumerable<Feat> GetProficiencyFeatWithSpecificWeaponFocus(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            var allWeapons = WeaponConstants.GetBaseNames();

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

                var templatedWeapon = mundaneWeaponGenerator.GenerateFrom(template, false) as Weapon;

                if (templatedWeapon != null && furtherValidation(templatedWeapon))
                    return true;
            }

            return false;
        }

        private IEnumerable<string> GetAllowedWeapons(IEnumerable<string> source)
        {
            var weapons = WeaponConstants.GetBaseNames();
            var allowedWeapons = source.Intersect(weapons);

            return allowedWeapons;
        }

        private Weapon GenerateFrom(IEnumerable<string> allowedWeapons, CharacterClass characterClass, Race race, Func<Weapon, bool> furtherValidation)
        {
            var effectiveLevel = (int)Math.Max(1, characterClass.EffectiveLevel);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            var power = percentileSelector.SelectFrom(tableName);

            var weapon = generator.Generate(
                () => GenerateWeapon(power, allowedWeapons),
                $"{power} weapon from [{string.Join(",", allowedWeapons)}]",
                w => WeaponIsValid(w, race, furtherValidation),
                () => GenerateDefaultWeapon(power, allowedWeapons, race));

            return weapon as Weapon;
        }

        private Item GenerateDefaultWeapon(string power, IEnumerable<string> allowedWeapons, Race race)
        {
            var template = new Weapon();
            template.ItemType = ItemTypeConstants.Weapon;
            template.Name = collectionsSelector.SelectRandomFrom(allowedWeapons);
            template.Size = race.Size;

            if (power == PowerConstants.Mundane)
            {
                var mundaneWeaponGenerator = justInTimeFactory.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon);
                return mundaneWeaponGenerator.GenerateFrom(template, true);
            }

            template.Magic.Bonus = 1;
            var magicalWeaponGenerator = justInTimeFactory.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon);
            return magicalWeaponGenerator.Generate(template, true);
        }

        private Item GenerateWeapon(string power, IEnumerable<string> proficientWeapons)
        {
            if (power == PowerConstants.Mundane)
            {
                var mundaneWeaponGenerator = justInTimeFactory.Build<MundaneItemGenerator>(ItemTypeConstants.Weapon);
                var mundaneWeapon = mundaneWeaponGenerator.GenerateFrom(proficientWeapons);

                return mundaneWeapon;
            }

            var magicalWeaponGenerator = justInTimeFactory.Build<MagicalItemGenerator>(ItemTypeConstants.Weapon);
            var magicalWeapon = magicalWeaponGenerator.GenerateFromSubset(power, proficientWeapons);

            return magicalWeapon;
        }

        private bool WeaponIsValid(Item item, Race race, Func<Weapon, bool> furtherValidation)
        {
            if (item == null)
                return true;

            if (!(item is Weapon))
                return false;

            var weapon = item as Weapon;

            if (weapon.Size != race.Size)
                return false;

            var isValid = furtherValidation(weapon);

            return isValid;
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
