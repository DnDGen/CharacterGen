using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Additional
{
    [TestFixture]
    public class AdditionalFeatDataTests : DataTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.AdditionalFeatData; }
        }
        protected override void PopulateIndices(IEnumerable<String> collection)
        {
            indices[DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex] = "BaseAttackRequirement";
            indices[DataIndexConstants.AdditionalFeatData.FocusTypeIndex] = "FocusType";
            indices[DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex] = "FrequencyQuantity";
            indices[DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex] = "FrequencyTimePeriod";
            indices[DataIndexConstants.AdditionalFeatData.IsFighterFeatIndex] = "IsFighterFeat";
            indices[DataIndexConstants.AdditionalFeatData.IsWizardFeatIndex] = "IsWizardFeat";
            indices[DataIndexConstants.AdditionalFeatData.StrengthIndex] = "Strength";
        }

        [TestCase(FeatConstants.AcrobaticId,
            0,
            "",
            0,
            "",
            false,
            false,
            2)]
        public void Data(String name, Int32 baseAttackRequirement, String focusType, Int32 frequencyQuantity, String frequencyTimePeriod, Boolean isFighterFeat, Boolean isWizardFeat, Int32 strength)
        {
            var data = new List<String>();
            for (var i = 0; i < 7; i++)
                data.Add(String.Empty);

            data[DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex] = Convert.ToString(baseAttackRequirement);
            data[DataIndexConstants.AdditionalFeatData.FocusTypeIndex] = focusType;
            data[DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.AdditionalFeatData.IsFighterFeatIndex] = Convert.ToString(isFighterFeat);
            data[DataIndexConstants.AdditionalFeatData.IsWizardFeatIndex] = Convert.ToString(isWizardFeat);
            data[DataIndexConstants.AdditionalFeatData.StrengthIndex] = Convert.ToString(strength);

            base.Data(name, data);
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.AcrobaticId
            };

            AssertCollectionNames(names);
        }
    }
}
