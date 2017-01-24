using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Abilities.Feats
{
    internal class RacialFeatsGenerator : IRacialFeatsGenerator
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

        public IEnumerable<Feat> GenerateWith(Race race, Dictionary<string, Skill> skills, Dictionary<string, Stat> stats)
        {
            var baseRacialFeatSelections = featsSelector.SelectRacial(race.BaseRace);
            var metaracialFeatSelections = featsSelector.SelectRacial(race.Metarace);
            var featToIncreasePower = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.AddMonsterHitDiceToPower);

            foreach (var selection in metaracialFeatSelections)
                if (featToIncreasePower.Contains(selection.Feat))
                    selection.Power += GetMonsterHitDice(race.BaseRace);

            var metaraceSpeciesFeatSelections = featsSelector.SelectRacial(race.MetaraceSpecies);
            var allRacialFeatSelections = baseRacialFeatSelections.Union(metaracialFeatSelections).Union(metaraceSpeciesFeatSelections);

            var monsterHitDice = GetMonsterHitDice(race.BaseRace);
            var applicableRacialFeatSelections = allRacialFeatSelections.Where(s => s.RequirementsMet(race, monsterHitDice, stats));
            var feats = new List<Feat>();

            foreach (var racialFeatSelection in applicableRacialFeatSelections)
            {
                var feat = new Feat();
                feat.Name = racialFeatSelection.Feat;

                var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom(racialFeatSelection.Feat, racialFeatSelection.FocusType, skills);
                if (string.IsNullOrEmpty(focus) == false)
                    feat.Foci = feat.Foci.Union(new[] { focus });

                feat.Frequency = racialFeatSelection.Frequency;
                feat.Power = racialFeatSelection.Power;

                feats.Add(feat);
            }

            return feats;
        }

        private int GetMonsterHitDice(string baseRace)
        {
            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (monsters.Contains(baseRace) == false)
                return 1;

            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice, baseRace);
            return hitDice;
        }
    }
}