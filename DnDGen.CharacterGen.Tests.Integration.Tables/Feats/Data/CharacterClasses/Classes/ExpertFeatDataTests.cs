﻿using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.CharacterClasses.Classes
{
    [TestFixture]
    public class ExpertFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Expert); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.LightArmorProficiency
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
            "", true)]
        [TestCase(FeatConstants.LightArmorProficiency,
            FeatConstants.LightArmorProficiency,
            "",
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
