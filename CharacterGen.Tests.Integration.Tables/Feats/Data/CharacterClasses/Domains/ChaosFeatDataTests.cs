using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Feats.Data.CharacterClasses.Domains
{
    [TestFixture]
    public class ChaosFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.Chaos); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] { FeatConstants.CastSpellBonus };
            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.CastSpellBonus,
            FeatConstants.CastSpellBonus,
            CharacterClassConstants.Domains.Chaos,
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        public override void ClassFeatData(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.ClassFeatData(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
