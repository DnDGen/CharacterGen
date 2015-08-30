using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses.Domains
{
    [TestFixture]
    public class WarFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.War); }
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
            ProficiencyConstants.All,
            0,
            "",
            "",
            0,
            0,
            0,
            "")]
        [TestCase(FeatConstants.WeaponFocus,
            FeatConstants.WeaponFocus,
            GroupConstants.WeaponsWithUnarmedAndGrappleAndRay,
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
