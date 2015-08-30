using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Abilities.Feats
{
    public class ClassFeatsGenerator : IClassFeatsGenerator
    {
        private IFeatsSelector featsSelector;
        private IFeatFocusGenerator featFocusGenerator;
        private ICollectionsSelector collectionsSelector;

        public ClassFeatsGenerator(IFeatsSelector featsSelector, IFeatFocusGenerator featFocusGenerator, ICollectionsSelector collectionsSelector)
        {
            this.featsSelector = featsSelector;
            this.featFocusGenerator = featFocusGenerator;
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, IEnumerable<Feat> racialFeats, Dictionary<String, Skill> skills)
        {
            var characterClassFeatSelections = featsSelector.SelectClass(characterClass.ClassName);
            var relevantClassFeatSelections = characterClassFeatSelections.Where(f => f.RequirementsMet(characterClass, race));
            var classFeats = GetClassFeats(relevantClassFeatSelections, racialFeats, stats, characterClass, skills);

            var specialistSelections = Enumerable.Empty<CharacterClassFeatSelection>();
            foreach (var specialistField in characterClass.SpecialistFields)
            {
                var specialistFeatSelections = featsSelector.SelectClass(specialistField);
                specialistSelections = specialistSelections.Union(specialistFeatSelections);
            }

            var earnedFeats = classFeats.Union(racialFeats);
            var specialistFeats = GetSpecialistFeats(specialistSelections, earnedFeats, stats, characterClass, skills);
            classFeats = classFeats.Union(specialistFeats);

            if (characterClass.ClassName == CharacterClassConstants.Ranger)
                return ImproveFavoredEnemyStrength(classFeats);

            return classFeats;
        }

        private IEnumerable<Feat> GetClassFeats(IEnumerable<CharacterClassFeatSelection> classFeatSelections, IEnumerable<Feat> earnedFeat, Dictionary<String, Stat> stats, CharacterClass characterClass, Dictionary<String, Skill> skills)
        {
            var classFeats = new List<Feat>();

            foreach (var classFeatSelection in classFeatSelections)
            {
                var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom(classFeatSelection.Feat, classFeatSelection.FocusType, skills, classFeatSelection.RequiredFeats, earnedFeat, characterClass);
                var classFeat = BuildFeatFrom(classFeatSelection, focus, earnedFeat, stats, characterClass, skills);

                classFeats.Add(classFeat);
                earnedFeat = earnedFeat.Union(classFeats);
            }

            return classFeats;
        }

        private Feat BuildFeatFrom(CharacterClassFeatSelection selection, String focus, IEnumerable<Feat> earnedFeat, Dictionary<String, Stat> stats, CharacterClass characterClass, Dictionary<String, Skill> skills)
        {
            var feat = new Feat();
            feat.Name = selection.Feat;
            feat.Focus = focus;
            feat.Frequency = selection.Frequency;

            if (selection.FrequencyQuantityStat != String.Empty)
                feat.Frequency.Quantity += stats[selection.FrequencyQuantityStat].Bonus;

            feat.Strength = selection.Strength;

            return feat;
        }

        private IEnumerable<Feat> GetSpecialistFeats(IEnumerable<CharacterClassFeatSelection> classFeatSelections, IEnumerable<Feat> earnedFeat, Dictionary<String, Stat> stats, CharacterClass characterClass, Dictionary<String, Skill> skills)
        {
            var specialistFeats = new List<Feat>();

            foreach (var classFeatSelection in classFeatSelections)
            {
                var focus = featFocusGenerator.GenerateFrom(classFeatSelection.Feat, classFeatSelection.FocusType, skills, classFeatSelection.RequiredFeats, earnedFeat, characterClass);
                var classFeat = BuildFeatFrom(classFeatSelection, focus, earnedFeat, stats, characterClass, skills);

                specialistFeats.Add(classFeat);
                earnedFeat = earnedFeat.Union(specialistFeats);
            }

            return specialistFeats;
        }

        private IEnumerable<Feat> ImproveFavoredEnemyStrength(IEnumerable<Feat> classFeats)
        {
            var favoredEnemyFeats = classFeats.Where(f => f.Name == FeatConstants.FavoredEnemy);
            var favoredEnemyQuantity = favoredEnemyFeats.Count();
            var timesToImprove = favoredEnemyQuantity - 1;

            while (timesToImprove-- > 0)
            {
                var feat = collectionsSelector.SelectRandomFrom<Feat>(favoredEnemyFeats);
                feat.Strength += 2;
            }

            return classFeats;
        }
    }
}