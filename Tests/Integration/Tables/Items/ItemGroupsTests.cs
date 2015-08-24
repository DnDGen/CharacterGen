using CharacterGen.Common.Abilities.Feats;
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
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.TowerShieldProficiency,
                GroupConstants.Weapons,
                WeaponConstants.Arrow,
                WeaponConstants.AssassinsDagger,
                WeaponConstants.BastardSword,
                WeaponConstants.Battleaxe,
                WeaponConstants.BerserkingSword,
                WeaponConstants.Club,
                WeaponConstants.CompositePlus0Longbow,
                WeaponConstants.CompositePlus0Shortbow,
                WeaponConstants.CompositePlus1Longbow,
                WeaponConstants.CompositePlus1Shortbow,
                WeaponConstants.CompositePlus2Longbow,
                WeaponConstants.CompositePlus2Shortbow,
                WeaponConstants.CompositePlus3Longbow,
                WeaponConstants.CompositePlus4Longbow,
                WeaponConstants.CrossbowBolt,
                WeaponConstants.CursedBackbiterSpear,
                WeaponConstants.CursedMinus2Sword,
                WeaponConstants.Dagger,
                WeaponConstants.DaggerOfVenom,
                WeaponConstants.Dart,
                WeaponConstants.DireFlail,
                WeaponConstants.DwarvenThrower,
                WeaponConstants.DwarvenUrgrosh,
                WeaponConstants.DwarvenWaraxe,
                WeaponConstants.Falchion,
                WeaponConstants.FlameTongue,
                WeaponConstants.FrostBrand,
                WeaponConstants.Gauntlet,
                WeaponConstants.Glaive,
                WeaponConstants.GnomeHookedHammer,
                WeaponConstants.Greataxe,
                WeaponConstants.GreaterSlayingArrow,
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
                WeaponConstants.HolyAvenger,
                WeaponConstants.Javelin,
                WeaponConstants.JavelinOfLightning,
                WeaponConstants.Kama,
                WeaponConstants.Kukri,
                WeaponConstants.Lance,
                WeaponConstants.LifeDrinker,
                WeaponConstants.LightCrossbow,
                WeaponConstants.LightFlail,
                WeaponConstants.LightHammer,
                WeaponConstants.LightMace,
                WeaponConstants.LightPick,
                WeaponConstants.Longbow,
                WeaponConstants.Longspear,
                WeaponConstants.Longsword,
                WeaponConstants.Morningstar,
                WeaponConstants.NineLivesStealer,
                WeaponConstants.Net,
                WeaponConstants.NetOfSnaring,
                WeaponConstants.Nunchaku,
                WeaponConstants.Oathbow,
                WeaponConstants.OrcDoubleAxe,
                WeaponConstants.PunchingDagger,
                WeaponConstants.Quarterstaff,
                WeaponConstants.Ranseur,
                WeaponConstants.Rapier,
                WeaponConstants.RapierOfPuncturing,
                WeaponConstants.HeavyRepeatingCrossbow,
                WeaponConstants.LightRepeatingCrossbow,
                WeaponConstants.Sap,
                WeaponConstants.Scimitar,
                WeaponConstants.ScreamingBolt,
                WeaponConstants.Scythe,
                WeaponConstants.Shatterspike,
                WeaponConstants.ShiftersSorrow,
                WeaponConstants.Shortbow,
                WeaponConstants.Shortspear,
                WeaponConstants.ShortSword,
                WeaponConstants.Shuriken,
                WeaponConstants.Siangham,
                WeaponConstants.Sickle,
                WeaponConstants.SlayingArrow,
                WeaponConstants.SleepArrow,
                WeaponConstants.Sling,
                WeaponConstants.SlingBullet,
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.SunBlade,
                WeaponConstants.SwordOfLifeStealing,
                WeaponConstants.SwordOfSubtlety,
                WeaponConstants.SwordOfThePlanes,
                WeaponConstants.SylvanScimitar,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TridentOfFishCommand,
                WeaponConstants.TridentOfWarning,
                WeaponConstants.TwoBladedSword,
                GroupConstants.TwoHanded,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.MediumArmorProficiency,
            ArmorConstants.HideArmor,
            ArmorConstants.ScaleMail,
            ArmorConstants.Chainmail,
            ArmorConstants.Breastplate)]
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
            ArmorConstants.ChainShirt)]
        [TestCase(WeaponConstants.Arrow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.AssassinsDagger, WeaponConstants.Dagger)]
        [TestCase(WeaponConstants.BastardSword, WeaponConstants.BastardSword)]
        [TestCase(WeaponConstants.Battleaxe, WeaponConstants.Battleaxe)]
        [TestCase(WeaponConstants.BerserkingSword, WeaponConstants.Greatsword)]
        [TestCase(WeaponConstants.Club, WeaponConstants.Club)]
        [TestCase(WeaponConstants.CompositePlus0Longbow, WeaponConstants.Longbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CompositePlus0Shortbow, WeaponConstants.Shortbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CompositePlus1Longbow, WeaponConstants.Longbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CompositePlus1Shortbow, WeaponConstants.Shortbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CompositePlus2Longbow, WeaponConstants.Longbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CompositePlus2Shortbow, WeaponConstants.Shortbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CompositePlus3Longbow, WeaponConstants.Longbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CompositePlus4Longbow, WeaponConstants.Longbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.CrossbowBolt, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.CursedBackbiterSpear, WeaponConstants.Shortspear)]
        [TestCase(WeaponConstants.CursedMinus2Sword, WeaponConstants.Longsword)]
        [TestCase(WeaponConstants.Dagger, WeaponConstants.Dagger)]
        [TestCase(WeaponConstants.DaggerOfVenom, WeaponConstants.Dagger)]
        [TestCase(WeaponConstants.Dart, WeaponConstants.Dart)]
        [TestCase(WeaponConstants.DireFlail, WeaponConstants.DireFlail)]
        [TestCase(WeaponConstants.DwarvenThrower, WeaponConstants.Warhammer)]
        [TestCase(WeaponConstants.DwarvenUrgrosh, WeaponConstants.DwarvenUrgrosh)]
        [TestCase(WeaponConstants.DwarvenWaraxe, WeaponConstants.DwarvenWaraxe)]
        [TestCase(WeaponConstants.Falchion, WeaponConstants.Falchion)]
        [TestCase(WeaponConstants.FlameTongue, WeaponConstants.Longsword)]
        [TestCase(WeaponConstants.FrostBrand, WeaponConstants.Greatsword)]
        [TestCase(WeaponConstants.Gauntlet, WeaponConstants.Gauntlet)]
        [TestCase(WeaponConstants.Glaive, WeaponConstants.Glaive)]
        [TestCase(WeaponConstants.GnomeHookedHammer, WeaponConstants.GnomeHookedHammer)]
        [TestCase(WeaponConstants.Greataxe, WeaponConstants.Greataxe)]
        [TestCase(WeaponConstants.Greatclub, WeaponConstants.Greatclub)]
        [TestCase(WeaponConstants.GreaterSlayingArrow, WeaponConstants.Arrow)]
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
        [TestCase(WeaponConstants.HolyAvenger, WeaponConstants.Longsword)]
        [TestCase(WeaponConstants.Javelin, WeaponConstants.Javelin)]
        [TestCase(WeaponConstants.JavelinOfLightning, WeaponConstants.Javelin)]
        [TestCase(WeaponConstants.Kama, WeaponConstants.Kama)]
        [TestCase(WeaponConstants.Kukri, WeaponConstants.Kukri)]
        [TestCase(WeaponConstants.Lance, WeaponConstants.Lance)]
        [TestCase(WeaponConstants.LifeDrinker, WeaponConstants.Greataxe)]
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
        [TestCase(WeaponConstants.NetOfSnaring, WeaponConstants.Net)]
        [TestCase(WeaponConstants.NineLivesStealer, WeaponConstants.Longsword)]
        [TestCase(WeaponConstants.Nunchaku, WeaponConstants.Nunchaku)]
        [TestCase(WeaponConstants.Oathbow, WeaponConstants.Longbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.OrcDoubleAxe, WeaponConstants.OrcDoubleAxe)]
        [TestCase(WeaponConstants.PunchingDagger, WeaponConstants.PunchingDagger)]
        [TestCase(WeaponConstants.Quarterstaff, WeaponConstants.Quarterstaff)]
        [TestCase(WeaponConstants.Ranseur, WeaponConstants.Ranseur)]
        [TestCase(WeaponConstants.Rapier, WeaponConstants.Rapier)]
        [TestCase(WeaponConstants.RapierOfPuncturing, WeaponConstants.Rapier)]
        [TestCase(WeaponConstants.HeavyRepeatingCrossbow, WeaponConstants.HeavyRepeatingCrossbow, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.LightRepeatingCrossbow, WeaponConstants.LightRepeatingCrossbow, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.Sap, WeaponConstants.Sap)]
        [TestCase(WeaponConstants.Scimitar, WeaponConstants.Scimitar)]
        [TestCase(WeaponConstants.ScreamingBolt, WeaponConstants.CrossbowBolt)]
        [TestCase(WeaponConstants.Scythe, WeaponConstants.Scythe)]
        [TestCase(WeaponConstants.Shatterspike, WeaponConstants.Longsword)]
        [TestCase(WeaponConstants.ShiftersSorrow, WeaponConstants.TwoBladedSword)]
        [TestCase(WeaponConstants.Shortbow, WeaponConstants.Shortbow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.Shortspear, WeaponConstants.Shortspear)]
        [TestCase(WeaponConstants.ShortSword, WeaponConstants.ShortSword)]
        [TestCase(WeaponConstants.Shuriken, WeaponConstants.Shuriken)]
        [TestCase(WeaponConstants.Siangham, WeaponConstants.Siangham)]
        [TestCase(WeaponConstants.Sickle, WeaponConstants.Sickle)]
        [TestCase(WeaponConstants.SlayingArrow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.SleepArrow, WeaponConstants.Arrow)]
        [TestCase(WeaponConstants.Sling, WeaponConstants.Sling, WeaponConstants.SlingBullet)]
        [TestCase(WeaponConstants.SlingBullet, WeaponConstants.SlingBullet)]
        [TestCase(WeaponConstants.SpikedChain, WeaponConstants.SpikedChain)]
        [TestCase(WeaponConstants.SpikedGauntlet, WeaponConstants.SpikedGauntlet)]
        [TestCase(WeaponConstants.SunBlade, WeaponConstants.ShortSword)]
        [TestCase(WeaponConstants.SwordOfLifeStealing, WeaponConstants.Longsword)]
        [TestCase(WeaponConstants.SwordOfSubtlety, WeaponConstants.ShortSword)]
        [TestCase(WeaponConstants.SwordOfThePlanes, WeaponConstants.Longsword)]
        [TestCase(WeaponConstants.SylvanScimitar, WeaponConstants.Scimitar)]
        [TestCase(WeaponConstants.ThrowingAxe, WeaponConstants.ThrowingAxe)]
        [TestCase(WeaponConstants.Trident, WeaponConstants.Trident)]
        [TestCase(WeaponConstants.TridentOfFishCommand, WeaponConstants.Trident)]
        [TestCase(WeaponConstants.TridentOfWarning, WeaponConstants.Trident)]
        [TestCase(WeaponConstants.TwoBladedSword, WeaponConstants.TwoBladedSword)]
        [TestCase(GroupConstants.TwoHanded,
            WeaponConstants.Longspear,
            WeaponConstants.Quarterstaff,
            WeaponConstants.Halfspear,
            WeaponConstants.HeavyCrossbow,
            WeaponConstants.LightCrossbow,
            WeaponConstants.Sling,
            WeaponConstants.Falchion,
            WeaponConstants.Glaive,
            WeaponConstants.Greataxe,
            WeaponConstants.Greatclub,
            WeaponConstants.Greatsword,
            WeaponConstants.Guisarme,
            WeaponConstants.Halberd,
            WeaponConstants.HeavyFlail,
            WeaponConstants.Ranseur,
            WeaponConstants.Scythe,
            WeaponConstants.Lance,
            WeaponConstants.Longbow,
            WeaponConstants.Shortbow,
            WeaponConstants.OrcDoubleAxe,
            WeaponConstants.SpikedChain,
            WeaponConstants.DireFlail,
            WeaponConstants.GnomeHookedHammer,
            WeaponConstants.DwarvenUrgrosh,
            WeaponConstants.HeavyRepeatingCrossbow,
            WeaponConstants.LightRepeatingCrossbow,
            WeaponConstants.Net,
            WeaponConstants.TwoBladedSword,
            WeaponConstants.HandCrossbow)]
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
                WeaponConstants.HeavyRepeatingCrossbow,
                WeaponConstants.LightRepeatingCrossbow,
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
