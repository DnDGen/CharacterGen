using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.CharacterClasses.Domains
{
    [TestFixture]
    public class WarFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.War); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.WeaponFocus
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.MartialWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.Foci.All,
            0,
            "",
            "",
            0,
            0,
            0,
            "", false)]
        [TestCase(FeatConstants.WeaponFocus,
            FeatConstants.WeaponFocus,
            FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay,
            0,
            "",
            "",
            0,
            0,
            0,
            "", false)]
        public override void ClassFeatData(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.ClassFeatData(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
