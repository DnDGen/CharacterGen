using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats
{
    [TestFixture]
    public class FeatFociTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.FeatFoci; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.SchoolsOfMagic,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.ExoticWeaponProficiency,
                FeatConstants.Foci.Weapons,
                FeatConstants.Foci.WeaponsWithUnarmed,
                FeatConstants.Foci.WeaponsWithUnarmedAndGrapple,
                FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay,
                FeatConstants.CombatStyle,
                GroupConstants.Skills,
                FeatConstants.MonkBonusFeat + "1",
                FeatConstants.MonkBonusFeat + "2",
                FeatConstants.MonkBonusFeat + "6",
                GroupConstants.FavoredEnemies
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.SchoolsOfMagic,
            CharacterClassConstants.Schools.Abjuration,
            CharacterClassConstants.Schools.Conjuration,
            CharacterClassConstants.Schools.Divination,
            CharacterClassConstants.Schools.Enchantment,
            CharacterClassConstants.Schools.Evocation,
            CharacterClassConstants.Schools.Illusion,
            CharacterClassConstants.Schools.Necromancy,
            CharacterClassConstants.Schools.Transmutation)]
        [TestCase(FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.Gauntlet,
            FeatConstants.Foci.UnarmedStrike,
            WeaponConstants.Dagger,
            WeaponConstants.PunchingDagger,
            WeaponConstants.SpikedGauntlet,
            WeaponConstants.LightMace,
            WeaponConstants.Sickle,
            WeaponConstants.Club,
            WeaponConstants.HeavyMace,
            WeaponConstants.Morningstar,
            WeaponConstants.Shortspear,
            WeaponConstants.Longspear,
            WeaponConstants.Quarterstaff,
            WeaponConstants.HeavyCrossbow,
            WeaponConstants.LightCrossbow,
            WeaponConstants.Dart,
            WeaponConstants.Javelin,
            WeaponConstants.Sling)]
        [TestCase(FeatConstants.MartialWeaponProficiency,
            WeaponConstants.ThrowingAxe,
            WeaponConstants.LightHammer,
            WeaponConstants.Handaxe,
            WeaponConstants.Kukri,
            WeaponConstants.LightPick,
            WeaponConstants.Sap,
            WeaponConstants.ShortSword,
            WeaponConstants.Battleaxe,
            WeaponConstants.LightFlail,
            WeaponConstants.Longsword,
            WeaponConstants.HeavyPick,
            WeaponConstants.Rapier,
            WeaponConstants.Scimitar,
            WeaponConstants.Trident,
            WeaponConstants.Warhammer,
            WeaponConstants.Falchion,
            WeaponConstants.Glaive,
            WeaponConstants.Greataxe,
            WeaponConstants.Greatclub,
            WeaponConstants.HeavyFlail,
            WeaponConstants.Greatsword,
            WeaponConstants.Guisarme,
            WeaponConstants.Halberd,
            WeaponConstants.Lance,
            WeaponConstants.Ranseur,
            WeaponConstants.Scythe,
            WeaponConstants.Longbow,
            WeaponConstants.Shortbow)]
        [TestCase(FeatConstants.ExoticWeaponProficiency,
            WeaponConstants.Kama,
            WeaponConstants.Nunchaku,
            WeaponConstants.Siangham,
            WeaponConstants.BastardSword,
            WeaponConstants.DwarvenWaraxe,
            WeaponConstants.Whip,
            WeaponConstants.OrcDoubleAxe,
            WeaponConstants.SpikedChain,
            WeaponConstants.DireFlail,
            WeaponConstants.GnomeHookedHammer,
            WeaponConstants.TwoBladedSword,
            WeaponConstants.DwarvenUrgrosh,
            WeaponConstants.HandCrossbow,
            WeaponConstants.HeavyRepeatingCrossbow,
            WeaponConstants.LightRepeatingCrossbow,
            WeaponConstants.Shuriken,
            WeaponConstants.Net)]
        [TestCase(FeatConstants.CombatStyle,
            FeatConstants.TwoWeaponFighting,
            FeatConstants.Foci.Archery)]
        [TestCase(FeatConstants.MonkBonusFeat + "1",
            FeatConstants.StunningFist,
            FeatConstants.ImprovedGrapple)]
        [TestCase(FeatConstants.MonkBonusFeat + "2",
            FeatConstants.CombatReflexes,
            FeatConstants.DeflectArrows)]
        [TestCase(FeatConstants.MonkBonusFeat + "6",
            FeatConstants.ImprovedDisarm,
            FeatConstants.ImprovedTrip)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void FavoredEnemyFoci()
        {
            var foci = new[]
            {
                "Aberration",
                "Animal",
                "Construct",
                "Dragon",
                "Elemental",
                "Fey",
                "Giant",
                "Humanoid (aquatic)",
                "Humanoid (dwarf)",
                "Humanoid (elf)",
                "Humanoid (goblinoid)",
                "Humanoid (gnoll)",
                "Humanoid (gnome)",
                "Humanoid (halfling)",
                "Humanoid (human)",
                "Humanoid (orc)",
                "Humanoid (reptilian)",
                "Magical beast",
                "Monstrous humanoid",
                "Ooze",
                "Outsider (air)",
                "Outsider (chaotic)",
                "Outsider (earth)",
                "Outsider (evil)",
                "Outsider (fire)",
                "Outsider (good)",
                "Outsider (lawful)",
                "Outsider (native)",
                "Outsider (water)",
                "Plant",
                "Undead",
                "Vermin"
            };

            base.DistinctCollection(GroupConstants.FavoredEnemies, foci);
        }

        [Test]
        public void WeaponFoci()
        {
            var foci = new[]
            {
                WeaponConstants.BastardSword,
                WeaponConstants.Battleaxe,
                WeaponConstants.Club,
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
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip
            };

            base.DistinctCollection(FeatConstants.Foci.Weapons, foci);
        }

        [Test]
        public void WeaponAndUnarmedFoci()
        {
            var foci = new[]
            {
                WeaponConstants.BastardSword,
                WeaponConstants.Battleaxe,
                WeaponConstants.Club,
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
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip,
                FeatConstants.Foci.UnarmedStrike
            };

            base.DistinctCollection(FeatConstants.Foci.WeaponsWithUnarmed, foci);
        }

        [Test]
        public void WeaponAndUnarmedAndGrappleFoci()
        {
            var foci = new[]
            {
                WeaponConstants.BastardSword,
                WeaponConstants.Battleaxe,
                WeaponConstants.Club,
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
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip,
                FeatConstants.Foci.UnarmedStrike,
                FeatConstants.Foci.Grapple
            };

            base.DistinctCollection(FeatConstants.Foci.WeaponsWithUnarmedAndGrapple, foci);
        }

        [Test]
        public void WeaponGrappleAndRayFoci()
        {
            var foci = new[]
            {
                WeaponConstants.BastardSword,
                WeaponConstants.Battleaxe,
                WeaponConstants.Club,
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
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip,
                FeatConstants.Foci.UnarmedStrike,
                FeatConstants.Foci.Grapple,
                FeatConstants.Foci.Ray
            };

            base.DistinctCollection(FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay, foci);
        }

        [Test]
        public void SkillsFoci()
        {
            var foci = new[]
            {
                SkillConstants.Appraise,
                SkillConstants.Balance,
                SkillConstants.Bluff,
                SkillConstants.Climb,
                SkillConstants.Concentration,
                SkillConstants.DecipherScript,
                SkillConstants.Diplomacy,
                SkillConstants.DisableDevice,
                SkillConstants.Disguise,
                SkillConstants.EscapeArtist,
                SkillConstants.Forgery,
                SkillConstants.GatherInformation,
                SkillConstants.HandleAnimal,
                SkillConstants.Heal,
                SkillConstants.Hide,
                SkillConstants.Intimidate,
                SkillConstants.Jump,
                SkillConstants.KnowledgeArcana,
                SkillConstants.KnowledgeArchitectureAndEngineering,
                SkillConstants.KnowledgeDungeoneering,
                SkillConstants.KnowledgeGeography,
                SkillConstants.KnowledgeHistory,
                SkillConstants.KnowledgeLocal,
                SkillConstants.KnowledgeNature,
                SkillConstants.KnowledgeNobilityAndRoyalty,
                SkillConstants.KnowledgeReligion,
                SkillConstants.KnowledgeThePlanes,
                SkillConstants.Listen,
                SkillConstants.MoveSilently,
                SkillConstants.OpenLock,
                SkillConstants.Perform,
                SkillConstants.Ride,
                SkillConstants.Search,
                SkillConstants.SenseMotive,
                SkillConstants.SleightOfHand,
                SkillConstants.Spellcraft,
                SkillConstants.Spot,
                SkillConstants.Survival,
                SkillConstants.Swim,
                SkillConstants.Tumble,
                SkillConstants.UseMagicDevice,
                SkillConstants.UseRope
            };

            base.DistinctCollection(GroupConstants.Skills, foci);
        }
    }
}
