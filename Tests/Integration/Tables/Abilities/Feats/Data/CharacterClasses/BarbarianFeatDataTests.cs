using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class BarbarianFeatDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Barbarian); }
        }

        [TestCase(FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.SimpleWeaponProficiencyId,
            "0",
            WeaponProficiencyConstants.All,
            "0",
            "0",
            "",
            "0")]
        [TestCase(CharacterClassConstants.Barbarian,
                FeatConstants.MartialWeaponProficiencyId,
                FeatConstants.LightArmorProficiencyId,
                FeatConstants.MediumArmorProficiencyId,
                FeatConstants.ShieldProficiencyId,
                FeatConstants.FastMovementId,
                FeatConstants.IlliteracyId)]
        [TestCase(FeatConstants.RageId + "1",
            FeatConstants.RageId,
            "1",
            "",
            "0",
            "1",
            FeatConstants.Frequencies.Day,
            "3")]
        [TestCase(FeatConstants.RageId + "2",
            FeatConstants.RageId,
            "4",
            "",
            "0",
            "2",
            FeatConstants.Frequencies.Day,
            "7")]
        [TestCase(FeatConstants.RageId + "3",
            FeatConstants.RageId,
            "8",
            "",
            "0",
            "3",
            FeatConstants.Frequencies.Day,
            "11")]
        [TestCase(FeatConstants.RageId + "4",
            FeatConstants.RageId,
            "12",
            "",
            "0",
            "4",
            FeatConstants.Frequencies.Day,
            "15")]
        [TestCase(FeatConstants.RageId + "5",
            FeatConstants.RageId,
            "16",
            "",
            "0",
            "5",
            FeatConstants.Frequencies.Day,
            "19")]
        [TestCase(FeatConstants.RageId + "6",
            FeatConstants.RageId,
            "20",
            "",
            "0",
            "6",
            FeatConstants.Frequencies.Day,
            "0")]
        [TestCase(FeatConstants.UncannyDodgeId,
            FeatConstants.UncannyDodgeId,
            "2",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.TrapSenseId + "1",
            FeatConstants.TrapSenseId,
            "3",
            "",
            "1",
            "0",
            "",
            "5")]
        [TestCase(FeatConstants.TrapSenseId + "2",
            FeatConstants.TrapSenseId,
            "6",
            "",
            "2",
            "0",
            "",
            "8")]
        [TestCase(FeatConstants.TrapSenseId + "3",
            FeatConstants.TrapSenseId,
            "9",
            "",
            "3",
            "0",
            "",
            "11")]
        [TestCase(FeatConstants.TrapSenseId + "4",
            FeatConstants.TrapSenseId,
            "12",
            "",
            "4",
            "0",
            "",
            "14")]
        [TestCase(FeatConstants.TrapSenseId + "5",
            FeatConstants.TrapSenseId,
            "15",
            "",
            "5",
            "0",
            "",
            "17")]
        [TestCase(FeatConstants.TrapSenseId + "6",
            FeatConstants.TrapSenseId,
            "18",
            "",
            "6",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.ImprovedUncannyDodgeId,
            FeatConstants.ImprovedUncannyDodgeId,
            "5",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.DamageReductionId + "1",
            FeatConstants.DamageReductionId,
            "7",
            "",
            "1",
            "0",
            "",
            "9")]
        [TestCase(FeatConstants.DamageReductionId + "2",
            FeatConstants.DamageReductionId,
            "10",
            "",
            "2",
            "0",
            "",
            "12")]
        [TestCase(FeatConstants.DamageReductionId + "3",
            FeatConstants.DamageReductionId,
            "13",
            "",
            "3",
            "0",
            "",
            "15")]
        [TestCase(FeatConstants.DamageReductionId + "4",
            FeatConstants.DamageReductionId,
            "16",
            "",
            "4",
            "0",
            "",
            "18")]
        [TestCase(FeatConstants.DamageReductionId + "5",
            FeatConstants.DamageReductionId,
            "19",
            "",
            "5",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.GreaterRageId,
            FeatConstants.GreaterRageId,
            "11",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.IndomitableWillId,
            FeatConstants.IndomitableWillId,
            "14",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.TirelessRageId,
            FeatConstants.TirelessRageId,
            "17",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.MightyRageId,
            FeatConstants.MightyRageId,
            "20",
            "",
            "0",
            "0",
            "",
            "0")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}
