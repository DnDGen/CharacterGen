using CharacterGen.Abilities.Feats;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Items;

namespace CharacterGen.Tests.Integration.Tables.Items
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
                WeaponConstants.Arrow,
                WeaponConstants.CrossbowBolt,
                WeaponConstants.SlingBullet,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.MediumArmorProficiency,
            ArmorConstants.HideArmor,
            ArmorConstants.ScaleMail,
            ArmorConstants.Chainmail,
            ArmorConstants.Breastplate,
            ArmorConstants.FullPlateOfSpeed)]
        [TestCase(FeatConstants.HeavyArmorProficiency,
            ArmorConstants.SplintMail,
            ArmorConstants.BandedMail,
            ArmorConstants.HalfPlate,
            ArmorConstants.FullPlate)]
        [TestCase(FeatConstants.ShieldProficiency,
            ArmorConstants.Buckler,
            ArmorConstants.HeavySteelShield,
            ArmorConstants.HeavyWoodenShield,
            ArmorConstants.LightSteelShield,
            ArmorConstants.LightWoodenShield)]
        [TestCase(FeatConstants.TowerShieldProficiency,
            ArmorConstants.TowerShield)]
        [TestCase(FeatConstants.LightArmorProficiency,
            ArmorConstants.PaddedArmor,
            ArmorConstants.LeatherArmor,
            ArmorConstants.StuddedLeatherArmor,
            ArmorConstants.ChainShirt,
            ArmorConstants.ElvenChain,
            ArmorConstants.CelestialArmor)]
        [TestCase(WeaponConstants.Arrow,
            WeaponConstants.CompositeLongbow,
            WeaponConstants.CompositePlus0Longbow,
            WeaponConstants.CompositePlus0Shortbow,
            WeaponConstants.CompositePlus1Longbow,
            WeaponConstants.CompositePlus1Shortbow,
            WeaponConstants.CompositePlus2Longbow,
            WeaponConstants.CompositePlus2Shortbow,
            WeaponConstants.CompositePlus3Longbow,
            WeaponConstants.CompositePlus4Longbow,
            WeaponConstants.CompositeShortbow,
            WeaponConstants.Longbow,
            WeaponConstants.Shortbow)]
        [TestCase(WeaponConstants.CrossbowBolt,
            WeaponConstants.HandCrossbow,
            WeaponConstants.HeavyCrossbow,
            WeaponConstants.HeavyRepeatingCrossbow,
            WeaponConstants.LightCrossbow,
            WeaponConstants.LightRepeatingCrossbow)]
        [TestCase(WeaponConstants.SlingBullet, WeaponConstants.Sling)]
        public void ItemGroup(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
