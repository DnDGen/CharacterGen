using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Items;
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
            "",
            "0",
            "0",
            WeaponProficiencyConstants.All,
            "0",
            "")]
        [TestCase(FeatConstants.ShieldProficiencyId,
            FeatConstants.ShieldProficiencyId,
            "",
            "0",
            "0",
            "",
            "0",
            "")]
        [TestCase(FeatConstants.NaturalArmorId,
            FeatConstants.NaturalArmorId,
            "",
            "0",
            "5",
            "",
            "0",
            "")]
        [TestCase(FeatConstants.HoldBreathId,
            FeatConstants.HoldBreathId,
            "",
            "0",
            "0",
            "",
            "0",
            "")]
        [TestCase(FeatConstants.NaturalWeaponId + "Claw",
            FeatConstants.NaturalWeaponId,
            "",
            "0",
            "0",
            "Claw (x2)",
            "0",
            "")]
        [TestCase(FeatConstants.NaturalWeaponId + "Bite",
            FeatConstants.NaturalWeaponId,
            "",
            "0",
            "0",
            "Bite",
            "0",
            "")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}
