using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Selectors
{
    public class FeatsSelector : IFeatsSelector
    {
        private ICollectionsSelector collectionsSelector;

        public FeatsSelector(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<RacialFeatSelection> SelectRacial()
        {
            var racialFeatNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.Racial);
            var racialFeatSelections = new List<RacialFeatSelection>();

            foreach (var racialFeatName in racialFeatNames)
            {
                var racialFeatSelection = new RacialFeatSelection();
                racialFeatSelection.FeatName = racialFeatName;

                var featData = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatData, racialFeatName);
                racialFeatSelection.SizeRequirement = featData.First();
                racialFeatSelection.FeatStrength = Convert.ToInt32(featData.Last());

                racialFeatSelection.BaseRaceRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RacialFeatBaseRaceRequirements, racialFeatName);
                racialFeatSelection.HitDieRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RacialFeatHitDieRequirements, racialFeatName)
                    .Select(r => Convert.ToInt32(r));
                racialFeatSelection.MetaraceRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RacialFeatMetaraceRequirements, racialFeatName);
                racialFeatSelection.MetaraceSpeciesRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RacialFeatMetaraceSpeciesRequirements, racialFeatName);

                racialFeatSelections.Add(racialFeatSelection);
            }

            return racialFeatSelections;
        }

        public IEnumerable<AdditionalFeatSelection> SelectAdditional()
        {
            throw new NotImplementedException();
        }

        public AdditionalFeatSelection SelectAdditional(String featName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CharacterClassFeatSelection> SelectClassFeats()
        {
            throw new NotImplementedException();
        }
    }
}