using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses.Domains
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
        public override void Data(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
