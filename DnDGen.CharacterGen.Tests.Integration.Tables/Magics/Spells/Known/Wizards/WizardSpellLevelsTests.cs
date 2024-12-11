﻿using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class WizardSpellLevelsTests : CollectionTests
    {
        protected override string tableName => string.Format(TableNameConstants.Formattable.Collection.CLASSSpellLevels, CharacterClassConstants.Wizard);

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 10).Select(n => n.ToString());
            AssertCollectionNames(names);
        }

        [TestCase("0",
            SpellConstants.AcidSplash,
            SpellConstants.Resistance,
            SpellConstants.DetectPoison,
            SpellConstants.DetectMagic,
            SpellConstants.ReadMagic,
            SpellConstants.Daze,
            SpellConstants.DancingLights,
            SpellConstants.Flare,
            SpellConstants.Light,
            SpellConstants.RayOfFrost,
            SpellConstants.GhostSound,
            SpellConstants.DisruptUndead,
            SpellConstants.TouchOfFatigue,
            SpellConstants.MageHand,
            SpellConstants.Mending,
            SpellConstants.Message,
            SpellConstants.OpenClose,
            SpellConstants.ArcaneMark,
            SpellConstants.Prestidigitation)]
        [TestCase("1",
            SpellConstants.Alarm,
            SpellConstants.EndureElements,
            SpellConstants.HoldPortal,
            SpellConstants.ProtectionFromChaos,
            SpellConstants.ProtectionFromEvil,
            SpellConstants.ProtectionFromGood,
            SpellConstants.ProtectionFromLaw,
            SpellConstants.Shield,
            SpellConstants.Grease,
            SpellConstants.MageArmor,
            SpellConstants.Mount,
            SpellConstants.ObscuringMist,
            SpellConstants.SummonMonsterI,
            SpellConstants.UnseenServant,
            SpellConstants.ComprehendLanguages,
            SpellConstants.DetectSecretDoors,
            SpellConstants.DetectUndead,
            SpellConstants.Identify,
            SpellConstants.TrueStrike,
            SpellConstants.CharmPerson,
            SpellConstants.Hypnotism,
            SpellConstants.Sleep,
            SpellConstants.BurningHands,
            SpellConstants.TensersFloatingDisk,
            SpellConstants.MagicMissile,
            SpellConstants.ShockingGrasp,
            SpellConstants.ColorSpray,
            SpellConstants.DisguiseSelf,
            SpellConstants.NystulsMagicAura,
            SpellConstants.SilentImage,
            SpellConstants.Ventriloquism,
            SpellConstants.CauseFear,
            SpellConstants.ChillTouch,
            SpellConstants.RayOfEnfeeblement,
            SpellConstants.AnimateRope,
            SpellConstants.EnlargePerson,
            SpellConstants.Erase,
            SpellConstants.ExpeditiousRetreat,
            SpellConstants.FeatherFall,
            SpellConstants.Jump,
            SpellConstants.MagicWeapon,
            SpellConstants.ReducePerson)]
        [TestCase("2",
            SpellConstants.ArcaneLock,
            SpellConstants.ObscureObject,
            SpellConstants.ProtectionFromArrows,
            SpellConstants.ResistEnergy,
            SpellConstants.MelfsAcidArrow,
            SpellConstants.FogCloud,
            SpellConstants.Glitterdust,
            SpellConstants.SummonMonsterII,
            SpellConstants.SummonSwarm,
            SpellConstants.Web,
            SpellConstants.DetectThoughts,
            SpellConstants.LocateObject,
            SpellConstants.SeeInvisibility,
            SpellConstants.DazeMonster,
            SpellConstants.TashasHideousLaughter,
            SpellConstants.TouchOfIdiocy,
            SpellConstants.ContinualFlame,
            SpellConstants.Darkness,
            SpellConstants.FlamingSphere,
            SpellConstants.GustOfWind,
            SpellConstants.ScorchingRay,
            SpellConstants.Shatter,
            SpellConstants.Blur,
            SpellConstants.HypnoticPattern,
            SpellConstants.Invisibility,
            SpellConstants.MagicMouth,
            SpellConstants.MinorImage,
            SpellConstants.MirrorImage,
            SpellConstants.Misdirection,
            SpellConstants.LeomundsTrap,
            SpellConstants.BlindnessDeafness,
            SpellConstants.CommandUndead,
            SpellConstants.FalseLife,
            SpellConstants.GhoulTouch,
            SpellConstants.Scare,
            SpellConstants.SpectralHand,
            SpellConstants.AlterSelf,
            SpellConstants.BearsEndurance,
            SpellConstants.BullsStrength,
            SpellConstants.CatsGrace,
            SpellConstants.Darkvision,
            SpellConstants.EaglesSplendor,
            SpellConstants.FoxsCunning,
            SpellConstants.Knock,
            SpellConstants.Levitate,
            SpellConstants.OwlsWisdom,
            SpellConstants.Pyrotechnics,
            SpellConstants.RopeTrick,
            SpellConstants.SpiderClimb,
            SpellConstants.WhisperingWind)]
        [TestCase("3",
            SpellConstants.DispelMagic,
            SpellConstants.ExplosiveRunes,
            SpellConstants.MagicCircleAgainstEvil,
            SpellConstants.MagicCircleAgainstChaos,
            SpellConstants.MagicCircleAgainstGood,
            SpellConstants.MagicCircleAgainstLaw,
            SpellConstants.Nondetection,
            SpellConstants.ProtectionFromEnergy,
            SpellConstants.PhantomSteed,
            SpellConstants.SepiaSnakeSigil,
            SpellConstants.SleetStorm,
            SpellConstants.StinkingCloud,
            SpellConstants.SummonMonsterIII,
            SpellConstants.ArcaneSight,
            SpellConstants.ClairaudienceClairvoyance,
            SpellConstants.Tongues,
            SpellConstants.DeepSlumber,
            SpellConstants.Heroism,
            SpellConstants.HoldPerson,
            SpellConstants.Rage,
            SpellConstants.Suggestion,
            SpellConstants.Daylight,
            SpellConstants.Fireball,
            SpellConstants.LightningBolt,
            SpellConstants.LeomundsTinyHut,
            SpellConstants.WindWall,
            SpellConstants.Displacement,
            SpellConstants.IllusoryScript,
            SpellConstants.InvisibilitySphere,
            SpellConstants.MajorImage,
            SpellConstants.GentleRepose,
            SpellConstants.HaltUndead,
            SpellConstants.RayOfExhaustion,
            SpellConstants.VampiricTouch,
            SpellConstants.Blink,
            SpellConstants.FlameArrow,
            SpellConstants.Fly,
            SpellConstants.GaseousForm,
            SpellConstants.Haste,
            SpellConstants.KeenEdge,
            SpellConstants.MagicWeapon_Greater,
            SpellConstants.SecretPage,
            SpellConstants.ShrinkItem,
            SpellConstants.Slow,
            SpellConstants.WaterBreathing)]
        [TestCase("4",
            SpellConstants.DimensionalAnchor,
            SpellConstants.FireTrap,
            SpellConstants.GlobeOfInvulnerability_Lesser,
            SpellConstants.RemoveCurse,
            SpellConstants.Stoneskin,
            SpellConstants.EvardsBlackTentacles,
            SpellConstants.DimensionDoor,
            SpellConstants.MinorCreation,
            SpellConstants.LeomundsSecureShelter,
            SpellConstants.SolidFog,
            SpellConstants.SummonMonsterIV,
            SpellConstants.ArcaneEye,
            SpellConstants.DetectScrying,
            SpellConstants.LocateCreature,
            SpellConstants.Scrying,
            SpellConstants.CharmMonster,
            SpellConstants.Confusion,
            SpellConstants.CrushingDespair,
            SpellConstants.Geas_Lesser,
            SpellConstants.FireShield,
            SpellConstants.IceStorm,
            SpellConstants.OtilukesResilientSphere,
            SpellConstants.Shout,
            SpellConstants.WallOfFire,
            SpellConstants.WallOfIce,
            SpellConstants.HallucinatoryTerrain,
            SpellConstants.IllusoryWall,
            SpellConstants.Invisibility_Greater,
            SpellConstants.PhantasmalKiller,
            SpellConstants.RainbowPattern,
            SpellConstants.ShadowConjuration,
            SpellConstants.AnimateDead,
            SpellConstants.BestowCurse,
            SpellConstants.Contagion,
            SpellConstants.Enervation,
            SpellConstants.Fear,
            SpellConstants.EnlargePerson_Mass,
            SpellConstants.RarysMnemonicEnhancer,
            SpellConstants.Polymorph,
            SpellConstants.ReducePerson_Mass,
            SpellConstants.StoneShape)]
        [TestCase("5",
            SpellConstants.BreakEnchantment,
            SpellConstants.Dismissal,
            SpellConstants.MordenkainensPrivateSanctum,
            SpellConstants.Cloudkill,
            SpellConstants.MordenkainensFaithfulHound,
            SpellConstants.MajorCreation,
            SpellConstants.PlanarBinding_Lesser,
            SpellConstants.LeomundsSecretChest,
            SpellConstants.SummonMonsterV,
            SpellConstants.Teleport,
            SpellConstants.WallOfStone,
            SpellConstants.ContactOtherPlane,
            SpellConstants.PryingEyes,
            SpellConstants.RarysTelepathicBond,
            SpellConstants.DominatePerson,
            SpellConstants.Feeblemind,
            SpellConstants.HoldMonster,
            SpellConstants.MindFog,
            SpellConstants.SymbolOfSleep,
            SpellConstants.ConeOfCold,
            SpellConstants.BigbysInterposingHand,
            SpellConstants.Sending,
            SpellConstants.WallOfForce,
            SpellConstants.Dream,
            SpellConstants.FalseVision,
            SpellConstants.MirageArcana,
            SpellConstants.Nightmare,
            SpellConstants.PersistentImage,
            SpellConstants.Seeming,
            SpellConstants.ShadowEvocation,
            SpellConstants.Blight,
            SpellConstants.MagicJar,
            SpellConstants.SymbolOfPain,
            SpellConstants.WavesOfFatigue,
            SpellConstants.AnimalGrowth,
            SpellConstants.Fabricate,
            SpellConstants.OverlandFlight,
            SpellConstants.Passwall,
            SpellConstants.Telekinesis,
            SpellConstants.TransmuteMudToRock,
            SpellConstants.TransmuteRockToMud,
            SpellConstants.Permanency)]
        [TestCase("6",
            SpellConstants.AntimagicField,
            SpellConstants.DispelMagic_Greater,
            SpellConstants.GlobeOfInvulnerability,
            SpellConstants.GuardsAndWards,
            SpellConstants.Repulsion,
            SpellConstants.AcidFog,
            SpellConstants.PlanarBinding,
            SpellConstants.SummonMonsterVI,
            SpellConstants.WallOfIron,
            SpellConstants.AnalyzeDweomer,
            SpellConstants.LegendLore,
            SpellConstants.TrueSeeing,
            SpellConstants.GeasQuest,
            SpellConstants.Heroism_Greater,
            SpellConstants.Suggestion_Mass,
            SpellConstants.SymbolOfPersuasion,
            SpellConstants.ChainLightning,
            SpellConstants.Contingency,
            SpellConstants.BigbysForcefulHand,
            SpellConstants.OtilukesFreezingSphere,
            SpellConstants.Mislead,
            SpellConstants.PermanentImage,
            SpellConstants.ProgrammedImage,
            SpellConstants.ShadowWalk,
            SpellConstants.Veil,
            SpellConstants.CircleOfDeath,
            SpellConstants.CreateUndead,
            SpellConstants.Eyebite,
            SpellConstants.SymbolOfFear,
            SpellConstants.UndeathToDeath,
            SpellConstants.BearsEndurance_Mass,
            SpellConstants.BullsStrength_Mass,
            SpellConstants.CatsGrace_Mass,
            SpellConstants.ControlWater,
            SpellConstants.Disintegrate,
            SpellConstants.EaglesSplendor_Mass,
            SpellConstants.FleshToStone,
            SpellConstants.FoxsCunning_Mass,
            SpellConstants.MordenkainensLucubration,
            SpellConstants.MoveEarth,
            SpellConstants.OwlsWisdom_Mass,
            SpellConstants.StoneToFlesh,
            SpellConstants.TensersTransformation)]
        [TestCase("7",
            SpellConstants.Banishment,
            SpellConstants.Sequester,
            SpellConstants.SpellTurning,
            SpellConstants.DrawmijsInstantSummons,
            SpellConstants.MordenkainensMagnificentMansion,
            SpellConstants.PhaseDoor,
            SpellConstants.PlaneShift,
            SpellConstants.SummonMonsterVII,
            SpellConstants.Teleport_Greater,
            SpellConstants.TeleportObject,
            SpellConstants.ArcaneSight_Greater,
            SpellConstants.Scrying_Greater,
            SpellConstants.Vision,
            SpellConstants.HoldPerson_Mass,
            SpellConstants.Insanity,
            SpellConstants.PowerWordBlind,
            SpellConstants.SymbolOfStunning,
            SpellConstants.DelayedBlastFireball,
            SpellConstants.Forcecage,
            SpellConstants.BigbysGraspingHand,
            SpellConstants.MordenkainensSword,
            SpellConstants.PrismaticSpray,
            SpellConstants.Invisibility_Mass,
            SpellConstants.ProjectImage,
            SpellConstants.ShadowConjuration_Greater,
            SpellConstants.Simulacrum,
            SpellConstants.ControlUndead,
            SpellConstants.FingerOfDeath,
            SpellConstants.SymbolOfWeakness,
            SpellConstants.WavesOfExhaustion,
            SpellConstants.ControlWeather,
            SpellConstants.EtherealJaunt,
            SpellConstants.ReverseGravity,
            SpellConstants.Statue,
            SpellConstants.LimitedWish)]
        [TestCase("8",
            SpellConstants.DimensionalLock,
            SpellConstants.MindBlank,
            SpellConstants.PrismaticWall,
            SpellConstants.ProtectionFromSpells,
            SpellConstants.IncendiaryCloud,
            SpellConstants.Maze,
            SpellConstants.PlanarBinding_Greater,
            SpellConstants.SummonMonsterVIII,
            SpellConstants.TrapTheSoul,
            SpellConstants.DiscernLocation,
            SpellConstants.MomentOfPrescience,
            SpellConstants.PryingEyes_Greater,
            SpellConstants.Antipathy,
            SpellConstants.Binding,
            SpellConstants.CharmMonster_Mass,
            SpellConstants.Demand,
            SpellConstants.OttosIrresistibleDance,
            SpellConstants.PowerWordStun,
            SpellConstants.SymbolOfInsanity,
            SpellConstants.Sympathy,
            SpellConstants.BigbysClenchedFist,
            SpellConstants.PolarRay,
            SpellConstants.Shout_Greater,
            SpellConstants.Sunburst,
            SpellConstants.OtilukesTelekineticSphere,
            SpellConstants.ScintillatingPattern,
            SpellConstants.Screen,
            SpellConstants.ShadowEvocation_Greater,
            SpellConstants.Clone,
            SpellConstants.CreateGreaterUndead,
            SpellConstants.HorridWilting,
            SpellConstants.SymbolOfDeath,
            SpellConstants.IronBody,
            SpellConstants.PolymorphAnyObject,
            SpellConstants.TemporalStasis)]
        [TestCase("9",
            SpellConstants.Freedom,
            SpellConstants.Imprisonment,
            SpellConstants.MordenkainensDisjunction,
            SpellConstants.PrismaticSphere,
            SpellConstants.Gate,
            SpellConstants.Refuge,
            SpellConstants.SummonMonsterIX,
            SpellConstants.TeleportationCircle,
            SpellConstants.Foresight,
            SpellConstants.DominateMonster,
            SpellConstants.HoldMonster_Mass,
            SpellConstants.PowerWordKill,
            SpellConstants.BigbysCrushingHand,
            SpellConstants.MeteorSwarm,
            SpellConstants.Shades,
            SpellConstants.Weird,
            SpellConstants.AstralProjection,
            SpellConstants.EnergyDrain,
            SpellConstants.SoulBind,
            SpellConstants.WailOfTheBanshee,
            SpellConstants.Etherealness,
            SpellConstants.Shapechange,
            SpellConstants.TimeStop,
            SpellConstants.Wish)]
        public override void Collection(string name, params string[] collection)
        {
            base.Collection(name, collection);
        }
    }
}
