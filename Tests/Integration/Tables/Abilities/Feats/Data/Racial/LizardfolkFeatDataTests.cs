using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class LizardfolkFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.LizardfolkId); }
        }

        [TestCase(FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.SimpleWeaponProficiencyId,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.ShieldProficiencyId,
            FeatConstants.ShieldProficiencyId,
            "",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.NaturalArmorId,
            FeatConstants.NaturalArmorId,
            "",
            0,
            "",
            0,
            "",
            5)]
        [TestCase(FeatConstants.HoldBreathId,
            FeatConstants.HoldBreathId,
            "",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.NaturalWeaponId + "Claw",
            FeatConstants.NaturalWeaponId,
            "Claw (x2)",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.NaturalWeaponId + "Bite",
            FeatConstants.NaturalWeaponId,
            "Bite",
            0,
            "",
            0,
            "",
            0)]
        public override void Data(String name, String featId, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength)
        {
            base.Data(name, featId, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength);
        }
    }
}
