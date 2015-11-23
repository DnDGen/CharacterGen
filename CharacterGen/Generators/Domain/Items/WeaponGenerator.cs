using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Verifiers.Exceptions;
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
    public class WeaponGenerator : IWeaponGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IPercentileSelector percentileSelector;
        private IMundaneItemGenerator mundaneWeaponGenerator;
        private IMagicalItemGenerator magicalWeaponGenerator;
        private Generator generator;

        public WeaponGenerator(ICollectionsSelector collectionsSelector, IPercentileSelector percentileSelector,
            IMundaneItemGenerator mundaneWeaponGenerator, IMagicalItemGenerator magicalWeaponGenerator, Generator generator)
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

            if (weapon == null)
                throw new NoWeaponProficienciesException();

            return weapon;
        }

        private Item GenerateWeapon(String power)
        {
            if (power == PowerConstants.Mundane)
                return mundaneWeaponGenerator.Generate();

            return magicalWeaponGenerator.GenerateAtPower(power);
        }

        private IEnumerable<String> GetAmmunitionBaseTypes(IEnumerable<String> allowedWeapons)
        {
            var baseTypesWithAmmunition = new List<String>();

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
            return nonProficiencyFeats.Where(f => allWeapons.Contains(f.Focus));
        }

        private IEnumerable<Feat> GetProficiencyFeatWithSpecificWeaponFocus(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            var allWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.Foci.Weapons);

            return proficiencyFeats.Where(f => allWeapons.Contains(f.Focus));
        }

        private IEnumerable<Feat> GetProficiencyFeatWithFocusOfAll(IEnumerable<Feat> feats)
        {
            var proficiencyFeats = GetProficiencyFeats(feats);
            return proficiencyFeats.Where(f => f.Focus == FeatConstants.Foci.All);
        }

        private IEnumerable<Feat> GetProficiencyFeats(IEnumerable<Feat> feats)
        {
            var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency);
            return feats.Where(f => proficiencyFeatNames.Contains(f.Name));
        }

        public Item GenerateAmmunition(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, String ammunitionType)
        {
            var ammunitions = new[] { ammunitionType };
            return GenerateFiltered(feats, characterClass, race, ammunitions);
        }

        public Item GenerateMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var meleeWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Melee);
            var weapon = GenerateFiltered(feats, characterClass, race, meleeWeapons);

            if (weapon == null)
                throw new NoWeaponProficienciesException();

            return weapon;
        }

        private Item GenerateFiltered(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, IEnumerable<String> filteredWeapons)
        {
            var allowedWeapons = GetAllowedWeapons(feats, filteredWeapons);

            if (allowedWeapons.Any() == false)
                return null;

            return GenerateFrom(allowedWeapons, characterClass, race);
        }

        private IEnumerable<String> GetAllowedWeapons(IEnumerable<Feat> feats, IEnumerable<String> filteredWeapons)
        {
            var nonProficiencyFeatsWithWeaponFoci = GetNonProficiencyFeatsWithWeaponFoci(feats);
            var allowedWeapons = nonProficiencyFeatsWithWeaponFoci.Select(f => f.Focus);
            allowedWeapons = GetAmmunitionBaseTypes(allowedWeapons);
            allowedWeapons = allowedWeapons.Intersect(filteredWeapons);

            if (allowedWeapons.Any())
                return allowedWeapons;

            var proficiencyFeatsWithSpecificWeaponFoci = GetProficiencyFeatWithSpecificWeaponFocus(feats);
            allowedWeapons = proficiencyFeatsWithSpecificWeaponFoci.Select(f => f.Focus);
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

        private Item GenerateFrom(IEnumerable<String> allowedWeapons, CharacterClass characterClass, Race race)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            var power = percentileSelector.SelectFrom(tableName);
            var weapon = generator.Generate(() => GenerateWeapon(power), w => WeaponIsValid(w, allowedWeapons, race));

            return weapon;
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

        public Item GenerateOneHandedMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var meleeWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Melee);
            var twoHandedWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.TwoHanded);
            var oneHandedWeapons = meleeWeapons.Except(twoHandedWeapons);

            var weapon = GenerateFiltered(feats, characterClass, race, oneHandedWeapons);

            if (weapon == null)
                throw new NoWeaponProficienciesException();

            return weapon;
        }

        public Item GenerateRangedFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            var weapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.Foci.Weapons);
            var meleeWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Melee);
            var ammunitions = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Ammunition);
            var rangedWeapons = weapons.Except(meleeWeapons).Except(ammunitions);

            var weapon = GenerateFiltered(feats, characterClass, race, rangedWeapons);

            if (weapon == null)
                throw new NoWeaponProficienciesException();

            return weapon;
        }
    }
}
