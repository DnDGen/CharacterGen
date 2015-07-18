using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities.Feats
{
    public class FeatFocusGenerator : IFeatFocusGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IDice dice;

        public FeatFocusGenerator(ICollectionsSelector collectionsSelector, IDice dice)
        {
            this.collectionsSelector = collectionsSelector;
            this.dice = dice;
        }

        public String GenerateFrom(String feat, String focusType, Dictionary<String, Skill> skills, IEnumerable<RequiredFeat> requiredFeats, IEnumerable<Feat> otherFeats, CharacterClass characterClass)
        {
            if (String.IsNullOrEmpty(focusType))
                return String.Empty;

            var allSourceFeatFoci = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci);
            if (focusType != ProficiencyConstants.All && allSourceFeatFoci.Keys.Contains(focusType) == false)
                return focusType;

            var requiredFeatIds = requiredFeats.Select(f => f.Feat);
            var foci = GetFoci(feat, focusType, allSourceFeatFoci, otherFeats, requiredFeatIds);
            var usedFoci = otherFeats.Where(f => f.Name == feat).Select(f => f.Focus);

            if (usedFoci.Contains(ProficiencyConstants.All))
                return ProficiencyConstants.All;

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
                foci = foci.Except(new[] { ProficiencyConstants.Ray });

            if (foci.Any() == false)
                return ProficiencyConstants.All;

            var index = GetRandomIndexOf(foci);
            var focus = foci.ElementAt(index);

            return focus;
        }

        private IEnumerable<String> GetFoci(String feat, String focusType, Dictionary<String, IEnumerable<String>> allSourceFeatFoci, IEnumerable<Feat> otherFeats, IEnumerable<String> requiredFeatIds)
        {
            var proficiencyRequired = requiredFeatIds.Contains(GroupConstants.Proficiency);
            var sourceFeatFoci = GetExplodedFoci(allSourceFeatFoci, feat, focusType, otherFeats);

            if (proficiencyRequired == false && otherFeats.Any(f => RequirementHasFocus(requiredFeatIds, f)) == false)
                return sourceFeatFoci;

            var proficiencyFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency);
            var proficiencyFeats = otherFeats.Where(f => proficiencyFeatIds.Contains(f.Name));

            var requiredFeats = otherFeats.Where(f => RequirementHasFocus(requiredFeatIds, f));
            requiredFeats = requiredFeats.Union(proficiencyFeats);

            var requirementFoci = requiredFeats.Where(f => f.Focus != ProficiencyConstants.All).Select(f => f.Focus);
            var featsWithAllFocus = requiredFeats.Where(f => f.Focus == ProficiencyConstants.All);

            foreach (var featWithAllFocus in featsWithAllFocus)
            {
                var explodedFoci = GetExplodedFoci(allSourceFeatFoci, featWithAllFocus.Name, featWithAllFocus.Focus, otherFeats);
                requirementFoci = requirementFoci.Union(explodedFoci);
            }

            var applicableFoci = requirementFoci.Intersect(sourceFeatFoci);

            if (focusType.StartsWith(GroupConstants.Weapons))
            {
                var automaticFoci = allSourceFeatFoci[focusType].Except(allSourceFeatFoci[GroupConstants.Weapons]);
                applicableFoci = applicableFoci.Union(automaticFoci);
            }

            return applicableFoci;
        }

        private IEnumerable<String> GetExplodedFoci(Dictionary<String, IEnumerable<String>> allSourceFeatFoci, String feat, String focusType, IEnumerable<Feat> otherFeats)
        {
            if (focusType != ProficiencyConstants.All)
                return allSourceFeatFoci[focusType];

            if (feat != FeatConstants.MartialWeaponProficiency && feat != FeatConstants.ExoticWeaponProficiency)
                return allSourceFeatFoci[feat];

            var weaponFamiliartyFeats = otherFeats.Where(f => f.Name == FeatConstants.WeaponFamiliarity);
            var familiarityFoci = weaponFamiliartyFeats.Select(f => f.Focus);

            if (feat == FeatConstants.MartialWeaponProficiency)
                return allSourceFeatFoci[feat].Union(familiarityFoci);

            return allSourceFeatFoci[feat].Except(familiarityFoci);
        }

        private Boolean RequirementHasFocus(IEnumerable<String> requiredFeatIds, Feat feat)
        {
            return requiredFeatIds.Contains(feat.Name) && String.IsNullOrEmpty(feat.Focus) == false;
        }

        private Int32 GetRandomIndexOf(IEnumerable<Object> collection)
        {
            var die = collection.Count();
            return dice.Roll().d(die) - 1;
        }

        public String GenerateFrom(String feat, String focusType, Dictionary<String, Skill> skills)
        {
            if (String.IsNullOrEmpty(focusType))
                return String.Empty;

            var allSourceFeatFoci = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci);
            if (focusType != ProficiencyConstants.All && allSourceFeatFoci.Keys.Contains(focusType) == false)
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
                return ProficiencyConstants.All;

            var index = GetRandomIndexOf(foci);
            var focus = foci.ElementAt(index);

            return focus;
        }


        public String GenerateAllowingFocusOfAllFrom(String feat, String focusType, Dictionary<String, Skill> skills, IEnumerable<RequiredFeat> requiredFeats, IEnumerable<Feat> otherFeats, CharacterClass characterClass)
        {
            if (focusType == ProficiencyConstants.All)
                return focusType;

            return GenerateFrom(feat, focusType, skills, requiredFeats, otherFeats, characterClass);
        }

        public String GenerateAllowingFocusOfAllFrom(String feat, String focusType, Dictionary<String, Skill> skills)
        {
            if (focusType == ProficiencyConstants.All)
                return focusType;

            return GenerateFrom(feat, focusType, skills);
        }
    }
}