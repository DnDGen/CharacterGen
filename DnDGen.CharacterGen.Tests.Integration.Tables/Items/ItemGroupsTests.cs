using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Tables;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System.Linq;

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
                AttributeConstants.Specific,
                ItemTypeConstants.Weapon,
                AttributeConstants.Ammunition,
                AttributeConstants.Melee,
                AttributeConstants.Ranged,
                AttributeConstants.TwoHanded,
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

        [Test]
        public void ItemGroup_Weapons()
        {
            var weapons = WeaponConstants.GetAllWeapons(false, false).ToArray();
            base.DistinctCollection(ItemTypeConstants.Weapon, weapons);
        }

        [Test]
        public void ItemGroup_Ammunition()
        {
            var weapons = WeaponConstants
                .GetAllAmmunition(false, false)
                .Except(new[] {
                    WeaponConstants.Shuriken,
                })
                .ToArray();
            base.DistinctCollection(AttributeConstants.Ammunition, weapons);
        }

        [Test]
        public void ItemGroup_Melee()
        {
            var weapons = WeaponConstants
                .GetAllMelee(false, false)
                .Except(new[] {
                    WeaponConstants.ThrowingAxe,
                })
                .ToArray();
            base.DistinctCollection(AttributeConstants.Melee, weapons);
        }

        [Test]
        public void ItemGroup_Ranged()
        {
            var weapons = WeaponConstants
                .GetAllRanged(false, false, false)
                .Union(new[] {
                    WeaponConstants.Shuriken,
                    WeaponConstants.ThrowingAxe,
                })
                .ToArray();
            base.DistinctCollection(AttributeConstants.Ranged, weapons);
        }

        [Test]
        public void ItemGroup_TwoHanded()
        {
            var weapons = WeaponConstants.GetAllTwoHandedMelee(false, false).ToArray();
            base.DistinctCollection(AttributeConstants.TwoHanded, weapons);
        }
    }
}
