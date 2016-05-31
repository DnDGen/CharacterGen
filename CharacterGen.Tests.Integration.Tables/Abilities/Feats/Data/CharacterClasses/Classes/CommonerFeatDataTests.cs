using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses.Classes
{
    [TestFixture]
    public class CommonerFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Commoner); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SimpleWeaponProficiency
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SimpleWeaponProficiency,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.Foci.All,
            0,
            "",
            "",
            1,
            0,
            0,
            "", false)]
        public override void Data(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
