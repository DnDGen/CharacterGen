using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Selections;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Abilities.Feats
{
    internal class ClassFeatsGenerator : IClassFeatsGenerator
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

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Feat> racialFeats, Dictionary<String, Skill> skills)
        {
            var characterClassFeatSelections = featsSelector.SelectClass(characterClass.Name);
            var classFeats = GetClassFeats(characterClassFeatSelections, race, racialFeats, stats, characterClass, skills);

            var specialistSelections = Enumerable.Empty<CharacterClassFeatSelection>();
            foreach (var specialistField in characterClass.SpecialistFields)
            {
                var specialistFeatSelections = featsSelector.SelectClass(specialistField);
                specialistSelections = specialistSelections.Union(specialistFeatSelections);
            }

            var earnedFeats = classFeats.Union(racialFeats);
            var specialistFeats = GetClassFeats(specialistSelections, race, earnedFeats, stats, characterClass, skills);
            classFeats = classFeats.Union(specialistFeats);

            if (characterClass.Name == CharacterClassConstants.Ranger)
                return ImproveFavoredEnemyStrength(classFeats);

            return classFeats;
        }

        private IEnumerable<Feat> GetClassFeats(IEnumerable<CharacterClassFeatSelection> classFeatSelections, Race race, IEnumerable<Feat> earnedFeat, Dictionary<string, Stat> stats, CharacterClass characterClass, Dictionary<string, Skill> skills)
        {
            var classFeats = new List<Feat>();

            foreach (var classFeatSelection in classFeatSelections)
            {
                if (classFeatSelection.RequirementsMet(characterClass, race, earnedFeat) == false)
                    continue;

                var focus = classFeatSelection.FocusType;

                if (classFeatSelection.AllowFocusOfAll)
                    focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom(classFeatSelection.Feat, classFeatSelection.FocusType, skills, classFeatSelection.RequiredFeats, earnedFeat, characterClass);
                else
                    focus = featFocusGenerator.GenerateFrom(classFeatSelection.Feat, classFeatSelection.FocusType, skills, classFeatSelection.RequiredFeats, earnedFeat, characterClass);

                var classFeat = BuildFeatFrom(classFeatSelection, focus, earnedFeat, stats, characterClass, skills);
                classFeats.Add(classFeat);

                earnedFeat = earnedFeat.Union(classFeats);
            }

            return classFeats;
        }

        private Feat BuildFeatFrom(CharacterClassFeatSelection selection, string focus, IEnumerable<Feat> earnedFeat, Dictionary<string, Stat> stats, CharacterClass characterClass, Dictionary<string, Skill> skills)
        {
            var feat = new Feat();
            feat.Name = selection.Feat;

            if (string.IsNullOrEmpty(focus) == false)
                feat.Foci = feat.Foci.Union(new[] { focus });

            feat.Frequency = selection.Frequency;
            feat.Power = selection.Power;

            if (string.IsNullOrEmpty(selection.FrequencyQuantityStat) == false)
                feat.Frequency.Quantity += stats[selection.FrequencyQuantityStat].Bonus;

            if (feat.Frequency.Quantity < 0)
                feat.Frequency.Quantity = 0;

            return feat;
        }

        private IEnumerable<Feat> ImproveFavoredEnemyStrength(IEnumerable<Feat> classFeats)
        {
            var favoredEnemyFeats = classFeats.Where(f => f.Name == FeatConstants.FavoredEnemy);
            var favoredEnemyQuantity = favoredEnemyFeats.Count();
            var timesToImprove = favoredEnemyQuantity - 1;

            while (timesToImprove-- > 0)
            {
                var feat = collectionsSelector.SelectRandomFrom(favoredEnemyFeats);
                feat.Power += 2;
            }

            return classFeats;
        }
    }
}