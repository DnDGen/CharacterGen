using System;
using System.Linq;
using EquipmentGen.Common.Items;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats
{
    [TestFixture]
    public class FeatGroupsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.FeatGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                String.Empty,
                GroupConstants.Additional,
                GroupConstants.HasClassRequirements,
                GroupConstants.HasSkillRequirements,
                GroupConstants.HasStatRequirements,
                GroupConstants.TakenMultipleTimes,
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard,
                CharacterClassConstants.Domains.Air,
                CharacterClassConstants.Domains.Animal,
                CharacterClassConstants.Domains.Chaos,
                CharacterClassConstants.Domains.Death,
                CharacterClassConstants.Domains.Destruction,
                CharacterClassConstants.Domains.Earth,
                CharacterClassConstants.Domains.Evil,
                CharacterClassConstants.Domains.Fire,
                CharacterClassConstants.Domains.Good,
                CharacterClassConstants.Domains.Healing,
                CharacterClassConstants.Domains.Knowledge,
                CharacterClassConstants.Domains.Law,
                CharacterClassConstants.Domains.Luck,
                CharacterClassConstants.Domains.Magic,
                CharacterClassConstants.Domains.Plant,
                CharacterClassConstants.Domains.Protection,
                CharacterClassConstants.Domains.Strength,
                CharacterClassConstants.Domains.Sun,
                CharacterClassConstants.Domains.Travel,
                CharacterClassConstants.Domains.Trickery,
                CharacterClassConstants.Domains.War,
                CharacterClassConstants.Domains.Water,
                CharacterClassConstants.Schools.Conjuration,
                CharacterClassConstants.Schools.Evocation,
                CharacterClassConstants.Schools.Enchantment,
                CharacterClassConstants.Schools.Abjuration,
                CharacterClassConstants.Schools.Divination,
                CharacterClassConstants.Schools.Illusion,
                CharacterClassConstants.Schools.Necromancy,
                CharacterClassConstants.Schools.Transmutation,
                RaceConstants.BaseRaces.DeepHalfling,
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.Goblin,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.BaseRaces.HalfElf,
                RaceConstants.BaseRaces.HalfOrc,
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.Hobgoblin,
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.LightfootHalfling,
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.Metaraces.None,
                RaceConstants.BaseRaces.Orc,
                RaceConstants.BaseRaces.RockGnome,
                RaceConstants.BaseRaces.TallfellowHalfling,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Werewolf,
                RaceConstants.BaseRaces.WoodElf
            };

            AssertCollectionNames(names);
        }

        [TestCase("")]
        [TestCase(GroupConstants.HasClassRequirements,
            FeatConstants.CripplingStrike,
            FeatConstants.DefensiveRoll,
            FeatConstants.GreaterWeaponFocus,
            FeatConstants.GreaterWeaponSpecialization,
            FeatConstants.ImprovedEvasion,
            FeatConstants.Opportunist,
            FeatConstants.SkillMastery,
            FeatConstants.SlipperyMind,
            FeatConstants.ImprovedFamiliar,
            FeatConstants.Leadership,
            FeatConstants.SpellMastery,
            FeatConstants.WeaponSpecialization)]
        [TestCase(GroupConstants.HasSkillRequirements,
            FeatConstants.MountedArchery,
            FeatConstants.MountedCombat,
            FeatConstants.RideByAttack,
            FeatConstants.SpiritedCharge,
            FeatConstants.Trample)]
        [TestCase(GroupConstants.HasStatRequirements,
            FeatConstants.PowerAttack,
            FeatConstants.CombatExpertise,
            FeatConstants.DeflectArrows,
            FeatConstants.Dodge,
            FeatConstants.GreaterTwoWeaponFighting,
            FeatConstants.ImprovedGrapple,
            FeatConstants.ImprovedPreciseShot,
            FeatConstants.ImprovedTwoWeaponFighting,
            FeatConstants.Manyshot,
            FeatConstants.Mobility,
            FeatConstants.NaturalSpell,
            FeatConstants.RapidShot,
            FeatConstants.SnatchArrows,
            FeatConstants.SpringAttack,
            FeatConstants.StunningFist,
            FeatConstants.ShotOnTheRun,
            FeatConstants.TwoWeaponDefense,
            FeatConstants.TwoWeaponFighting,
            FeatConstants.WhirlwindAttack)]
        [TestCase(GroupConstants.TakenMultipleTimes,
            FeatConstants.SpellMastery,
            FeatConstants.Toughness,
            FeatConstants.SkillMastery,
            FeatConstants.SkillBonus,
            FeatConstants.AttackBonus,
            FeatConstants.DodgeBonus,
            FeatConstants.SaveBonus,
            FeatConstants.ExtraTurning)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [TestCase(CharacterClassConstants.Cleric,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.LightArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            FeatConstants.HeavyArmorProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Fighter,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.LightArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            FeatConstants.HeavyArmorProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.TowerShieldProficiency)]
        [TestCase(CharacterClassConstants.Sorcerer,
            FeatConstants.SimpleWeaponProficiency)]
        [TestCase(CharacterClassConstants.Wizard,
            FeatConstants.ScribeScroll,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.HeavyCrossbow,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff)]
        [TestCase(CharacterClassConstants.Domains.Air,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Domains.Animal,
            FeatConstants.SpellLikeAbility)]
        [TestCase(CharacterClassConstants.Domains.Chaos,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Death,
            FeatConstants.DeathTouch)]
        [TestCase(CharacterClassConstants.Domains.Destruction,
            FeatConstants.Smite)]
        [TestCase(CharacterClassConstants.Domains.Earth,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Domains.Evil,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Fire,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Domains.Good,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Healing,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Knowledge,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Law,
            FeatConstants.CastSpellBonus)]
        [TestCase(CharacterClassConstants.Domains.Luck,
            FeatConstants.GoodFortune)]
        [TestCase(CharacterClassConstants.Domains.Magic,
            FeatConstants.UseMagicDeviceAsWizard)]
        [TestCase(CharacterClassConstants.Domains.Plant,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Domains.Protection,
            FeatConstants.SpellLikeAbility)]
        [TestCase(CharacterClassConstants.Domains.Strength,
            FeatConstants.SupernaturalStrength)]
        [TestCase(CharacterClassConstants.Domains.Sun,
            FeatConstants.GreaterTurning)]
        [TestCase(CharacterClassConstants.Domains.Travel,
            FeatConstants.SpellLikeAbility)]
        [TestCase(CharacterClassConstants.Domains.Trickery)]
        [TestCase(CharacterClassConstants.Domains.War,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.WeaponFocus)]
        [TestCase(CharacterClassConstants.Domains.Water,
            FeatConstants.Turn)]
        [TestCase(CharacterClassConstants.Schools.Abjuration,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Conjuration,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Divination,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Enchantment,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Evocation,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Illusion,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Necromancy,
            FeatConstants.SkillBonus)]
        [TestCase(CharacterClassConstants.Schools.Transmutation,
            FeatConstants.SkillBonus)]
        public void ClassFeatGroup(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [TestCase(RaceConstants.BaseRaces.DeepHalfling,
            FeatConstants.SaveBonus + "All",
            FeatConstants.SaveBonus + "Fear",
            FeatConstants.AttackBonus + "ThrowOrSling",
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Appraise,
            FeatConstants.Darkvision,
            FeatConstants.Stonecunning)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger,
            FeatConstants.SkillBonus + SkillConstants.Bluff,
            FeatConstants.SkillBonus + SkillConstants.Disguise,
            FeatConstants.Darkvision,
            FeatConstants.NaturalArmor,
            FeatConstants.SpellLikeAbility,
            FeatConstants.ChangeShape,
            FeatConstants.ImmuneToEffect + "Sleep",
            FeatConstants.ImmuneToEffect + "Charm")]
        [TestCase(RaceConstants.BaseRaces.Drow,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.SaveBonus + "Will",
            FeatConstants.Darkvision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.HandCrossbow,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
            FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SpellResistance,
            FeatConstants.SpellLikeAbility + "Dancing lights",
            FeatConstants.SpellLikeAbility + "Darkness",
            FeatConstants.SpellLikeAbility + "Faerie fire",
            FeatConstants.LightBlindness)]
        [TestCase(RaceConstants.BaseRaces.Gnoll,
            FeatConstants.Darkvision,
            FeatConstants.NaturalArmor)]
        [TestCase(RaceConstants.BaseRaces.Goblin,
            FeatConstants.Darkvision,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus + SkillConstants.Ride)]
        [TestCase(RaceConstants.BaseRaces.HalfElf,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.LowLightVision,
            FeatConstants.ElvenBlood,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Diplomacy,
            FeatConstants.SkillBonus + SkillConstants.GatherInformation)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc,
            FeatConstants.Darkvision,
            FeatConstants.OrcBlood)]
        [TestCase(RaceConstants.BaseRaces.HighElf,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.LowLightVision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf,
            FeatConstants.Darkvision,
            FeatConstants.Stonecunning,
            FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenWaraxe,
            FeatConstants.Stability,
            FeatConstants.SaveBonus + "Poison",
            FeatConstants.SaveBonus + "Spell",
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus + SkillConstants.Appraise)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin,
            FeatConstants.Darkvision,
            FeatConstants.SkillBonus)]
        [TestCase(RaceConstants.BaseRaces.Human)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling,
            FeatConstants.SaveBonus + "All",
            FeatConstants.SaveBonus + "Fear",
            FeatConstants.AttackBonus + "ThrowOrSling",
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Climb,
            FeatConstants.SkillBonus + SkillConstants.Jump,
            FeatConstants.SkillBonus + SkillConstants.MoveSilently)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.ShieldProficiency,
            FeatConstants.NaturalArmor,
            FeatConstants.HoldBreath,
            FeatConstants.NaturalWeapon + "Claw",
            FeatConstants.NaturalWeapon + "Bite")]
        [TestCase(RaceConstants.Metaraces.None)]
        [TestCase(RaceConstants.BaseRaces.Orc,
            FeatConstants.Darkvision,
            FeatConstants.LightSensitivity)]
        [TestCase(RaceConstants.BaseRaces.RockGnome,
            FeatConstants.LowLightVision,
            FeatConstants.WeaponFamiliarity,
            FeatConstants.SaveBonus,
            FeatConstants.ImprovedSpell,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus + RaceConstants.BaseRaces.Kobold,
            FeatConstants.DodgeBonus,
            FeatConstants.SkillBonus,
            FeatConstants.SpellLikeAbility + "Speak with animals",
            FeatConstants.SpellLikeAbility + "Dancing lights",
            FeatConstants.SpellLikeAbility + "Ghost sound",
            FeatConstants.SpellLikeAbility + "Prestidigitation")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling,
            FeatConstants.SaveBonus + "All",
            FeatConstants.SaveBonus + "Fear",
            FeatConstants.AttackBonus + "ThrowOrSling",
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.PassiveSecretDoorSearch)]
        [TestCase(RaceConstants.Metaraces.Wereboar,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.NaturalArmor)]
        [TestCase(RaceConstants.Metaraces.Werewolf,
            FeatConstants.IronWill,
            FeatConstants.Track,
            FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.NaturalArmor)]
        [TestCase(RaceConstants.BaseRaces.WoodElf,
            FeatConstants.ImmuneToEffect,
            FeatConstants.SaveBonus,
            FeatConstants.LowLightVision,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus + SkillConstants.Listen)]
        public void RaceFeatGroup(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void RangerFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.LightArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.FavoredEnemy + "1",
                FeatConstants.FavoredEnemy + "2",
                FeatConstants.FavoredEnemy + "3",
                FeatConstants.FavoredEnemy + "4",
                FeatConstants.FavoredEnemy + "5",
                FeatConstants.Track,
                FeatConstants.WildEmpathy,
                FeatConstants.CombatStyle,
                FeatConstants.Endurance,
                FeatConstants.ImprovedCombatStyle,
                FeatConstants.WoodlandStride,
                FeatConstants.SwiftTracker,
                FeatConstants.Evasion,
                FeatConstants.CombatStyleMastery,
                FeatConstants.Camouflage,
                FeatConstants.HideInPlainSight,
                FeatConstants.RapidShot + "Ranger",
                FeatConstants.TwoWeaponFighting + "Ranger",
                FeatConstants.Manyshot + "Ranger",
                FeatConstants.ImprovedTwoWeaponFighting + "Ranger",
                FeatConstants.ImprovedPreciseShot + "Ranger",
                FeatConstants.GreaterTwoWeaponFighting + "Ranger"
            };

            base.DistinctCollection(CharacterClassConstants.Ranger, featIds);
        }

        [Test]
        public void MonkFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.HeavyCrossbow,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Sling,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Handaxe,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Javelin,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Kama,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Nunchaku,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Shuriken,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Siangham,
                FeatConstants.ArmorBonus + StatConstants.Wisdom,
                FeatConstants.ArmorBonus + "1",
                FeatConstants.ArmorBonus + "2",
                FeatConstants.ArmorBonus + "3",
                FeatConstants.ArmorBonus + "4",
                FeatConstants.FlurryOfBlows,
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "1d4",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "1d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "1d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "1d10",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "2d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Small + "2d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "1d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "1d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "1d10",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "2d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "2d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Medium + "2d10",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "1d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "2d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "2d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "3d6",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "3d8",
                FeatConstants.MonkUnarmedStrike + RaceConstants.Sizes.Large + "4d8",
                FeatConstants.ImprovedUnarmedStrike,
                FeatConstants.MonkBonusFeat + "1",
                FeatConstants.MonkBonusFeat + "2",
                FeatConstants.MonkBonusFeat + "6",
                FeatConstants.ImprovedGrapple,
                FeatConstants.StunningFist,
                FeatConstants.CombatReflexes,
                FeatConstants.DeflectArrows,
                FeatConstants.ImprovedDisarm,
                FeatConstants.ImprovedTrip,
                FeatConstants.Evasion,
                FeatConstants.FastMovement + "10",
                FeatConstants.FastMovement + "20",
                FeatConstants.FastMovement + "30",
                FeatConstants.FastMovement + "40",
                FeatConstants.FastMovement + "50",
                FeatConstants.FastMovement + "60",
                FeatConstants.StillMind,
                FeatConstants.KiStrike,
                FeatConstants.SlowFall,
                FeatConstants.PurityOfBody,
                FeatConstants.WholenessOfBody,
                FeatConstants.ImprovedEvasion,
                FeatConstants.DiamondBody,
                FeatConstants.AbundantStep,
                FeatConstants.DiamondSoul,
                FeatConstants.QuiveringPalm,
                FeatConstants.TimelessBody,
                FeatConstants.TongueOfSunAndMoon,
                FeatConstants.EmptyBody,
                FeatConstants.PerfectSelf
            };

            base.DistinctCollection(CharacterClassConstants.Monk, featIds);
        }

        [Test]
        public void HalfCelestialFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SpellLikeAbility + "Daylight",
                FeatConstants.SmiteEvil,
                FeatConstants.SpellLikeAbility + "Protection from evil",
                FeatConstants.SpellLikeAbility + "Bless",
                FeatConstants.SpellLikeAbility + "Aid",
                FeatConstants.SpellLikeAbility + "Detect evil",
                FeatConstants.SpellLikeAbility + "Cure serious wounds",
                FeatConstants.SpellLikeAbility + "Neutralize poison",
                FeatConstants.SpellLikeAbility + "Holy smite",
                FeatConstants.SpellLikeAbility + "Remove disease",
                FeatConstants.SpellLikeAbility + "Dispel evil",
                FeatConstants.SpellLikeAbility + "Holy word",
                FeatConstants.SpellLikeAbility + "Holy aura",
                FeatConstants.SpellLikeAbility + "Hallow",
                FeatConstants.SpellLikeAbility + "Mass charm monster",
                FeatConstants.SpellLikeAbility + "Summon monster IX (Celestials only)",
                FeatConstants.SpellLikeAbility + "Resurrection",
                FeatConstants.NaturalArmor,
                FeatConstants.Darkvision,
                FeatConstants.ImmuneToEffect,
                FeatConstants.ResistanceToAcid,
                FeatConstants.ResistanceToCold,
                FeatConstants.ResistanceToElectricity,
                FeatConstants.DamageReduction + "10-",
                FeatConstants.DamageReduction + "11+",
                FeatConstants.SpellResistance,
                FeatConstants.SaveBonus
            };

            base.DistinctCollection(RaceConstants.Metaraces.HalfCelestial, featIds);
        }

        [Test]
        public void BarbarianFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.FastMovement,
                FeatConstants.Illiteracy,
                FeatConstants.Rage + "1",
                FeatConstants.Rage + "2",
                FeatConstants.Rage + "3",
                FeatConstants.Rage + "4",
                FeatConstants.Rage + "5",
                FeatConstants.Rage + "6",
                FeatConstants.UncannyDodge,
                FeatConstants.TrapSense + "1",
                FeatConstants.TrapSense + "2",
                FeatConstants.TrapSense + "3",
                FeatConstants.TrapSense + "4",
                FeatConstants.TrapSense + "5",
                FeatConstants.TrapSense + "6",
                FeatConstants.ImprovedUncannyDodge,
                FeatConstants.DamageReduction + "1",
                FeatConstants.DamageReduction + "2",
                FeatConstants.DamageReduction + "3",
                FeatConstants.DamageReduction + "4",
                FeatConstants.DamageReduction + "5",
                FeatConstants.GreaterRage,
                FeatConstants.IndomitableWill,
                FeatConstants.TirelessRage,
                FeatConstants.MightyRage
            };

            base.DistinctCollection(CharacterClassConstants.Barbarian, featIds);
        }

        [Test]
        public void BardFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Sap,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.Whip,
                FeatConstants.LightArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.BardicMusic + "1",
                FeatConstants.BardicMusic + "2",
                FeatConstants.BardicMusic + "3",
                FeatConstants.BardicMusic + "4",
                FeatConstants.BardicMusic + "5",
                FeatConstants.BardicMusic + "6",
                FeatConstants.BardicMusic + "7",
                FeatConstants.BardicMusic + "8",
                FeatConstants.BardicMusic + "9",
                FeatConstants.BardicMusic + "10",
                FeatConstants.BardicMusic + "11",
                FeatConstants.BardicMusic + "12",
                FeatConstants.BardicMusic + "13",
                FeatConstants.BardicMusic + "14",
                FeatConstants.BardicMusic + "15",
                FeatConstants.BardicMusic + "16",
                FeatConstants.BardicMusic + "17",
                FeatConstants.BardicMusic + "18",
                FeatConstants.BardicMusic + "19",
                FeatConstants.BardicMusic + "20",
                FeatConstants.BardicKnowledge,
                FeatConstants.Countersong,
                FeatConstants.Fascinate,
                FeatConstants.InspireCourage + "1",
                FeatConstants.InspireCourage + "2",
                FeatConstants.InspireCourage + "3",
                FeatConstants.InspireCourage + "4",
                FeatConstants.InspireCompetence,
                FeatConstants.Suggestion,
                FeatConstants.InspireGreatness,
                FeatConstants.SongOfFreedom,
                FeatConstants.InspireHeroics,
                FeatConstants.MassSuggestion
            };

            base.DistinctCollection(CharacterClassConstants.Bard, featIds);
        }

        [Test]
        public void RogueFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SneakAttack + "1",
                FeatConstants.SneakAttack + "2",
                FeatConstants.SneakAttack + "3",
                FeatConstants.SneakAttack + "4",
                FeatConstants.SneakAttack + "5",
                FeatConstants.SneakAttack + "6",
                FeatConstants.SneakAttack + "7",
                FeatConstants.SneakAttack + "8",
                FeatConstants.SneakAttack + "9",
                FeatConstants.SneakAttack + "10",
                FeatConstants.Trapfinding,
                FeatConstants.Evasion,
                FeatConstants.UncannyDodge,
                FeatConstants.TrapSense + "1",
                FeatConstants.TrapSense + "2",
                FeatConstants.TrapSense + "3",
                FeatConstants.TrapSense + "4",
                FeatConstants.TrapSense + "5",
                FeatConstants.TrapSense + "6",
                FeatConstants.ImprovedUncannyDodge,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.HandCrossbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Sap,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
                FeatConstants.LightArmorProficiency
            };

            base.DistinctCollection(CharacterClassConstants.Rogue, featIds);
        }

        [Test]
        public void AdditionalFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.Acrobatic,
                FeatConstants.Agile,
                FeatConstants.Alertness,
                FeatConstants.AnimalAffinity,
                FeatConstants.Athletic,
                FeatConstants.AugmentSummoning,
                FeatConstants.BlindFight,
                FeatConstants.Cleave,
                FeatConstants.CombatCasting,
                FeatConstants.CombatExpertise,
                FeatConstants.CombatReflexes,
                FeatConstants.CripplingStrike,
                FeatConstants.Deceitful,
                FeatConstants.DefensiveRoll,
                FeatConstants.DeflectArrows,
                FeatConstants.DeftHands,
                FeatConstants.Diehard,
                FeatConstants.Diligent,
                FeatConstants.Dodge,
                FeatConstants.EmpowerSpell,
                FeatConstants.Endurance,
                FeatConstants.EnlargeSpell,
                FeatConstants.EschewMaterials,
                FeatConstants.ExoticWeaponProficiency,
                FeatConstants.ExtendSpell,
                FeatConstants.ExtraTurning,
                FeatConstants.FarShot,
                FeatConstants.GreatCleave,
                FeatConstants.GreaterSpellFocus,
                FeatConstants.GreaterSpellPenetration,
                FeatConstants.GreaterTwoWeaponFighting,
                FeatConstants.GreaterWeaponFocus,
                FeatConstants.GreaterWeaponSpecialization,
                FeatConstants.GreatFortitude,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.HeightenSpell,
                FeatConstants.ImprovedBullRush,
                FeatConstants.ImprovedCounterspell,
                FeatConstants.ImprovedCritical,
                FeatConstants.ImprovedDisarm,
                FeatConstants.ImprovedEvasion,
                FeatConstants.ImprovedFamiliar,
                FeatConstants.ImprovedFeint,
                FeatConstants.ImprovedGrapple,
                FeatConstants.ImprovedInitiative,
                FeatConstants.ImprovedOverrun,
                FeatConstants.ImprovedPreciseShot,
                FeatConstants.ImprovedShieldBash,
                FeatConstants.ImprovedSunder,
                FeatConstants.ImprovedTrip,
                FeatConstants.ImprovedTurning,
                FeatConstants.ImprovedTwoWeaponFighting,
                FeatConstants.ImprovedUnarmedStrike,
                FeatConstants.Investigator,
                FeatConstants.IronWill,
                FeatConstants.Leadership,
                FeatConstants.LightArmorProficiency,
                FeatConstants.LightningReflexes,
                FeatConstants.MagicalAptitude,
                FeatConstants.Manyshot,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.MaximizeSpell,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.Mobility,
                FeatConstants.MountedArchery,
                FeatConstants.MountedCombat,
                FeatConstants.NaturalSpell,
                FeatConstants.Negotiator,
                FeatConstants.NimbleFingers,
                FeatConstants.Opportunist,
                FeatConstants.Persuasive,
                FeatConstants.PointBlankShot,
                FeatConstants.PowerAttack,
                FeatConstants.PreciseShot,
                FeatConstants.QuickDraw,
                FeatConstants.QuickenSpell,
                FeatConstants.RapidReload,
                FeatConstants.RapidShot,
                FeatConstants.RideByAttack,
                FeatConstants.Run,
                FeatConstants.SelfSufficient,
                FeatConstants.ShieldProficiency,
                FeatConstants.ShotOnTheRun,
                FeatConstants.SilentSpell,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.SkillFocus,
                FeatConstants.SkillMastery,
                FeatConstants.SlipperyMind,
                FeatConstants.SnatchArrows,
                FeatConstants.SpellFocus,
                FeatConstants.SpellMastery,
                FeatConstants.SpellPenetration,
                FeatConstants.SpiritedCharge,
                FeatConstants.SpringAttack,
                FeatConstants.Stability,
                FeatConstants.Stealthy,
                FeatConstants.StillSpell,
                FeatConstants.StunningFist,
                FeatConstants.Toughness,
                FeatConstants.TowerShieldProficiency,
                FeatConstants.Track,
                FeatConstants.Trample,
                FeatConstants.TwoWeaponDefense,
                FeatConstants.TwoWeaponFighting,
                FeatConstants.WeaponFocus,
                FeatConstants.WeaponFinesse,
                FeatConstants.WeaponSpecialization,
                FeatConstants.WhirlwindAttack,
                FeatConstants.WidenSpell
            };

            base.DistinctCollection(GroupConstants.Additional, featIds);
        }

        [TestCase(CharacterClassConstants.Ranger,
            FeatConstants.ImprovedCombatStyle,
            FeatConstants.CombatStyle)]
        [TestCase(CharacterClassConstants.Ranger,
            FeatConstants.CombatStyleMastery,
            FeatConstants.ImprovedCombatStyle)]
        public void FeatGroupDependency(String name, String feat, String dependencyFeat)
        {
            var collection = table[name].ToList();
            var featIndex = collection.IndexOf(feat);
            var dependencyIndex = collection.IndexOf(dependencyFeat);

            Assert.That(featIndex, Is.Positive);
            Assert.That(dependencyIndex, Is.Positive);
            Assert.That(featIndex, Is.GreaterThan(dependencyIndex));
        }
    }
}