using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses.Domains
{
    [TestFixture]
    public class StrengthFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.Strength); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] { FeatConstants.SupernaturalStrength };
            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SupernaturalStrength,
            FeatConstants.SupernaturalStrength,
            "Enhance strength by cleric level, lasts 1 round",
            1,
            "",
            FeatConstants.Frequencies.Day,
            0,
            0,
            0,
            "", true)]
        public override void Data(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
