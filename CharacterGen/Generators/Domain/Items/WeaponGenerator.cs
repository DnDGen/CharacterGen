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
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            var power = percentileSelector.SelectFrom(tableName);
            var allowedWeapons = GetAllowedWeapons(feats);

            if (allowedWeapons.Any() == false)
                throw new NoWeaponProficienciesException();

            var additionalWeapons = new List<String>();

            foreach (var allowedWeapon in allowedWeapons)
            {
                var baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, allowedWeapon);
                additionalWeapons.AddRange(baseWeaponTypes);
            }

            allowedWeapons = allowedWeapons.Union(additionalWeapons);

            var weapon = generator.Generate(() => GenerateWeapon(power), w => WeaponIsValid(w, allowedWeapons, race));
            return weapon;
        }

        private Boolean WeaponIsValid(Item weapon, IEnumerable<String> allowedWeapons, Race race)
        {
            if (weapon.ItemType != ItemTypeConstants.Weapon || weapon.Attributes.Contains(AttributeConstants.Ammunition))
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
            var nonProficiencyFeatsWithWeaponFoci = GetNonProficiencyFeatsWithWeaponFoci(feats);
            if (nonProficiencyFeatsWithWeaponFoci.Any())
                return nonProficiencyFeatsWithWeaponFoci.Select(f => f.Focus);

            var proficiencyFeatsWithSpecificWeaponFoci = GetProficiencyFeatWithSpecificWeaponFocus(feats);
            if (proficiencyFeatsWithSpecificWeaponFoci.Any())
                return proficiencyFeatsWithSpecificWeaponFoci.Select(f => f.Focus);

            var proficientWithAllInFeat = GetProficiencyFeatWithFocusOfAll(feats);
            var proficientWeapons = new List<String>();

            foreach (var feat in proficientWithAllInFeat)
            {
                var featWeapons = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, feat.Name);
                proficientWeapons.AddRange(featWeapons);
            }

            return proficientWeapons;
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
            var tableName = String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            var power = percentileSelector.SelectFrom(tableName);
            var allowedWeapons = GetAllowedWeapons(feats);

            var additionalWeapons = new List<String>();

            foreach (var allowedWeapon in allowedWeapons)
            {
                var baseWeaponTypes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, allowedWeapon);
                additionalWeapons.AddRange(baseWeaponTypes);
            }

            allowedWeapons = allowedWeapons.Union(additionalWeapons);

            if (allowedWeapons.Contains(ammunitionType) == false)
                return null;

            var ammunition = generator.Generate(() => GenerateWeapon(power), a => AmmunitionIsValid(a, ammunitionType, race));
            return ammunition;
        }

        private Boolean AmmunitionIsValid(Item ammunition, String ammunitionType, Race race)
        {
            if (ammunition.ItemType != ItemTypeConstants.Weapon || ammunition.Attributes.Contains(AttributeConstants.Ammunition) == false)
                return false;

            if (ammunition.IsMagical == false && ammunition.Traits.Contains(race.Size) == false)
                return false;

            var baseAmmunitionType = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, ammunition.Name).First();
            return ammunitionType == baseAmmunitionType;
        }

        public Item GenerateMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            return GenerateFiltered(feats, characterClass, race, IsMelee);
        }

        private Boolean IsMelee(Item weapon)
        {
            return weapon.Attributes.Contains(AttributeConstants.Melee);
        }

        private Item GenerateFiltered(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, Func<Item, Boolean> filter)
        {
            var weapon = generator.Generate(() => GenerateFrom(feats, characterClass, race), w => w != null && filter(w));

            if (weapon != null)
                return weapon;

            var filteredFeats = GetProficiencyFeatWithSpecificWeaponFocus(feats);
            if (filteredFeats.Any())
                weapon = generator.Generate(() => GenerateFrom(filteredFeats, characterClass, race), w => w != null && filter(w));

            if (weapon != null)
                return weapon;

            filteredFeats = GetProficiencyFeatWithFocusOfAll(feats);
            weapon = generator.Generate(() => GenerateFrom(filteredFeats, characterClass, race), filter);

            return weapon;
        }

        public Item GenerateOneHandedMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            return GenerateFiltered(feats, characterClass, race, IsOneHandedMelee);
        }

        private Boolean IsOneHandedMelee(Item weapon)
        {
            return IsMelee(weapon) && weapon.Attributes.Contains(AttributeConstants.TwoHanded) == false;
        }

        public Item GenerateRangedFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            return GenerateFiltered(feats, characterClass, race, IsRanged);
        }

        private Boolean IsRanged(Item weapon)
        {
            return IsMelee(weapon) == false;
        }
    }
}
