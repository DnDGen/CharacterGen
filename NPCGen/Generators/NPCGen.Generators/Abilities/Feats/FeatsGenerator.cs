using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities.Feats;

namespace NPCGen.Generators.Abilities.Feats
{
    public class FeatsGenerator : IFeatsGenerator
    {
        private IRacialFeatsGenerator racialFeatsGenerator;
        private IClassFeatsGenerator classFeatsGenerator;
        private IAdditionalFeatsGenerator additionalFeatsGenerator;

        public FeatsGenerator(IRacialFeatsGenerator racialFeatsGenerator, IClassFeatsGenerator classFeatsGenerator,
            IAdditionalFeatsGenerator additionalFeatsGenerator)
        {
            this.racialFeatsGenerator = racialFeatsGenerator;
            this.classFeatsGenerator = classFeatsGenerator;
            this.additionalFeatsGenerator = additionalFeatsGenerator;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, BaseAttack baseAttack)
        {
            var racialFeats = racialFeatsGenerator.GenerateWith(race);
            var classFeats = classFeatsGenerator.GenerateWith(characterClass);
            var additionalFeats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);

            var allFeats = racialFeats.Union(classFeats).Union(additionalFeats);

            var featsToCombine = allFeats.Where(f => CanCombine(f, allFeats));

            foreach (var featToCombine in featsToCombine)
            {
                var otherFeats = featsToCombine.Where(f => CanCombine(f, featsToCombine) && f != featsToCombine);

                foreach (var otherFeat in otherFeats)
                    featToCombine.Frequency.Quantity += otherFeat.Frequency.Quantity;

                allFeats = allFeats.Except(otherFeats);
            }

            return allFeats;
        }

        private Boolean CanCombine(Feat feat, IEnumerable<Feat> allFeats)
        {
            var count = allFeats.Count(f => f.Name.Id == feat.Name.Id
                                        && f.Focus == feat.Focus
                                        && f.Strength == feat.Strength
                                        && f.Frequency.TimePeriod == feat.Frequency.TimePeriod);

            return count > 1;
        }
    }
}