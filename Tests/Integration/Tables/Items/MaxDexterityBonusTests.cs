using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items
{
    [TestFixture]
    public class MaxDexterityBonusTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.MaxDexterityBonus; }
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
                ArmorConstants.StuddedLeatherArmor
            };

            AssertCollectionNames(names);
        }

        [TestCase(ArmorConstants.PaddedArmor, 8)]
        [TestCase(ArmorConstants.LeatherArmor, 6)]
        [TestCase(ArmorConstants.StuddedLeatherArmor, 5)]
        [TestCase(ArmorConstants.ChainShirt, 4)]
        [TestCase(ArmorConstants.HideArmor, 4)]
        [TestCase(ArmorConstants.ScaleMail, 3)]
        [TestCase(ArmorConstants.Chainmail, 2)]
        [TestCase(ArmorConstants.Breastplate, 3)]
        [TestCase(ArmorConstants.SplintMail, 0)]
        [TestCase(ArmorConstants.BandedMail, 1)]
        [TestCase(ArmorConstants.HalfPlate, 0)]
        [TestCase(ArmorConstants.FullPlate, 1)]
        [TestCase(ArmorConstants.ArmorOfArrowAttraction, 1)]
        [TestCase(ArmorConstants.ArmorOfRage, 1)]
        [TestCase(ArmorConstants.BandedMailOfLuck, 1)]
        [TestCase(ArmorConstants.BreastplateOfCommand, 3)]
        [TestCase(ArmorConstants.CelestialArmor, 8)]
        [TestCase(ArmorConstants.DemonArmor, 1)]
        [TestCase(ArmorConstants.DwarvenPlate, 1)]
        [TestCase(ArmorConstants.ElvenChain, 2)]
        [TestCase(ArmorConstants.FullPlateOfSpeed, 1)]
        [TestCase(ArmorConstants.PlateArmorOfTheDeep, 1)]
        [TestCase(ArmorConstants.RhinoHide, 4)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
