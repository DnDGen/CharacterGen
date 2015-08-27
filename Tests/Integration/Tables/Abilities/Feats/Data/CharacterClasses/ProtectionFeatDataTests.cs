using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class ProtectionFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.Protection); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] { FeatConstants.SpellLikeAbility };
            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SpellLikeAbility,
            FeatConstants.SpellLikeAbility,
            "Grant someone you touch a resistance bonus equal to your cleric level on his or her next saving throw.",
            1,
            "",
            FeatConstants.Frequencies.Day,
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
