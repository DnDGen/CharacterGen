using System;
using System.Collections.Generic;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    public abstract class RacialFeatDataTests : DataTests
    {
        protected override void PopulateIndices(IEnumerable<String> collection)
        {
            indices[DataIndexConstants.RacialFeatData.FeatIdIndex] = "FeatId";
            indices[DataIndexConstants.RacialFeatData.FocusIndex] = "FocusType";
            indices[DataIndexConstants.RacialFeatData.FrequencyQuantityIndex] = "FrequencyQuantity";
            indices[DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex] = "FrequencyTimePeriod";
            indices[DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex] = "MinHitDiceRequirement";
            indices[DataIndexConstants.RacialFeatData.SizeRequirementIndex] = "SizeRequirement";
            indices[DataIndexConstants.RacialFeatData.StrengthIndex] = "Strength";
        }

        public virtual void Data(String name, String featId, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength)
        {
            Data(name, featId, focus, frequencyQuantity.ToString(), frequencyTimePeriod, minimumHitDiceRequirement.ToString(), sizeRequirement, strength.ToString());
        }
    }
}
