using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Items;

namespace CharacterGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class ArmorBonusesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.ArmorBonuses; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                ArmorConstants.ArmorOfArrowAttraction,
                ArmorConstants.ArmorOfRage,
                ArmorConstants.BandedMail,
                ArmorConstants.BandedMailOfLuck,
                ArmorConstants.Breastplate,
                ArmorConstants.BreastplateOfCommand,
                ArmorConstants.CelestialArmor,
                ArmorConstants.Chainmail,
                ArmorConstants.ChainShirt,
                ArmorConstants.DemonArmor,
                ArmorConstants.DwarvenPlate,
                ArmorConstants.ElvenChain,
                ArmorConstants.FullPlate,
                ArmorConstants.FullPlateOfSpeed,
                ArmorConstants.HalfPlate,
                ArmorConstants.HideArmor,
                ArmorConstants.LeatherArmor,
                ArmorConstants.PaddedArmor,
                ArmorConstants.PlateArmorOfTheDeep,
                ArmorConstants.RhinoHide,
                ArmorConstants.ScaleMail,
                ArmorConstants.SplintMail,
                ArmorConstants.StuddedLeatherArmor,
                ArmorConstants.AbsorbingShield,
                ArmorConstants.Buckler,
                ArmorConstants.CastersShield,
                ArmorConstants.HeavySteelShield,
                ArmorConstants.HeavyWoodenShield,
                ArmorConstants.LightSteelShield,
                ArmorConstants.LightWoodenShield,
                ArmorConstants.LionsShield,
                ArmorConstants.SpinedShield,
                ArmorConstants.TowerShield,
                ArmorConstants.WingedShield
            };

            AssertCollectionNames(names);
        }

        [TestCase(ArmorConstants.PaddedArmor, 1)]
        [TestCase(ArmorConstants.LeatherArmor, 2)]
        [TestCase(ArmorConstants.StuddedLeatherArmor, 3)]
        [TestCase(ArmorConstants.ChainShirt, 4)]
        [TestCase(ArmorConstants.HideArmor, 3)]
        [TestCase(ArmorConstants.ScaleMail, 4)]
        [TestCase(ArmorConstants.Chainmail, 5)]
        [TestCase(ArmorConstants.Breastplate, 5)]
        [TestCase(ArmorConstants.SplintMail, 6)]
        [TestCase(ArmorConstants.BandedMail, 6)]
        [TestCase(ArmorConstants.HalfPlate, 7)]
        [TestCase(ArmorConstants.FullPlate, 8)]
        [TestCase(ArmorConstants.ArmorOfArrowAttraction, 8)]
        [TestCase(ArmorConstants.ArmorOfRage, 8)]
        [TestCase(ArmorConstants.BandedMailOfLuck, 6)]
        [TestCase(ArmorConstants.BreastplateOfCommand, 5)]
        [TestCase(ArmorConstants.CelestialArmor, 5)]
        [TestCase(ArmorConstants.DemonArmor, 8)]
        [TestCase(ArmorConstants.DwarvenPlate, 8)]
        [TestCase(ArmorConstants.ElvenChain, 5)]
        [TestCase(ArmorConstants.FullPlateOfSpeed, 8)]
        [TestCase(ArmorConstants.PlateArmorOfTheDeep, 8)]
        [TestCase(ArmorConstants.RhinoHide, 3)]
        [TestCase(ArmorConstants.AbsorbingShield, 2)]
        [TestCase(ArmorConstants.Buckler, 1)]
        [TestCase(ArmorConstants.CastersShield, 1)]
        [TestCase(ArmorConstants.HeavySteelShield, 2)]
        [TestCase(ArmorConstants.HeavyWoodenShield, 2)]
        [TestCase(ArmorConstants.LightSteelShield, 1)]
        [TestCase(ArmorConstants.LightWoodenShield, 1)]
        [TestCase(ArmorConstants.LionsShield, 2)]
        [TestCase(ArmorConstants.SpinedShield, 2)]
        [TestCase(ArmorConstants.TowerShield, 4)]
        [TestCase(ArmorConstants.WingedShield, 2)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
