using CharacterGen.Tables;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    public abstract class RacialFeatDataTests : DataTests
    {
        protected override void PopulateIndices(IEnumerable<String> collection)
        {
            indices[DataIndexConstants.RacialFeatData.FeatNameIndex] = "FeatId";
            indices[DataIndexConstants.RacialFeatData.FocusIndex] = "FocusType";
            indices[DataIndexConstants.RacialFeatData.FrequencyQuantityIndex] = "FrequencyQuantity";
            indices[DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex] = "FrequencyTimePeriod";
            indices[DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex] = "MinHitDiceRequirement";
            indices[DataIndexConstants.RacialFeatData.SizeRequirementIndex] = "SizeRequirement";
            indices[DataIndexConstants.RacialFeatData.StrengthIndex] = "Strength";
            indices[DataIndexConstants.RacialFeatData.MaximumHitDiceRequirementIndex] = "MaxHitDiceRequirement";
            indices[DataIndexConstants.RacialFeatData.RequiredStatIndex] = "RequiredStat";
            indices[DataIndexConstants.RacialFeatData.RequiredStatMinimumValueIndex] = "RequiredStatMinimumValue";
        }

        public virtual void Data(String name, String feat, String focus, Int32 frequencyQuantity,
            String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement,
            Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumStats)
        {
            var data = new List<String>();
            for (var i = 0; i < 10; i++)
                data.Add(String.Empty);

            var requiredStat = String.Join(",", minimumStats);

            data[DataIndexConstants.RacialFeatData.FeatNameIndex] = feat;
            data[DataIndexConstants.RacialFeatData.FocusIndex] = focus;
            data[DataIndexConstants.RacialFeatData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex] = Convert.ToString(minimumHitDiceRequirement);
            data[DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.RacialFeatData.SizeRequirementIndex] = sizeRequirement;
            data[DataIndexConstants.RacialFeatData.StrengthIndex] = Convert.ToString(strength);
            data[DataIndexConstants.RacialFeatData.MaximumHitDiceRequirementIndex] = Convert.ToString(maximumHitDiceRequirement);
            data[DataIndexConstants.RacialFeatData.RequiredStatIndex] = requiredStat;
            data[DataIndexConstants.RacialFeatData.RequiredStatMinimumValueIndex] = Convert.ToString(requiredStatMinimumValue);

            Data(name, data);
        }
    }
}
