using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
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

        public RacialFeatsGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            IFeatsSelector featsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.featsSelector = featsSelector;
        }

        public IEnumerable<Feat> GenerateWith(Race race)
        {
            var baseRacialFeatSelections = featsSelector.SelectRacial(race.BaseRace.Id);
            var metaracialFeatSelections = featsSelector.SelectRacial(race.Metarace.Id);
            var metaraceSpeciesFeatSelections = featsSelector.SelectRacial(race.MetaraceSpecies);
            var allRacialFeatSelections = baseRacialFeatSelections.Union(metaracialFeatSelections).Union(metaraceSpeciesFeatSelections);

            var monsterHitDice = GetMonsterHitDice(race.BaseRace.Id);
            var applicableRacialFeatSelections = allRacialFeatSelections.Where(s => s.MinimumHitDieRequirement <= monsterHitDice)
                                                                        .Where(s => String.IsNullOrEmpty(s.SizeRequirement) || s.SizeRequirement == race.Size);

            var feats = new HashSet<Feat>();
            foreach (var racialFeatSelection in applicableRacialFeatSelections)
            {
                var feat = new Feat();
                feat.Name.Id = racialFeatSelection.FeatId;
                feat.Focus = racialFeatSelection.Focus;
                feat.Frequency = racialFeatSelection.Frequency;
                feat.Strength = racialFeatSelection.Strength;

                feats.Add(feat);
            }

            return feats;
        }

        private Int32 GetMonsterHitDice(String baseRaceId)
        {
            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (!monsters.Contains(baseRaceId))
                return 1;

            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice);
            return hitDice[baseRaceId];
        }
    }
}