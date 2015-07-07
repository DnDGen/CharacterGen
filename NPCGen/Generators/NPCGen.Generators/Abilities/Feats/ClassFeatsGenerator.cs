using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Generators.Abilities.Feats
{
    public class ClassFeatsGenerator : IClassFeatsGenerator
    {
        private IFeatsSelector featsSelector;
        private IFeatFocusGenerator featFocusGenerator;
        private IDice dice;

        public ClassFeatsGenerator(IFeatsSelector featsSelector, IFeatFocusGenerator featFocusGenerator, IDice dice)
        {
            this.featsSelector = featsSelector;
            this.featFocusGenerator = featFocusGenerator;
            this.dice = dice;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Dictionary<String, Stat> stats, IEnumerable<Feat> racialFeats)
        {
            var characterClassFeatSelections = featsSelector.SelectClass(characterClass.ClassName).ToList();

            foreach (var specialistField in characterClass.SpecialistFields)
            {
                var specialistFeatSelections = featsSelector.SelectClass(specialistField);
                characterClassFeatSelections.AddRange(specialistFeatSelections);
            }

            var relevantClassFeatSelections = characterClassFeatSelections.Where(f => f.RequirementsMet(characterClass));
            var classFeats = GetClassFeats(relevantClassFeatSelections, racialFeats, stats, characterClass);

            if (characterClass.ClassName == CharacterClassConstants.Ranger)
                return ImproveFavoredEnemyStrength(classFeats);

            return classFeats;
        }

        private IEnumerable<Feat> GetClassFeats(IEnumerable<CharacterClassFeatSelection> classFeatSelections, IEnumerable<Feat> earnedFeat, Dictionary<String, Stat> stats, CharacterClass characterClass)
        {
            var classFeats = new List<Feat>();

            foreach (var classFeatSelection in classFeatSelections)
            {
                var classFeat = new Feat();
                classFeat.Name.Id = classFeatSelection.FeatId;
                classFeat.Focus = featFocusGenerator.GenerateFrom(classFeatSelection.FeatId, classFeatSelection.FocusType, classFeatSelection.RequiredFeats, earnedFeat, characterClass);
                classFeat.Frequency = classFeatSelection.Frequency;

                if (classFeatSelection.FrequencyQuantityStat != String.Empty)
                    classFeat.Frequency.Quantity += stats[classFeatSelection.FrequencyQuantityStat].Bonus;

                classFeat.Strength = classFeatSelection.Strength;

                classFeats.Add(classFeat);
                earnedFeat = earnedFeat.Union(classFeats);
            }

            return classFeats;
        }

        private IEnumerable<Feat> ImproveFavoredEnemyStrength(IEnumerable<Feat> classFeats)
        {
            var favoredEnemyFeats = classFeats.Where(f => f.Name.Id == FeatConstants.FavoredEnemyId);
            var favoredEnemyQuantity = favoredEnemyFeats.Count();
            var timesToImprove = favoredEnemyQuantity - 1;

            while (timesToImprove-- > 0)
            {
                var index = dice.Roll().d(favoredEnemyQuantity) - 1;
                var feat = favoredEnemyFeats.ElementAt(index);
                feat.Strength += 2;
            }

            return classFeats;
        }
    }
}