using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class MagicFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.Magic); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] { FeatConstants.UseMagicDeviceAsWizard };
            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.UseMagicDeviceAsWizard,
            FeatConstants.UseMagicDeviceAsWizard,
            "Use as if a Wizard of half your Cleric level",
            0,
            "",
            "",
            0,
            0,
            0,
            "")]
        public override void Data(String name, String feat, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength, String sizeRequirement)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement);
        }
    }
}
