using System;
using EquipmentGen.Common.Items;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats
{
    [TestFixture]
    public class FeatFociTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.FeatFoci; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                GroupConstants.SchoolsOfMagic,
                FeatConstants.SimpleWeaponProficiencyId,
                FeatConstants.MartialWeaponProficiencyId,
                FeatConstants.ExoticWeaponProficiencyId,
                GroupConstants.Weapons,
                GroupConstants.WeaponsWithUnarmedAndGrapple,
                GroupConstants.WeaponsWithUnarmedAndGrappleAndRay,
                FeatConstants.CombatStyleId,
                GroupConstants.Skills
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
        [TestCase(FeatConstants.SimpleWeaponProficiencyId,
            WeaponConstants.Gauntlet,
            WeaponProficiencyConstants.UnarmedStrike,
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
        [TestCase(FeatConstants.MartialWeaponProficiencyId,
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
        [TestCase(FeatConstants.ExoticWeaponProficiencyId,
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
            WeaponConstants.RepeatingCrossbow,
            WeaponConstants.Shuriken,
            WeaponConstants.Net)]
        [TestCase(FeatConstants.CombatStyleId,
            FeatConstants.TwoWeaponFightingId,
            FeatConstants.Foci.Archery)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
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
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip
            };

            base.DistinctCollection(GroupConstants.Weapons, foci);
        }

        [Test]
        public void WeaponAnGrappleFoci()
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
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip,
                WeaponProficiencyConstants.UnarmedStrike,
                WeaponProficiencyConstants.Grapple
            };

            base.DistinctCollection(GroupConstants.WeaponsWithUnarmedAndGrapple, foci);
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
                WeaponConstants.SpikedChain,
                WeaponConstants.SpikedGauntlet,
                WeaponConstants.ThrowingAxe,
                WeaponConstants.Trident,
                WeaponConstants.TwoBladedSword,
                WeaponConstants.Warhammer,
                WeaponConstants.Whip,
                WeaponProficiencyConstants.UnarmedStrike,
                WeaponProficiencyConstants.Grapple,
                WeaponProficiencyConstants.Ray
            };

            base.DistinctCollection(GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, foci);
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
