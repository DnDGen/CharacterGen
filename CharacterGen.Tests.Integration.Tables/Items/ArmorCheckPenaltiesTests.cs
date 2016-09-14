using CharacterGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Items;

namespace CharacterGen.Tests.Integration.Tables.Items
{
    [TestFixture]
    public class ArmorCheckPenaltiesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.ArmorCheckPenalties; }
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
                ArmorConstants.WingedShield,
                TraitConstants.SpecialMaterials.Adamantine,
                TraitConstants.SpecialMaterials.Darkwood,
                TraitConstants.SpecialMaterials.Mithral
            };

            AssertCollectionNames(names);
        }

        [TestCase(ArmorConstants.PaddedArmor, 0)]
        [TestCase(ArmorConstants.LeatherArmor, 0)]
        [TestCase(ArmorConstants.StuddedLeatherArmor, -1)]
        [TestCase(ArmorConstants.ChainShirt, -2)]
        [TestCase(ArmorConstants.HideArmor, -3)]
        [TestCase(ArmorConstants.ScaleMail, -4)]
        [TestCase(ArmorConstants.Chainmail, -5)]
        [TestCase(ArmorConstants.Breastplate, -4)]
        [TestCase(ArmorConstants.SplintMail, -7)]
        [TestCase(ArmorConstants.BandedMail, -6)]
        [TestCase(ArmorConstants.HalfPlate, -7)]
        [TestCase(ArmorConstants.FullPlate, -6)]
        [TestCase(ArmorConstants.ArmorOfArrowAttraction, -6)]
        [TestCase(ArmorConstants.ArmorOfRage, -6)]
        [TestCase(ArmorConstants.BandedMailOfLuck, -6)]
        [TestCase(ArmorConstants.BreastplateOfCommand, -4)]
        [TestCase(ArmorConstants.CelestialArmor, -2)]
        [TestCase(ArmorConstants.DemonArmor, -6)]
        [TestCase(ArmorConstants.DwarvenPlate, -6)]
        [TestCase(ArmorConstants.ElvenChain, -5)]
        [TestCase(ArmorConstants.FullPlateOfSpeed, -6)]
        [TestCase(ArmorConstants.PlateArmorOfTheDeep, -6)]
        [TestCase(ArmorConstants.RhinoHide, -1)]
        [TestCase(ArmorConstants.AbsorbingShield, -2)]
        [TestCase(ArmorConstants.Buckler, -1)]
        [TestCase(ArmorConstants.CastersShield, -1)]
        [TestCase(ArmorConstants.HeavySteelShield, -2)]
        [TestCase(ArmorConstants.HeavyWoodenShield, -2)]
        [TestCase(ArmorConstants.LightSteelShield, -1)]
        [TestCase(ArmorConstants.LightWoodenShield, -1)]
        [TestCase(ArmorConstants.LionsShield, -2)]
        [TestCase(ArmorConstants.SpinedShield, -2)]
        [TestCase(ArmorConstants.TowerShield, -10)]
        [TestCase(ArmorConstants.WingedShield, -2)]
        [TestCase(TraitConstants.SpecialMaterials.Adamantine, 1)]
        [TestCase(TraitConstants.SpecialMaterials.Darkwood, 2)]
        [TestCase(TraitConstants.SpecialMaterials.Mithral, 3)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
