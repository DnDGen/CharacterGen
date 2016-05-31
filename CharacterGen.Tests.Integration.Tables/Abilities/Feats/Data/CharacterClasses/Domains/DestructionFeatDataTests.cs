using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses.Domains
{
    [TestFixture]
    public class DestructionFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.Destruction); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] { FeatConstants.Smite };
            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Smite,
            FeatConstants.Smite,
            "",
            1,
            "",
            FeatConstants.Frequencies.Day,
            1,
            0,
            4,
            "", true)]
        public override void Data(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
