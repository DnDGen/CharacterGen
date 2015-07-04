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

        public String GenerateFrom(String featId, String focusType, IEnumerable<String> requiredFeatIds, IEnumerable<Feat> otherFeats, CharacterClass characterClass)
        {
            if (String.IsNullOrEmpty(focusType))
                return String.Empty;

            var foci = GetFoci(focusType, otherFeats, requiredFeatIds);
            var usedFoci = otherFeats.Where(f => f.Name.Id == featId).Select(f => f.Focus);
            foci = foci.Except(usedFoci);

            if (focusType == GroupConstants.SchoolsOfMagic)
                foci = foci.Except(characterClass.ProhibitedFields);

            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters);
            if (spellcasters.Contains(characterClass.ClassName) == false)
                foci = foci.Except(new[] { WeaponProficiencyConstants.Ray });

            var index = GetRandomIndexOf(foci);
            var focus = foci.ElementAt(index);

            return focus;
        }

        private IEnumerable<String> GetFoci(String focusType, IEnumerable<Feat> otherFeats, IEnumerable<String> requiredFeatIds)
        {
            var sourceFeatFoci = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, focusType);
            var proficiencyRequired = requiredFeatIds.Contains(GroupConstants.Proficiency);

            if (focusType == FeatConstants.MartialWeaponProficiencyId)
            {
                var weaponFamiliartyFeats = otherFeats.Where(f => f.Name.Id == FeatConstants.WeaponFamiliarityId);
                var familiarityFoci = weaponFamiliartyFeats.Select(f => f.Focus);
                sourceFeatFoci = sourceFeatFoci.Union(familiarityFoci);
            }

            if (proficiencyRequired == false && otherFeats.Any(f => RequirementHasFocus(requiredFeatIds, f)) == false)
                return sourceFeatFoci;

            var proficiencyFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency);
            var proficiencyFeats = otherFeats.Where(f => proficiencyFeatIds.Contains(f.Name.Id));

            var requiredFeats = otherFeats.Where(f => RequirementHasFocus(requiredFeatIds, f));
            requiredFeats = requiredFeats.Union(proficiencyFeats);

            var requirementFoci = requiredFeats.Where(f => f.Focus != f.Name.Id).Select(f => f.Focus);
            var featsWithAllFocus = requiredFeats.Where(f => f.Focus == f.Name.Id);

            foreach (var featWithAllFocus in featsWithAllFocus)
            {
                var explodedFoci = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, featWithAllFocus.Name.Id);

                if (featWithAllFocus.Name.Id == FeatConstants.MartialWeaponProficiencyId)
                {
                    var weaponFamiliartyFeats = otherFeats.Where(f => f.Name.Id == FeatConstants.WeaponFamiliarityId);
                    var familiarityFoci = weaponFamiliartyFeats.Select(f => f.Focus);
                    explodedFoci = explodedFoci.Union(familiarityFoci);
                }

                requirementFoci = requirementFoci.Union(explodedFoci);
            }

            var applicableFoci = requirementFoci.Intersect(sourceFeatFoci);

            return applicableFoci;
        }

        private Boolean RequirementHasFocus(IEnumerable<String> requiredFeatIds, Feat feat)
        {
            return requiredFeatIds.Contains(feat.Name.Id) && String.IsNullOrEmpty(feat.Focus) == false;
        }

        private Int32 GetRandomIndexOf(IEnumerable<Object> collection)
        {
            var die = collection.Count();
            return dice.Roll().d(die) - 1;
        }

        public String GenerateFrom(String featId, String focusType, Dictionary<String, Skill> skills)
        {
            if (String.IsNullOrEmpty(focusType))
                return String.Empty;

            var foci = GetFoci(focusType, Enumerable.Empty<Feat>(), Enumerable.Empty<String>());
            var allSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills);
            var skillFoci = allSkills.Intersect(foci);

            if (skillFoci.Any())
            {
                var missingSkills = allSkills.Except(skills.Keys);
                foci = foci.Except(missingSkills);
            }

            if (foci.Any() == false)
                return String.Empty;

            var index = GetRandomIndexOf(foci);
            var focus = foci.ElementAt(index);

            return focus;
        }
    }
}