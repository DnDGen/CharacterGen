using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses.Classes
{
    [TestFixture]
    public class ClericFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Cleric); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.Turn
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SimpleWeaponProficiency,
            FeatConstants.SimpleWeaponProficiency,
            ProficiencyConstants.All,
            0,
            "",
            "",
            1,
            0,
            0,
            "")]
        [TestCase(FeatConstants.LightArmorProficiency,
            FeatConstants.LightArmorProficiency,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "")]
        [TestCase(FeatConstants.MediumArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "")]
        [TestCase(FeatConstants.HeavyArmorProficiency,
            FeatConstants.HeavyArmorProficiency,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "")]
        [TestCase(FeatConstants.ShieldProficiency,
            FeatConstants.ShieldProficiency,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "")]
        [TestCase(FeatConstants.Turn,
            FeatConstants.Turn,
            "Undead",
            3,
            StatConstants.Charisma,
            FeatConstants.Frequencies.Day,
            1,
            0,
            0,
            "")]
        public override void Data(String name, String feat, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength, String sizeRequirement)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement);
        }
    }
}