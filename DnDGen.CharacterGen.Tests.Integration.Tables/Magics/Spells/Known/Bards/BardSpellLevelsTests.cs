﻿using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Bards
{
    [TestFixture]
    public class BardSpellLevelsTests : CollectionTests
    {
        protected override string tableName => string.Format(TableNameConstants.Formattable.Collection.CLASSSpellLevels, CharacterClassConstants.Bard);

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 7).Select(n => n.ToString());
            AssertCollectionNames(names);
        }

        [TestCase("0",
            SpellConstants.DancingLights,
            SpellConstants.Daze,
            SpellConstants.DetectMagic,
            SpellConstants.Flare,
            SpellConstants.GhostSound,
            SpellConstants.KnowDirection,
            SpellConstants.Light,
            SpellConstants.Lullaby,
            SpellConstants.MageHand,
            SpellConstants.Mending,
            SpellConstants.Message,
            SpellConstants.OpenClose,
            SpellConstants.Prestidigitation,
            SpellConstants.ReadMagic,
            SpellConstants.Resistance,
            SpellConstants.SummonInstrument)]
        [TestCase("1",
            SpellConstants.Alarm,
            SpellConstants.AnimateRope,
            SpellConstants.CauseFear,
            SpellConstants.CharmPerson,
            SpellConstants.ComprehendLanguages,
            SpellConstants.Confusion_Lesser,
            SpellConstants.CureLightWounds,
            SpellConstants.DetectSecretDoors,
            SpellConstants.DisguiseSelf,
            SpellConstants.Erase,
            SpellConstants.ExpeditiousRetreat,
            SpellConstants.FeatherFall,
            SpellConstants.Grease,
            SpellConstants.TashasHideousLaughter,
            SpellConstants.Hypnotism,
            SpellConstants.Identify,
            SpellConstants.MagicMouth,
            SpellConstants.NystulsMagicAura,
            SpellConstants.ObscureObject,
            SpellConstants.RemoveFear,
            SpellConstants.SilentImage,
            SpellConstants.Sleep,
            SpellConstants.SummonMonsterI,
            SpellConstants.UndetectableAlignment,
            SpellConstants.UnseenServant,
            SpellConstants.Ventriloquism)]
        [TestCase("2",
            SpellConstants.AlterSelf,
            SpellConstants.AnimalMessenger,
            SpellConstants.AnimalTrance,
            SpellConstants.BlindnessDeafness,
            SpellConstants.Blur,
            SpellConstants.CalmEmotions,
            SpellConstants.CatsGrace,
            SpellConstants.CureModerateWounds,
            SpellConstants.Darkness,
            SpellConstants.DazeMonster,
            SpellConstants.DelayPoison,
            SpellConstants.DetectThoughts,
            SpellConstants.EaglesSplendor,
            SpellConstants.Enthrall,
            SpellConstants.FoxsCunning,
            SpellConstants.Glitterdust,
            SpellConstants.Heroism,
            SpellConstants.HoldPerson,
            SpellConstants.HypnoticPattern,
            SpellConstants.Invisibility,
            SpellConstants.LocateObject,
            SpellConstants.MinorImage,
            SpellConstants.MirrorImage,
            SpellConstants.Misdirection,
            SpellConstants.Pyrotechnics,
            SpellConstants.Rage,
            SpellConstants.Scare,
            SpellConstants.Shatter,
            SpellConstants.Silence,
            SpellConstants.SoundBurst,
            SpellConstants.Suggestion,
            SpellConstants.SummonMonsterII,
            SpellConstants.SummonSwarm,
            SpellConstants.Tongues,
            SpellConstants.WhisperingWind)]
        [TestCase("3",
            SpellConstants.Blink,
            SpellConstants.CharmMonster,
            SpellConstants.ClairaudienceClairvoyance,
            SpellConstants.Confusion,
            SpellConstants.CrushingDespair,
            SpellConstants.CureSeriousWounds,
            SpellConstants.Daylight,
            SpellConstants.DeepSlumber,
            SpellConstants.DispelMagic,
            SpellConstants.Displacement,
            SpellConstants.Fear,
            SpellConstants.GaseousForm,
            SpellConstants.Geas_Lesser,
            SpellConstants.Glibness,
            SpellConstants.GoodHope,
            SpellConstants.Haste,
            SpellConstants.IllusoryScript,
            SpellConstants.InvisibilitySphere,
            SpellConstants.MajorImage,
            SpellConstants.PhantomSteed,
            SpellConstants.RemoveCurse,
            SpellConstants.Scrying,
            SpellConstants.SculptSound,
            SpellConstants.SecretPage,
            SpellConstants.SeeInvisibility,
            SpellConstants.SepiaSnakeSigil,
            SpellConstants.Slow,
            SpellConstants.SpeakWithAnimals,
            SpellConstants.SummonMonsterIII,
            SpellConstants.LeomundsTinyHut)]
        [TestCase("4",
            SpellConstants.BreakEnchantment,
            SpellConstants.CureCriticalWounds,
            SpellConstants.DetectScrying,
            SpellConstants.DimensionDoor,
            SpellConstants.DominatePerson,
            SpellConstants.FreedomOfMovement,
            SpellConstants.HallucinatoryTerrain,
            SpellConstants.HoldMonster,
            SpellConstants.Invisibility_Greater,
            SpellConstants.LegendLore,
            SpellConstants.LocateCreature,
            SpellConstants.ModifyMemory,
            SpellConstants.NeutralizePoison,
            SpellConstants.RainbowPattern,
            SpellConstants.RepelVermin,
            SpellConstants.LeomundsSecureShelter,
            SpellConstants.ShadowConjuration,
            SpellConstants.Shout,
            SpellConstants.SpeakWithPlants,
            SpellConstants.SummonMonsterIV,
            SpellConstants.ZoneOfSilence)]
        [TestCase("5",
            SpellConstants.CureLightWounds_Mass,
            SpellConstants.DispelMagic_Greater,
            SpellConstants.Dream,
            SpellConstants.FalseVision,
            SpellConstants.Heroism_Greater,
            SpellConstants.MindFog,
            SpellConstants.MirageArcana,
            SpellConstants.Mislead,
            SpellConstants.Nightmare,
            SpellConstants.PersistentImage,
            SpellConstants.Seeming,
            SpellConstants.ShadowEvocation,
            SpellConstants.ShadowWalk,
            SpellConstants.SongOfDiscord,
            SpellConstants.Suggestion_Mass,
            SpellConstants.SummonMonsterV)]
        [TestCase("6",
            SpellConstants.AnalyzeDweomer,
            SpellConstants.AnimateObjects,
            SpellConstants.CatsGrace_Mass,
            SpellConstants.CharmMonster_Mass,
            SpellConstants.CureModerateWounds_Mass,
            SpellConstants.EaglesSplendor_Mass,
            SpellConstants.Eyebite,
            SpellConstants.FindThePath,
            SpellConstants.FoxsCunning_Mass,
            SpellConstants.GeasQuest,
            SpellConstants.HeroesFeast,
            SpellConstants.OttosIrresistibleDance,
            SpellConstants.PermanentImage,
            SpellConstants.ProgrammedImage,
            SpellConstants.ProjectImage,
            SpellConstants.Scrying_Greater,
            SpellConstants.Shout_Greater,
            SpellConstants.SummonMonsterVI,
            SpellConstants.SympatheticVibration,
            SpellConstants.Veil)]
        public override void Collection(string name, params string[] collection)
        {
            base.Collection(name, collection);
        }
    }
}
