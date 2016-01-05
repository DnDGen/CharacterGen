using CharacterGen.Tables;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    public abstract class RacialFeatDataTests : DataTests
    {
        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.RacialFeatData.FeatNameIndex] = "FeatId";
            indices[DataIndexConstants.RacialFeatData.FocusIndex] = "FocusType";
            indices[DataIndexConstants.RacialFeatData.FrequencyQuantityIndex] = "FrequencyQuantity";
            indices[DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex] = "FrequencyTimePeriod";
            indices[DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex] = "MinHitDiceRequirement";
            indices[DataIndexConstants.RacialFeatData.SizeRequirementIndex] = "SizeRequirement";
            indices[DataIndexConstants.RacialFeatData.PowerIndex] = "Power";
            indices[DataIndexConstants.RacialFeatData.MaximumHitDiceRequirementIndex] = "MaxHitDiceRequirement";
            indices[DataIndexConstants.RacialFeatData.RequiredStatIndex] = "RequiredStat";
            indices[DataIndexConstants.RacialFeatData.RequiredStatMinimumValueIndex] = "RequiredStatMinimumValue";
        }

        public virtual void Data(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumStats)
        {
            var data = new List<string>();
            for (var i = 0; i < 10; i++)
                data.Add(string.Empty);

            var requiredStat = string.Join(",", minimumStats);

            data[DataIndexConstants.RacialFeatData.FeatNameIndex] = feat;
            data[DataIndexConstants.RacialFeatData.FocusIndex] = focus;
            data[DataIndexConstants.RacialFeatData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex] = Convert.ToString(minimumHitDiceRequirement);
            data[DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.RacialFeatData.SizeRequirementIndex] = sizeRequirement;
            data[DataIndexConstants.RacialFeatData.PowerIndex] = Convert.ToString(power);
            data[DataIndexConstants.RacialFeatData.MaximumHitDiceRequirementIndex] = Convert.ToString(maximumHitDiceRequirement);
            data[DataIndexConstants.RacialFeatData.RequiredStatIndex] = requiredStat;
            data[DataIndexConstants.RacialFeatData.RequiredStatMinimumValueIndex] = Convert.ToString(requiredStatMinimumValue);

            Data(name, data);
        }
    }
}
