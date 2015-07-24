using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities.Feats
{
    public class RacialFeatsGenerator : IRacialFeatsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IFeatsSelector featsSelector;
        private IFeatFocusGenerator featFocusGenerator;

        public RacialFeatsGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            IFeatsSelector featsSelector, IFeatFocusGenerator featFocusGenerator)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.featsSelector = featsSelector;
            this.featFocusGenerator = featFocusGenerator;
        }

        public IEnumerable<Feat> GenerateWith(Race race, Dictionary<String, Skill> skills)
        {
            var baseRacialFeatSelections = featsSelector.SelectRacial(race.BaseRace);
            var metaracialFeatSelections = featsSelector.SelectRacial(race.Metarace);
            var metaraceSpeciesFeatSelections = featsSelector.SelectRacial(race.MetaraceSpecies);
            var allRacialFeatSelections = baseRacialFeatSelections.Union(metaracialFeatSelections).Union(metaraceSpeciesFeatSelections);

            var monsterHitDice = GetMonsterHitDice(race.BaseRace);
            var applicableRacialFeatSelections = allRacialFeatSelections.Where(s => s.MinimumHitDieRequirement <= monsterHitDice)
                                                                        .Where(s => String.IsNullOrEmpty(s.SizeRequirement) || s.SizeRequirement == race.Size);

            var feats = new HashSet<Feat>();
            foreach (var racialFeatSelection in applicableRacialFeatSelections)
            {
                var feat = new Feat();
                feat.Name = racialFeatSelection.Feat;
                feat.Focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom(racialFeatSelection.Feat, racialFeatSelection.FocusType, skills);
                feat.Frequency = racialFeatSelection.Frequency;
                feat.Strength = racialFeatSelection.Strength;

                feats.Add(feat);
            }

            return feats;
        }

        private Int32 GetMonsterHitDice(String baseRace)
        {
            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (!monsters.Contains(baseRace))
                return 1;

            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice);
            return hitDice[baseRace];
        }
    }
}