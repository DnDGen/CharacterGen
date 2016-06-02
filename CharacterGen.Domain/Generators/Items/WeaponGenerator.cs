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
    internal class WeaponGenerator : IWeaponGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IPercentileSelector percentileSelector;
        private MundaneItemGenerator mundaneWeaponGenerator;
        private MagicalItemGenerator magicalWeaponGenerator;
        private Generator generator;

        public WeaponGenerator(ICollectionsSelector collectionsSelector, IPercentileSelector percentileSelector,
            MundaneItemGenerator mundaneWeaponGenerator, MagicalItemGenerator magicalWeaponGenerator, Generator generator)
        {
            this.collectionsSelector = collectionsSelector;
            this.percentileSelector = percentileSelector;
            this.mundaneWeaponGenerator = mundaneWeaponGenerator;
            this.magicalWeaponGenerator = magicalWeaponGenerator;
            this.generator = generator;
        }

        public Item GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var weapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.Foci.Weapons);
            var ammunition = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Ammunition);
            var nonAmmunitionWeapons = weapons.Except(ammunition);

            var weapon = GenerateFiltered(feats, characterClass, race, nonAmmunitionWeapons);
            return weapon;
        }

        private IEnumerable<string> GetAmmunitionBaseTypes(IEnumerable<string> allowedWeapons)
        {
            var baseTypesWithAmmunition = new List<string>();

            foreach (var allowedWeapon in allowedWeapons)
            {
                var baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, allowedWeapon);
                baseTypesWithAmmunition.AddRange(baseWeaponTypes);
            }

            return baseTypesWithAmmunition;
        }

        private IEnumerable<Feat> GetNonProficiencyFeatsWithWeaponFoci(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            var allWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.Foci.Weapons);

            var nonProficiencyFeats = feats.Except(proficiencyFeats).Where(f => f.Name != FeatConstants.WeaponFamiliarity);
            return nonProficiencyFeats.Where(f => allWeapons.Intersect(f.Foci).Any());
        }

        private IEnumerable<Feat> GetProficiencyFeatWithSpecificWeaponFocus(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            var allWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.Foci.Weapons);

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

        public Item GenerateAmmunition(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, string ammunitionType)
        {
            var ammunitions = new[] { ammunitionType };
            return GenerateFiltered(feats, characterClass, race, ammunitions);
        }

        public Item GenerateMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var meleeWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Melee);
            var weapon = GenerateFiltered(feats, characterClass, race, meleeWeapons);

            return weapon;
        }

        private Item GenerateFiltered(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, IEnumerable<string> filteredWeapons)
        {
            var allowedWeapons = GetAllowedWeapons(feats, filteredWeapons);

            if (allowedWeapons.Any() == false)
                return null;

            var filteredWeapon = GenerateFrom(allowedWeapons, characterClass, race);
            return filteredWeapon;
        }

        private IEnumerable<string> GetAllowedWeapons(IEnumerable<Feat> feats, IEnumerable<string> filteredWeapons)
        {
            var nonProficiencyFeatsWithWeaponFoci = GetNonProficiencyFeatsWithWeaponFoci(feats);
            var allowedWeapons = nonProficiencyFeatsWithWeaponFoci.SelectMany(f => f.Foci);
            allowedWeapons = GetAmmunitionBaseTypes(allowedWeapons);
            allowedWeapons = allowedWeapons.Intersect(filteredWeapons);

            if (allowedWeapons.Any())
                return allowedWeapons;

            var proficiencyFeatsWithSpecificWeaponFoci = GetProficiencyFeatWithSpecificWeaponFocus(feats);
            allowedWeapons = proficiencyFeatsWithSpecificWeaponFoci.SelectMany(f => f.Foci);
            allowedWeapons = GetAmmunitionBaseTypes(allowedWeapons);
            allowedWeapons = allowedWeapons.Intersect(filteredWeapons);

            if (allowedWeapons.Any())
                return allowedWeapons;

            var proficientWithAllInFeat = GetProficiencyFeatWithFocusOfAll(feats);

            foreach (var feat in proficientWithAllInFeat)
            {
                var featWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, feat.Name);
                allowedWeapons = allowedWeapons.Union(featWeapons);
            }

            allowedWeapons = GetAmmunitionBaseTypes(allowedWeapons);
            allowedWeapons = allowedWeapons.Intersect(filteredWeapons);

            return allowedWeapons;
        }

        private Item GenerateFrom(IEnumerable<string> allowedWeapons, CharacterClass characterClass, Race race)
        {
            var effectiveLevel = characterClass.Level;
            var npcs = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);

            if (npcs.Contains(characterClass.ClassName))
                effectiveLevel = Math.Max(1, characterClass.Level / 2);

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            var power = percentileSelector.SelectFrom(tableName);
            var weapon = generator.Generate(() => GenerateWeapon(power), w => WeaponIsValid(w, allowedWeapons, race));

            return weapon;
        }

        private Item GenerateWeapon(string power)
        {
            if (power == PowerConstants.Mundane)
                return mundaneWeaponGenerator.Generate();

            return magicalWeaponGenerator.GenerateAtPower(power);
        }

        private bool WeaponIsValid(Item weapon, IEnumerable<string> allowedWeapons, Race race)
        {
            if (weapon == null)
                return true;

            if (weapon.ItemType != ItemTypeConstants.Weapon)
                return false;

            if (weapon.IsMagical == false && weapon.Traits.Contains(race.Size) == false)
                return false;

            var baseWeaponType = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, weapon.Name).First();
            return allowedWeapons.Contains(baseWeaponType);
        }

        public Item GenerateOneHandedMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var meleeWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Melee);
            var twoHandedWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.TwoHanded);
            var oneHandedWeapons = meleeWeapons.Except(twoHandedWeapons);

            var weapon = GenerateFiltered(feats, characterClass, race, oneHandedWeapons);
            return weapon;
        }

        public Item GenerateRangedFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var weapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.Foci.Weapons);
            var meleeWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Melee);
            var ammunitions = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Ammunition);
            var rangedWeapons = weapons.Except(meleeWeapons).Except(ammunitions);

            var weapon = GenerateFiltered(feats, characterClass, race, rangedWeapons);
            return weapon;
        }
    }
}
