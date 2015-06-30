using System;
using System.Collections.Generic;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    public abstract class CharacterClassFeatDataTests : DataTests
    {
        protected override void PopulateIndices(IEnumerable<String> collection)
        {
            indices[DataIndexConstants.CharacterClassFeatData.FeatIdIndex] = "FeatId";
            indices[DataIndexConstants.CharacterClassFeatData.FocusTypeIndex] = "FocusType";
            indices[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityIndex] = "FrequencyQuantity";
            indices[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityStatIndex] = "FrequencyQuantityStat";
            indices[DataIndexConstants.CharacterClassFeatData.FrequencyTimePeriodIndex] = "FrequencyTimePeriod";
            indices[DataIndexConstants.CharacterClassFeatData.MaximumLevelRequirementIndex] = "MaxLevel";
            indices[DataIndexConstants.CharacterClassFeatData.MinimumLevelRequirementIndex] = "MinLevel";
            indices[DataIndexConstants.CharacterClassFeatData.StrengthIndex] = "Strength";
        }

        public virtual void Data(String name, String featId, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength)
        {
            Data(name, featId, focusType, frequencyQuantity.ToString(), frequencyQuantityStat, frequencyTimePeriod, minimumLevel.ToString(), maximumLevel.ToString(), strength.ToString());
        }
    }
}
