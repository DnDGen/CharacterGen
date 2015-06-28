using System;
using System.Collections.Generic;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public abstract class CharacterClassFeatDataTests : DataTests
    {
        protected override Dictionary<Int32, String> PopulateDataIndices()
        {
            var classFeatSelectionIndices = new Dictionary<Int32, String>();

            classFeatSelectionIndices[DataIndexConstants.CharacterClassFeatData.FeatIdIndex] = "FeatId";
            classFeatSelectionIndices[DataIndexConstants.CharacterClassFeatData.FocusTypeIndex] = "FocusType";
            classFeatSelectionIndices[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityIndex] = "FrequencyQuantity";
            classFeatSelectionIndices[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityStatIndex] = "FrequencyQuantityStat";
            classFeatSelectionIndices[DataIndexConstants.CharacterClassFeatData.FrequencyTimePeriodIndex] = "FrequencyTimePeriod";
            classFeatSelectionIndices[DataIndexConstants.CharacterClassFeatData.MaximumLevelRequirementIndex] = "MaxLevel";
            classFeatSelectionIndices[DataIndexConstants.CharacterClassFeatData.MinimumLevelRequirementIndex] = "MinLevel";
            classFeatSelectionIndices[DataIndexConstants.CharacterClassFeatData.StrengthIndex] = "Strength";

            return classFeatSelectionIndices;
        }

        public virtual void Data(String name, String featId, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength)
        {
            Data(name, featId, focusType, frequencyQuantity.ToString(), frequencyQuantityStat, frequencyTimePeriod, minimumLevel.ToString(), maximumLevel.ToString(), strength.ToString());
        }
    }
}
