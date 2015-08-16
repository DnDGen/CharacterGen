using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items
{
    [TestFixture]
    public class ItemGroupsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.ItemGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.Weapons,
                WeaponConstants.Arrow,
                WeaponConstants.BastardSword,
                WeaponConstants.Battleaxe,
                WeaponConstants.Club,
                WeaponConstants.CrossbowBolt,
                WeaponConstants.Dagger,
                WeaponConstants.Dart,
                WeaponConstants.DireFlail,
                WeaponConstants.DwarvenUrgrosh,
                WeaponConstants.DwarvenWaraxe,
                WeaponConstants.Falchion,
                WeaponConstants.Gauntlet,
                WeaponConstants.Glaive,
                WeaponConstants.GnomeHookedHammer,
                WeaponConstants.Greataxe,
                WeaponConstants.Greatclub,
                WeaponConstants.Greatsword,
                WeaponConstants.Guisarme,
                WeaponConstants.Halberd,
                WeaponConstants.Halfspear,
                WeaponConstants.Handaxe,
                WeaponConstants.HandCrossbow,
                WeaponConstants.HeavyCrossbow,
                WeaponConstants.HeavyFlail,
                WeaponConstants.HeavyMace,
                WeaponConstants.HeavyPick,
                WeaponConstants.Javelin,
                WeaponConstants.Kama,
                WeaponConstants.Kukri,
                WeaponConstants.Lance,
                WeaponConstants.LightCrossbow,
                WeaponConstants.LightFlail,
                WeaponConstants.LightHammer,
                WeaponConstants.LightMace,
                WeaponConstants.LightPick,
                WeaponConstants.Longbow,
                WeaponConstants.Longspear,
                WeaponConstants.Longsword,
                WeaponConstants.Morningstar,
                WeaponConstants.Net,
                WeaponConstants.Nunchaku,
                WeaponConstants.OrcDoubleAxe,
                WeaponConstants.PunchingDagger,
                WeaponConstants.Quarterstaff,
                WeaponConstants.Ranseur,
                WeaponConstants.Rapier,
                WeaponConstants.RepeatingCrossbow,
                WeaponConstants.Sap,
                WeaponConstants.Scimitar,
                WeaponConstants.Scythe,
                WeaponConstants.Shortbow,
                WeaponConstants.Shortspear,
                WeaponConstants.ShortSword,
                WeaponConstants.Shuriken,
                WeaponConstants.Siangham,
                WeaponConstants.Sickle,
                WeaponConstants.Sling,
                WeaponConstants.SlingBullet,
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip
            };

            AssertCollectionNames(names);
        }

        [TestCase(WeaponConstants.Arrow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.BastardSword, WeaponConstants.BastardSword)]
        [TestCase(WeaponConstants.Battleaxe, WeaponConstants.Battleaxe)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Club)]
        [TestCase(WeaponConstants.CrossbowBolt, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger)]
        [TestCase(WeaponConstants.Dart, WeaponConstants.Dart)]
        [TestCase(WeaponConstants.DireFlail, WeaponConstants.DireFlail)]
        [TestCase(WeaponConstants.DwarvenUrgrosh, WeaponConstants.DwarvenUrgrosh)]
        [TestCase(WeaponConstants.DwarvenWaraxe, WeaponConstants.DwarvenWaraxe)]
        [TestCase(WeaponConstants.Falchion, WeaponConstants.Falchion)]
        [TestCase(WeaponConstants.Gauntlet, WeaponConstants.Gauntlet)]
        [TestCase(WeaponConstants.Glaive, WeaponConstants.Glaive)]
        [TestCase(WeaponConstants.GnomeHookedHammer, WeaponConstants.GnomeHookedHammer)]
        [TestCase(WeaponConstants.Greataxe, WeaponConstants.Greataxe)]
        [TestCase(WeaponConstants.Greatclub, WeaponConstants.Greatclub)]
        [TestCase(WeaponConstants.Greatsword, WeaponConstants.Greatsword)]
        [TestCase(WeaponConstants.Guisarme, WeaponConstants.Guisarme)]
        [TestCase(WeaponConstants.Halberd, WeaponConstants.Halberd)]
        [TestCase(WeaponConstants.Halfspear, WeaponConstants.Halfspear)]
        [TestCase(WeaponConstants.Handaxe, WeaponConstants.Handaxe)]
        [TestCase(WeaponConstants.HandCrossbow, WeaponConstants.HandCrossbow, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.HeavyCrossbow, WeaponConstants.HeavyCrossbow, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.HeavyFlail, WeaponConstants.HeavyFlail)]
        [TestCase(WeaponConstants.HeavyMace, WeaponConstants.HeavyMace)]
        [TestCase(WeaponConstants.HeavyPick, WeaponConstants.HeavyPick)]
        [TestCase(WeaponConstants.Javelin, WeaponConstants.Javelin)]
        [TestCase(WeaponConstants.Kama, WeaponConstants.Kama)]
        [TestCase(WeaponConstants.Kukri, WeaponConstants.Kukri)]
        [TestCase(WeaponConstants.Lance, WeaponConstants.Lance)]
        [TestCase(WeaponConstants.LightCrossbow, WeaponConstants.LightCrossbow, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.LightFlail, WeaponConstants.LightFlail)]
        [TestCase(WeaponConstants.LightHammer, WeaponConstants.LightHammer)]
        [TestCase(WeaponConstants.LightMace, WeaponConstants.LightMace)]
        [TestCase(WeaponConstants.LightPick, WeaponConstants.LightPick)]
        [TestCase(WeaponConstants.Longbow, WeaponConstants.Longbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.Longspear, WeaponConstants.Longspear)]
        [TestCase(WeaponConstants.Longsword, WeaponConstants.Longsword)]
        [TestCase(WeaponConstants.Morningstar, WeaponConstants.Morningstar)]
        [TestCase(WeaponConstants.Net, WeaponConstants.Net)]
        [TestCase(WeaponConstants.Nunchaku, WeaponConstants.Nunchaku)]
        [TestCase(WeaponConstants.OrcDoubleAxe, WeaponConstants.OrcDoubleAxe)]
        [TestCase(WeaponConstants.PunchingDagger, WeaponConstants.PunchingDagger)]
        [TestCase(WeaponConstants.Quarterstaff, WeaponConstants.Quarterstaff)]
        [TestCase(WeaponConstants.Ranseur, WeaponConstants.Ranseur)]
        [TestCase(WeaponConstants.Rapier, WeaponConstants.Rapier)]
        [TestCase(WeaponConstants.RepeatingCrossbow, WeaponConstants.RepeatingCrossbow, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.Sap, WeaponConstants.Sap)]
        [TestCase(WeaponConstants.Scimitar, WeaponConstants.Scimitar)]
        [TestCase(WeaponConstants.Scythe, WeaponConstants.Scythe)]
        [TestCase(WeaponConstants.Shortbow, WeaponConstants.Shortbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.Shortspear, WeaponConstants.Shortspear)]
        [TestCase(WeaponConstants.ShortSword, WeaponConstants.ShortSword)]
        [TestCase(WeaponConstants.Shuriken, WeaponConstants.Shuriken)]
        [TestCase(WeaponConstants.Siangham, WeaponConstants.Siangham)]
        [TestCase(WeaponConstants.Sickle, WeaponConstants.Sickle)]
        [TestCase(WeaponConstants.Sling, WeaponConstants.Sling, WeaponConstants.SlingBullet)]
        [TestCase(WeaponConstants.SlingBullet, WeaponConstants.SlingBullet)]
        [TestCase(WeaponConstants.SpikedChain, WeaponConstants.SpikedChain)]
        [TestCase(WeaponConstants.SpikedGauntlet, WeaponConstants.SpikedGauntlet)]
        [TestCase(WeaponConstants.ThrowingAxe, WeaponConstants.ThrowingAxe)]
        [TestCase(WeaponConstants.Trident, WeaponConstants.Trident)]
        [TestCase(WeaponConstants.TwoBladedSword, WeaponConstants.TwoBladedSword)]
        [TestCase(WeaponConstants.Warhammer, WeaponConstants.Warhammer)]
        [TestCase(WeaponConstants.Whip, WeaponConstants.Whip)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void Weapons()
        {
            var items = new[]
            {
                WeaponConstants.Arrow,
                WeaponConstants.BastardSword,
                WeaponConstants.Battleaxe,
                WeaponConstants.Club,
                WeaponConstants.CrossbowBolt,
                WeaponConstants.Dagger,
                WeaponConstants.Dart,
                WeaponConstants.DireFlail,
                WeaponConstants.DwarvenUrgrosh,
                WeaponConstants.DwarvenWaraxe,
                WeaponConstants.Falchion,
                WeaponConstants.Gauntlet,
                WeaponConstants.Glaive,
                WeaponConstants.GnomeHookedHammer,
                WeaponConstants.Greataxe,
                WeaponConstants.Greatclub,
                WeaponConstants.Greatsword,
                WeaponConstants.Guisarme,
                WeaponConstants.Halberd,
                WeaponConstants.Halfspear,
                WeaponConstants.Handaxe,
                WeaponConstants.HandCrossbow,
                WeaponConstants.HeavyCrossbow,
                WeaponConstants.HeavyFlail,
                WeaponConstants.HeavyMace,
                WeaponConstants.HeavyPick,
                WeaponConstants.Javelin,
                WeaponConstants.Kama,
                WeaponConstants.Kukri,
                WeaponConstants.Lance,
                WeaponConstants.LightCrossbow,
                WeaponConstants.LightFlail,
                WeaponConstants.LightHammer,
                WeaponConstants.LightMace,
                WeaponConstants.LightPick,
                WeaponConstants.Longbow,
                WeaponConstants.Longspear,
                WeaponConstants.Longsword,
                WeaponConstants.Morningstar,
                WeaponConstants.Net,
                WeaponConstants.Nunchaku,
                WeaponConstants.OrcDoubleAxe,
                WeaponConstants.PunchingDagger,
                WeaponConstants.Quarterstaff,
                WeaponConstants.Ranseur,
                WeaponConstants.Rapier,
                WeaponConstants.RepeatingCrossbow,
                WeaponConstants.Sap,
                WeaponConstants.Scimitar,
                WeaponConstants.Scythe,
                WeaponConstants.Shortbow,
                WeaponConstants.Shortspear,
                WeaponConstants.ShortSword,
                WeaponConstants.Shuriken,
                WeaponConstants.Siangham,
                WeaponConstants.Sickle,
                WeaponConstants.Sling,
                WeaponConstants.SlingBullet,
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip
            };

            base.DistinctCollection(GroupConstants.Weapons, items);
        }
    }
}
