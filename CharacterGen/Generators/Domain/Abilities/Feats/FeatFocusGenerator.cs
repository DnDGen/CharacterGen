using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Generators.Domain.Abilities.Feats
{
    public class FeatFocusGenerator : IFeatFocusGenerator
    {
        private ICollectionsSelector collectionsSelector;

        public FeatFocusGenerator(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public String GenerateFrom(String feat, String focusType, Dictionary<String, Skill> skills, IEnumerable<RequiredFeat> requiredFeats, IEnumerable<Feat> otherFeats, CharacterClass characterClass)
        {
            if (String.IsNullOrEmpty(focusType))
                return String.Empty;

            var allSourceFeatFoci = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci);
            if (focusType != FeatConstants.Foci.All && allSourceFeatFoci.Keys.Contains(focusType) == false)
                return focusType;

            var requiredFeatIds = requiredFeats.Select(f => f.Feat);
            var foci = GetFoci(feat, focusType, allSourceFeatFoci, otherFeats, requiredFeatIds);
            var usedFeats = otherFeats.Where(f => f.Name == feat);
            var usedFoci = Enumerable.Empty<String>();

            foreach (var usedFeat in usedFeats)
                usedFoci = usedFoci.Union(usedFeat.Foci);

            if (usedFoci.Contains(FeatConstants.Foci.All))
                return FeatConstants.Foci.All;

            foci = foci.Except(usedFoci);

            var allSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills);
            var skillFoci = allSkills.Intersect(foci);

            if (skillFoci.Any())
            {
                var missingSkills = allSkills.Except(skills.Keys);
                foci = foci.Except(missingSkills);
            }

            if (focusType == GroupConstants.SchoolsOfMagic)
                foci = foci.Except(characterClass.ProhibitedFields);

            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters);
            if (spellcasters.Contains(characterClass.ClassName) == false)
                foci = foci.Except(new[] { FeatConstants.Foci.Ray });

            if (foci.Any() == false)
                return FeatConstants.Foci.All;

            return collectionsSelector.SelectRandomFrom(foci);
        }

        private IEnumerable<String> GetFoci(String feat, String focusType, Dictionary<String, IEnumerable<String>> allSourceFeatFoci, IEnumerable<Feat> otherFeats, IEnumerable<String> requiredFeatNames)
        {
            var proficiencyRequired = requiredFeatNames.Contains(ItemTypeConstants.Weapon + GroupConstants.Proficiency);
            var sourceFeatFoci = GetExplodedFoci(allSourceFeatFoci, feat, focusType, otherFeats);

            if (proficiencyRequired == false && otherFeats.Any(f => RequirementHasFocus(requiredFeatNames, f)) == false)
                return sourceFeatFoci;

            var requiredFeatWithFoci = otherFeats.Where(f => RequirementHasFocus(requiredFeatNames, f));

            if (proficiencyRequired)
            {
                var proficiencyFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency);
                var proficiencyFeats = otherFeats.Where(f => proficiencyFeatNames.Contains(f.Name));

                requiredFeatWithFoci = requiredFeatWithFoci.Union(proficiencyFeats);
            }

            var requiredFeats = requiredFeatWithFoci.Where(f => f.Foci.Contains(FeatConstants.Foci.All) == false);
            var requiredFoci = Enumerable.Empty<String>();

            foreach (var requiredFeat in requiredFeats)
                requiredFoci = requiredFoci.Union(requiredFeat.Foci);

            var featsWithAllFocus = requiredFeatWithFoci.Where(f => f.Foci.Contains(FeatConstants.Foci.All));

            foreach (var featWithAllFocus in featsWithAllFocus)
            {
                var explodedFoci = GetExplodedFoci(allSourceFeatFoci, featWithAllFocus.Name, FeatConstants.Foci.All, otherFeats);
                requiredFoci = requiredFoci.Union(explodedFoci);
            }

            var applicableFoci = requiredFoci.Intersect(sourceFeatFoci);

            if (focusType.StartsWith(FeatConstants.Foci.Weapons) && requiredFeatWithFoci.Any() == false)
            {
                var automaticFoci = allSourceFeatFoci[focusType].Except(allSourceFeatFoci[FeatConstants.Foci.Weapons]);
                applicableFoci = applicableFoci.Union(automaticFoci);
            }

            return applicableFoci;
        }

        private IEnumerable<String> GetExplodedFoci(Dictionary<String, IEnumerable<String>> allSourceFeatFoci, String feat, String focusType, IEnumerable<Feat> otherFeats)
        {
            if (focusType != FeatConstants.Foci.All)
                return allSourceFeatFoci[focusType];

            if (feat != FeatConstants.MartialWeaponProficiency && feat != FeatConstants.ExoticWeaponProficiency)
                return allSourceFeatFoci[feat];

            var weaponFamiliarityFeats = otherFeats.Where(f => f.Name == FeatConstants.WeaponFamiliarity);
            var familiarityFoci = Enumerable.Empty<String>();

            foreach (var weaponFamiliarityFeat in weaponFamiliarityFeats)
                familiarityFoci = familiarityFoci.Union(weaponFamiliarityFeat.Foci);

            if (feat == FeatConstants.MartialWeaponProficiency)
                return allSourceFeatFoci[feat].Union(familiarityFoci);

            return allSourceFeatFoci[feat].Except(familiarityFoci);
        }

        private Boolean RequirementHasFocus(IEnumerable<String> requiredFeatNames, Feat feat)
        {
            return requiredFeatNames.Contains(feat.Name) && feat.Foci.Any();
        }

        public String GenerateFrom(String feat, String focusType, Dictionary<String, Skill> skills)
        {
            if (String.IsNullOrEmpty(focusType))
                return String.Empty;

            var allSourceFeatFoci = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci);
            if (focusType != FeatConstants.Foci.All && allSourceFeatFoci.Keys.Contains(focusType) == false)
                return focusType;

            var foci = GetFoci(feat, focusType, allSourceFeatFoci, Enumerable.Empty<Feat>(), Enumerable.Empty<String>());
            var allSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills);
            var skillFoci = allSkills.Intersect(foci);

            if (skillFoci.Any())
            {
                var missingSkills = allSkills.Except(skills.Keys);
                foci = foci.Except(missingSkills);
            }

            if (foci.Any() == false)
                return FeatConstants.Foci.All;

            return collectionsSelector.SelectRandomFrom(foci);
        }


        public String GenerateAllowingFocusOfAllFrom(String feat, String focusType, Dictionary<String, Skill> skills, IEnumerable<RequiredFeat> requiredFeats, IEnumerable<Feat> otherFeats, CharacterClass characterClass)
        {
            if (focusType == FeatConstants.Foci.All)
                return focusType;

            return GenerateFrom(feat, focusType, skills, requiredFeats, otherFeats, characterClass);
        }

        public String GenerateAllowingFocusOfAllFrom(String feat, String focusType, Dictionary<String, Skill> skills)
        {
            if (focusType == FeatConstants.Foci.All)
                return focusType;

            return GenerateFrom(feat, focusType, skills);
        }
    }
}