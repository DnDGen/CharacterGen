using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Tables;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Items
{
    [TestFixture]
    public class ItemGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.ItemGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.TowerShieldProficiency,
                AttributeConstants.Metal,
                PowerConstants.Mundane,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.HeavyArmorProficiency,
            ArmorConstants.SplintMail,
            ArmorConstants.BandedMail,
            ArmorConstants.HalfPlate,
            ArmorConstants.FullPlate)]
        [TestCase(FeatConstants.LightArmorProficiency,
            ArmorConstants.PaddedArmor,
            ArmorConstants.LeatherArmor,
            ArmorConstants.StuddedLeatherArmor,
            ArmorConstants.ChainShirt,
            ArmorConstants.ElvenChain,
            ArmorConstants.CelestialArmor)]
        [TestCase(FeatConstants.MediumArmorProficiency,
            ArmorConstants.HideArmor,
            ArmorConstants.ScaleMail,
            ArmorConstants.Chainmail,
            ArmorConstants.Breastplate,
            ArmorConstants.FullPlateOfSpeed)]
        [TestCase(FeatConstants.ShieldProficiency,
            ArmorConstants.Buckler,
            ArmorConstants.HeavySteelShield,
            ArmorConstants.HeavyWoodenShield,
            ArmorConstants.LightSteelShield,
            ArmorConstants.LightWoodenShield)]
        [TestCase(FeatConstants.TowerShieldProficiency, ArmorConstants.TowerShield)]
        [TestCase(AttributeConstants.Metal,
            ArmorConstants.SplintMail,
            ArmorConstants.BandedMail,
            ArmorConstants.HalfPlate,
            ArmorConstants.FullPlate,
            ArmorConstants.StuddedLeatherArmor,
            ArmorConstants.ChainShirt,
            ArmorConstants.ElvenChain,
            ArmorConstants.CelestialArmor,
            ArmorConstants.ScaleMail,
            ArmorConstants.Chainmail,
            ArmorConstants.Breastplate,
            ArmorConstants.FullPlateOfSpeed,
            ArmorConstants.HeavySteelShield,
            ArmorConstants.LightSteelShield)]
        [TestCase(AttributeConstants.Specific,
            ArmorConstants.ElvenChain,
            ArmorConstants.CelestialArmor,
            ArmorConstants.FullPlateOfSpeed)]
        public void ItemGroup(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
