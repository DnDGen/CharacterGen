using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses.Classes
{
    [TestFixture]
    public class PaladinFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Paladin); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.AuraOfGood,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectEvil,
                FeatConstants.SmiteEvil + "1",
                FeatConstants.SmiteEvil + "2",
                FeatConstants.SmiteEvil + "3",
                FeatConstants.SmiteEvil + "4",
                FeatConstants.SmiteEvil + "5",
                FeatConstants.DivineGrace,
                FeatConstants.LayOnHands,
                FeatConstants.AuraOfCourage,
                FeatConstants.DivineHealth,
                FeatConstants.Turn,
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "1",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "2",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "3",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "4",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "5"
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "1",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            1,
            "",
            FeatConstants.Frequencies.Week,
            6,
            8,
            0,
            "")]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "2",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            2,
            "",
            FeatConstants.Frequencies.Week,
            9,
            11,
            0,
            "")]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "3",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            3,
            "",
            FeatConstants.Frequencies.Week,
            12,
            14,
            0,
            "")]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "4",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            4,
            "",
            FeatConstants.Frequencies.Week,
            15,
            17,
            0,
            "")]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "5",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            5,
            "",
            FeatConstants.Frequencies.Week,
            18,
            0,
            0,
            "")]
        [TestCase(FeatConstants.Turn,
            FeatConstants.Turn,
            "Undead (as cleric of level - 3)",
            3,
            StatConstants.Charisma,
            FeatConstants.Frequencies.Day,
            4,
            0,
            0,
            "")]
        [TestCase(FeatConstants.DivineHealth,
            FeatConstants.DivineHealth,
            "",
            0,
            "",
            "",
            3,
            0,
            0,
            "")]
        [TestCase(FeatConstants.AuraOfCourage,
            FeatConstants.AuraOfCourage,
            "",
            0,
            "",
            "",
            3,
            0,
            0,
            "")]
        [TestCase(FeatConstants.LayOnHands,
            FeatConstants.LayOnHands,
            "",
            0,
            "",
            "",
            2,
            0,
            0,
            "")]
        [TestCase(FeatConstants.DivineGrace,
            FeatConstants.DivineGrace,
            "",
            0,
            "",
            "",
            2,
            0,
            0,
            "")]
        [TestCase(FeatConstants.SmiteEvil + "1",
            FeatConstants.SmiteEvil,
            "",
            1,
            "",
            FeatConstants.Frequencies.Day,
            1,
            4,
            0,
            "")]
        [TestCase(FeatConstants.SmiteEvil + "2",
            FeatConstants.SmiteEvil,
            "",
            2,
            "",
            FeatConstants.Frequencies.Day,
            5,
            9,
            0,
            "")]
        [TestCase(FeatConstants.SmiteEvil + "3",
            FeatConstants.SmiteEvil,
            "",
            3,
            "",
            FeatConstants.Frequencies.Day,
            10,
            14,
            0,
            "")]
        [TestCase(FeatConstants.SmiteEvil + "4",
            FeatConstants.SmiteEvil,
            "",
            4,
            "",
            FeatConstants.Frequencies.Day,
            15,
            19,
            0,
            "")]
        [TestCase(FeatConstants.SmiteEvil + "5",
            FeatConstants.SmiteEvil,
            "",
            5,
            "",
            FeatConstants.Frequencies.Day,
            20,
            0,
            0,
            "")]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectEvil,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectEvil,
            0,
            "",
            FeatConstants.Frequencies.AtWill,
            1,
            0,
            0,
            "")]
        [TestCase(FeatConstants.AuraOfGood,
            FeatConstants.AuraOfGood,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "")]
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
        [TestCase(FeatConstants.MartialWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
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
        public override void Data(String name, String feat, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength, String sizeRequirement)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement);
        }
    }
}
