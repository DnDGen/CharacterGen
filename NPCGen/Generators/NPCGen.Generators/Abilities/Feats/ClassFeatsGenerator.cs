using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Abilities.Feats
{
    public class ClassFeatsGenerator : IClassFeatsGenerator
    {
        private IFeatsSelector featsSelector;
        private IFeatFocusGenerator featFocusGenerator;

        public ClassFeatsGenerator(IFeatsSelector featsSelector, IFeatFocusGenerator featFocusGenerator)
        {
            this.featsSelector = featsSelector;
            this.featFocusGenerator = featFocusGenerator;
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
            var classFeats = new List<Feat>();
            var earnedFeat = racialFeats;

            foreach (var classFeatSelection in relevantClassFeatSelections)
            {
                var classFeat = new Feat();
                classFeat.Name.Id = classFeatSelection.FeatId;
                classFeat.Focus = featFocusGenerator.GenerateFrom(classFeatSelection.FeatId, classFeatSelection.FocusType, classFeatSelection.RequiredFeatIds, earnedFeat, characterClass);
                classFeat.Frequency = classFeatSelection.Frequency;

                if (classFeatSelection.FrequencyQuantityStat != String.Empty)
                    classFeat.Frequency.Quantity += stats[classFeatSelection.FrequencyQuantityStat].Bonus;

                classFeat.Strength = classFeatSelection.Strength;

                classFeats.Add(classFeat);
                earnedFeat = earnedFeat.Union(classFeats);
            }

            return classFeats;
        }
    }
}