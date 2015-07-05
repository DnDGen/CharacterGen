using System;
using System.Linq;
using EquipmentGen.Common.Items;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
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
                "",
                GroupConstants.HasClassRequirements,
                GroupConstants.HasSkillRequirements,
                GroupConstants.HasStatRequirements,
                GroupConstants.TakenMultipleTimes,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard,
                CharacterClassConstants.Domains.Death,
                CharacterClassConstants.Domains.Earth,
                CharacterClassConstants.Domains.Evil,
                CharacterClassConstants.Domains.Strength,
                CharacterClassConstants.Schools.Conjuration,
                CharacterClassConstants.Schools.Evocation,
                RaceConstants.BaseRaces.DeepHalflingId,
                RaceConstants.BaseRaces.DoppelgangerId,
                RaceConstants.BaseRaces.GnollId,
                RaceConstants.BaseRaces.GoblinId,
                RaceConstants.BaseRaces.HalfElfId,
                RaceConstants.BaseRaces.HalfOrcId,
                RaceConstants.BaseRaces.HighElfId,
                RaceConstants.BaseRaces.HillDwarfId,
                RaceConstants.BaseRaces.HobgoblinId,
                RaceConstants.BaseRaces.HumanId,
                RaceConstants.BaseRaces.LightfootHalflingId,
                RaceConstants.BaseRaces.LizardfolkId,
                RaceConstants.Metaraces.NoneId,
                RaceConstants.BaseRaces.OrcId,
                RaceConstants.BaseRaces.RockGnomeId,
                RaceConstants.BaseRaces.TallfellowHalflingId,
                RaceConstants.Metaraces.WerewolfId,
                RaceConstants.BaseRaces.WoodElfId,
                GroupConstants.Additional,
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Rogue,
                RaceConstants.Metaraces.HalfCelestialId
            };

            AssertCollectionNames(names);
        }

        [TestCase("")]
        [TestCase(GroupConstants.HasClassRequirements)]
        [TestCase(GroupConstants.HasSkillRequirements)]
        [TestCase(GroupConstants.HasStatRequirements)]
        [TestCase(GroupConstants.TakenMultipleTimes,
            FeatConstants.SpellMasteryId,
            FeatConstants.ToughnessId,
            FeatConstants.SkillMasteryId,
            FeatConstants.SkillBonusId,
            FeatConstants.AttackBonusId,
            FeatConstants.DodgeBonusId,
            FeatConstants.SaveBonusId)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [TestCase(CharacterClassConstants.Cleric,
            FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.LightArmorProficiencyId,
            FeatConstants.MediumArmorProficiencyId,
            FeatConstants.HeavyArmorProficiencyId,
            FeatConstants.ShieldProficiencyId,
            FeatConstants.TurnId)]
        [TestCase(CharacterClassConstants.Fighter,
            FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.MartialWeaponProficiencyId,
            FeatConstants.LightArmorProficiencyId,
            FeatConstants.MediumArmorProficiencyId,
            FeatConstants.HeavyArmorProficiencyId,
            FeatConstants.ShieldProficiencyId,
            FeatConstants.TowerShieldProficiencyId)]
        [TestCase(CharacterClassConstants.Ranger,
            FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.MartialWeaponProficiencyId,
            FeatConstants.LightArmorProficiencyId,
            FeatConstants.ShieldProficiencyId,
            FeatConstants.FavoredEnemyId + "1",
            FeatConstants.FavoredEnemyId + "2",
            FeatConstants.FavoredEnemyId + "3",
            FeatConstants.FavoredEnemyId + "4",
            FeatConstants.FavoredEnemyId + "5",
            FeatConstants.TrackId,
            FeatConstants.WildEmpathyId,
            FeatConstants.CombatStyleId,
            FeatConstants.EnduranceId,
            FeatConstants.ImprovedCombatStyleId,
            FeatConstants.WoodlandStrideId,
            FeatConstants.SwiftTrackerId,
            FeatConstants.EvasionId,
            FeatConstants.CombatStyleMasteryId,
            FeatConstants.CamouflageId,
            FeatConstants.HideInPlainSightId)]
        [TestCase(CharacterClassConstants.Sorcerer,
            FeatConstants.SimpleWeaponProficiencyId)]
        [TestCase(CharacterClassConstants.Wizard,
            FeatConstants.ScribeScrollId,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Club,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Dagger,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.HeavyCrossbow,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Quarterstaff)]
        [TestCase(CharacterClassConstants.Domains.Death,
            FeatConstants.DeathTouchId)]
        [TestCase(CharacterClassConstants.Domains.Earth,
            FeatConstants.TurnId)]
        [TestCase(CharacterClassConstants.Domains.Evil,
            FeatConstants.CastSpellBonusId)]
        [TestCase(CharacterClassConstants.Domains.Strength,
            FeatConstants.SupernaturalStrengthId)]
        [TestCase(CharacterClassConstants.Schools.Conjuration,
            FeatConstants.SkillBonusId)]
        [TestCase(CharacterClassConstants.Schools.Evocation,
            FeatConstants.SkillBonusId)]
        public void ClassFeatGroup(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [TestCase(RaceConstants.BaseRaces.DeepHalflingId,
            FeatConstants.SaveBonusId + "All",
            FeatConstants.SaveBonusId + "Fear",
            FeatConstants.AttackBonusId + "ThrowOrSling",
            FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId + SkillConstants.Appraise,
            FeatConstants.DarkvisionId,
            FeatConstants.StonecunningId)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId,
            FeatConstants.SkillBonusId + SkillConstants.Bluff,
            FeatConstants.SkillBonusId + SkillConstants.Disguise,
            FeatConstants.DarkvisionId,
            FeatConstants.NaturalArmorId,
            FeatConstants.SpellLikeAbilityId,
            FeatConstants.ChangeShapeId,
            FeatConstants.ImmuneToEffectId + "Sleep",
            FeatConstants.ImmuneToEffectId + "Charm")]
        [TestCase(RaceConstants.BaseRaces.GnollId,
            FeatConstants.DarkvisionId,
            FeatConstants.NaturalArmorId)]
        [TestCase(RaceConstants.BaseRaces.GoblinId,
            FeatConstants.DarkvisionId,
            FeatConstants.SkillBonusId + SkillConstants.MoveSilently,
            FeatConstants.SkillBonusId + SkillConstants.Ride)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId,
            FeatConstants.ImmuneToEffectId,
            FeatConstants.SaveBonusId,
            FeatConstants.LowLightVisionId,
            FeatConstants.ElvenBloodId,
            FeatConstants.SkillBonusId + SkillConstants.Search,
            FeatConstants.SkillBonusId + SkillConstants.Spot,
            FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId + SkillConstants.Diplomacy,
            FeatConstants.SkillBonusId + SkillConstants.GatherInformation)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId,
            FeatConstants.DarkvisionId,
            FeatConstants.OrcBloodId)]
        [TestCase(RaceConstants.BaseRaces.HighElfId,
            FeatConstants.ImmuneToEffectId,
            FeatConstants.SaveBonusId,
            FeatConstants.LowLightVisionId,
            FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
            FeatConstants.PassiveSecretDoorSearchId,
            FeatConstants.SkillBonusId + SkillConstants.Search,
            FeatConstants.SkillBonusId + SkillConstants.Spot,
            FeatConstants.SkillBonusId + SkillConstants.Listen)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId,
            FeatConstants.DarkvisionId,
            FeatConstants.StonecunningId,
            FeatConstants.WeaponFamiliarityId + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarityId + WeaponConstants.DwarvenWaraxe,
            FeatConstants.StabilityId,
            FeatConstants.SaveBonusId + "Poison",
            FeatConstants.SaveBonusId + "Spell",
            FeatConstants.AttackBonusId + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonusId + RaceConstants.BaseRaces.Goblin,
            FeatConstants.DodgeBonusId,
            FeatConstants.SkillBonusId + SkillConstants.Appraise)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId,
            FeatConstants.DarkvisionId,
            FeatConstants.SkillBonusId)]
        [TestCase(RaceConstants.BaseRaces.HumanId)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId,
            FeatConstants.SaveBonusId + "All",
            FeatConstants.SaveBonusId + "Fear",
            FeatConstants.AttackBonusId + "ThrowOrSling",
            FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId + SkillConstants.Climb,
            FeatConstants.SkillBonusId + SkillConstants.Jump,
            FeatConstants.SkillBonusId + SkillConstants.MoveSilently)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId,
            FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.ShieldProficiencyId,
            FeatConstants.NaturalArmorId,
            FeatConstants.HoldBreathId,
            FeatConstants.NaturalWeaponId + "Claw",
            FeatConstants.NaturalWeaponId + "Bite")]
        [TestCase(RaceConstants.Metaraces.NoneId)]
        [TestCase(RaceConstants.BaseRaces.OrcId,
            FeatConstants.DarkvisionId,
            FeatConstants.LightSensitivityId)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId,
            FeatConstants.LowLightVisionId,
            FeatConstants.WeaponFamiliarityId,
            FeatConstants.SaveBonusId,
            FeatConstants.ImprovedSpellId,
            FeatConstants.AttackBonusId + RaceConstants.BaseRaces.GoblinId,
            FeatConstants.AttackBonusId + RaceConstants.BaseRaces.KoboldId,
            FeatConstants.DodgeBonusId,
            FeatConstants.SkillBonusId,
            FeatConstants.SpellLikeAbilityId + "Speak with animals",
            FeatConstants.SpellLikeAbilityId + "Dancing lights",
            FeatConstants.SpellLikeAbilityId + "Ghost sound",
            FeatConstants.SpellLikeAbilityId + "Prestidigitation")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId,
            FeatConstants.SaveBonusId + "All",
            FeatConstants.SaveBonusId + "Fear",
            FeatConstants.AttackBonusId + "ThrowOrSling",
            FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId + SkillConstants.Search,
            FeatConstants.SkillBonusId + SkillConstants.Spot,
            FeatConstants.PassiveSecretDoorSearchId)]
        [TestCase(RaceConstants.Metaraces.WerewolfId,
            FeatConstants.IronWillId,
            FeatConstants.TrackId)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId,
            FeatConstants.ImmuneToEffectId,
            FeatConstants.SaveBonusId,
            FeatConstants.LowLightVisionId,
            FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
            FeatConstants.PassiveSecretDoorSearchId,
            FeatConstants.SkillBonusId + SkillConstants.Search,
            FeatConstants.SkillBonusId + SkillConstants.Spot,
            FeatConstants.SkillBonusId + SkillConstants.Listen)]
        public void RaceFeatGroup(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void HalfCelestialFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SpellLikeAbilityId + "Daylight",
                FeatConstants.SmiteEvilId,
                FeatConstants.SpellLikeAbilityId + "Protection from evil",
                FeatConstants.SpellLikeAbilityId + "Bless",
                FeatConstants.SpellLikeAbilityId + "Aid",
                FeatConstants.SpellLikeAbilityId + "Detect evil",
                FeatConstants.SpellLikeAbilityId + "Cure serious wounds",
                FeatConstants.SpellLikeAbilityId + "Neutralize poison",
                FeatConstants.SpellLikeAbilityId + "Holy smite",
                FeatConstants.SpellLikeAbilityId + "Remove disease",
                FeatConstants.SpellLikeAbilityId + "Dispel evil",
                FeatConstants.SpellLikeAbilityId + "Holy word",
                FeatConstants.SpellLikeAbilityId + "Holy aura",
                FeatConstants.SpellLikeAbilityId + "Hallow",
                FeatConstants.SpellLikeAbilityId + "Mass charm monster",
                FeatConstants.SpellLikeAbilityId + "Summon monster IX (Celestials only)",
                FeatConstants.SpellLikeAbilityId + "Resurrection",
                FeatConstants.NaturalArmorId,
                FeatConstants.DarkvisionId,
                FeatConstants.ImmuneToEffectId,
                FeatConstants.ResistanceToAcidId,
                FeatConstants.ResistanceToColdId,
                FeatConstants.ResistanceToElectricityId,
                FeatConstants.DamageReductionId + "10-",
                FeatConstants.DamageReductionId + "11+",
                FeatConstants.SpellResistanceId,
                FeatConstants.SaveBonusId
            };

            base.DistinctCollection(RaceConstants.Metaraces.HalfCelestialId, featIds);
        }

        [Test]
        public void BarbarianFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SimpleWeaponProficiencyId,
                FeatConstants.MartialWeaponProficiencyId,
                FeatConstants.LightArmorProficiencyId,
                FeatConstants.MediumArmorProficiencyId,
                FeatConstants.ShieldProficiencyId,
                FeatConstants.FastMovementId,
                FeatConstants.IlliteracyId,
                FeatConstants.RageId + "1",
                FeatConstants.RageId + "2",
                FeatConstants.RageId + "3",
                FeatConstants.RageId + "4",
                FeatConstants.RageId + "5",
                FeatConstants.RageId + "6",
                FeatConstants.UncannyDodgeId,
                FeatConstants.TrapSenseId + "1",
                FeatConstants.TrapSenseId + "2",
                FeatConstants.TrapSenseId + "3",
                FeatConstants.TrapSenseId + "4",
                FeatConstants.TrapSenseId + "5",
                FeatConstants.TrapSenseId + "6",
                FeatConstants.ImprovedUncannyDodgeId,
                FeatConstants.DamageReductionId + "1",
                FeatConstants.DamageReductionId + "2",
                FeatConstants.DamageReductionId + "3",
                FeatConstants.DamageReductionId + "4",
                FeatConstants.DamageReductionId + "5",
                FeatConstants.GreaterRageId,
                FeatConstants.IndomitableWillId,
                FeatConstants.TirelessRageId,
                FeatConstants.MightyRageId
            };

            base.DistinctCollection(CharacterClassConstants.Barbarian, featIds);
        }

        [Test]
        public void BardFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SimpleWeaponProficiencyId,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longsword,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Sap,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.ShortSword,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
                FeatConstants.ExoticWeaponProficiencyId + WeaponConstants.Whip,
                FeatConstants.LightArmorProficiencyId,
                FeatConstants.ShieldProficiencyId,
                FeatConstants.BardicMusicId + "1",
                FeatConstants.BardicMusicId + "2",
                FeatConstants.BardicMusicId + "3",
                FeatConstants.BardicMusicId + "4",
                FeatConstants.BardicMusicId + "5",
                FeatConstants.BardicMusicId + "6",
                FeatConstants.BardicMusicId + "7",
                FeatConstants.BardicMusicId + "8",
                FeatConstants.BardicMusicId + "9",
                FeatConstants.BardicMusicId + "10",
                FeatConstants.BardicMusicId + "11",
                FeatConstants.BardicMusicId + "12",
                FeatConstants.BardicMusicId + "13",
                FeatConstants.BardicMusicId + "14",
                FeatConstants.BardicMusicId + "15",
                FeatConstants.BardicMusicId + "16",
                FeatConstants.BardicMusicId + "17",
                FeatConstants.BardicMusicId + "18",
                FeatConstants.BardicMusicId + "19",
                FeatConstants.BardicMusicId + "20",
                FeatConstants.BardicKnowledgeId,
                FeatConstants.CountersongId,
                FeatConstants.FascinateId,
                FeatConstants.InspireCourageId + "1",
                FeatConstants.InspireCourageId + "2",
                FeatConstants.InspireCourageId + "3",
                FeatConstants.InspireCourageId + "4",
                FeatConstants.InspireCompetenceId,
                FeatConstants.SuggestionId,
                FeatConstants.InspireGreatnessId,
                FeatConstants.SongOfFreedomId,
                FeatConstants.InspireHeroicsId,
                FeatConstants.MassSuggestionId
            };

            base.DistinctCollection(CharacterClassConstants.Bard, featIds);
        }

        [Test]
        public void RogueFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.SneakAttackId + "1",
                FeatConstants.SneakAttackId + "2",
                FeatConstants.SneakAttackId + "3",
                FeatConstants.SneakAttackId + "4",
                FeatConstants.SneakAttackId + "5",
                FeatConstants.SneakAttackId + "6",
                FeatConstants.SneakAttackId + "7",
                FeatConstants.SneakAttackId + "8",
                FeatConstants.SneakAttackId + "9",
                FeatConstants.SneakAttackId + "10",
                FeatConstants.TrapfindingId,
                FeatConstants.EvasionId,
                FeatConstants.UncannyDodgeId,
                FeatConstants.TrapSenseId + "1",
                FeatConstants.TrapSenseId + "2",
                FeatConstants.TrapSenseId + "3",
                FeatConstants.TrapSenseId + "4",
                FeatConstants.TrapSenseId + "5",
                FeatConstants.TrapSenseId + "6",
                FeatConstants.ImprovedUncannyDodgeId,
                FeatConstants.SimpleWeaponProficiencyId,
                FeatConstants.ExoticWeaponProficiencyId + WeaponConstants.HandCrossbow,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Sap,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.ShortSword,
                FeatConstants.LightArmorProficiencyId
            };

            base.DistinctCollection(CharacterClassConstants.Rogue, featIds);
        }

        [Test]
        public void AdditionalFeatGroup()
        {
            var featIds = new[]
            {
                FeatConstants.AcrobaticId,
                FeatConstants.AgileId,
                FeatConstants.AlertnessId,
                FeatConstants.AmbidexterityId,
                FeatConstants.AnimalAffinityId,
                FeatConstants.AthleticId,
                FeatConstants.AugmentSummoningId,
                FeatConstants.BlindFightId,
                FeatConstants.CleaveId,
                FeatConstants.CombatCastingId,
                FeatConstants.CombatExpertiseId,
                FeatConstants.CombatReflexesId,
                FeatConstants.CripplingStrikeId,
                FeatConstants.DeceitfulId,
                FeatConstants.DefensiveRollId,
                FeatConstants.DeflectArrowsId,
                FeatConstants.DeftHandsId,
                FeatConstants.DiehardId,
                FeatConstants.DiligentId,
                FeatConstants.DodgeId,
                FeatConstants.EmpowerSpellId,
                FeatConstants.EnduranceId,
                FeatConstants.EnlargeSpellId,
                FeatConstants.EschewMaterialsId,
                FeatConstants.ExoticWeaponProficiencyId,
                FeatConstants.ExtendSpellId,
                FeatConstants.ExtraTurningId,
                FeatConstants.FarShotId,
                FeatConstants.GreatCleaveId,
                FeatConstants.GreaterSpellFocusId,
                FeatConstants.GreaterSpellPenetrationId,
                FeatConstants.GreaterTwoWeaponFightingId,
                FeatConstants.GreaterWeaponFocusId,
                FeatConstants.GreaterWeaponSpecializationId,
                FeatConstants.GreatFortitudeId,
                FeatConstants.HeavyArmorProficiencyId,
                FeatConstants.HeightenSpellId,
                FeatConstants.ImprovedBullRushId,
                FeatConstants.ImprovedCounterspellId,
                FeatConstants.ImprovedCriticalId,
                FeatConstants.ImprovedDisarmId,
                FeatConstants.ImprovedEvasionId,
                FeatConstants.ImprovedFamiliarId,
                FeatConstants.ImprovedFeintId,
                FeatConstants.ImprovedGrappleId,
                FeatConstants.ImprovedInitiativeId,
                FeatConstants.ImprovedOverrunId,
                FeatConstants.ImprovedPreciseShotId,
                FeatConstants.ImprovedShieldBashId,
                FeatConstants.ImprovedSunderId,
                FeatConstants.ImprovedTripId,
                FeatConstants.ImprovedTurningId,
                FeatConstants.ImprovedTwoWeaponFightingId,
                FeatConstants.ImprovedUnarmedStrikeId,
                FeatConstants.InvestigatorId,
                FeatConstants.IronWillId,
                FeatConstants.LeadershipId,
                FeatConstants.LightArmorProficiencyId,
                FeatConstants.LightningReflexesId,
                FeatConstants.MagicalAptitudeId,
                FeatConstants.ManyshotId,
                FeatConstants.MartialWeaponProficiencyId,
                FeatConstants.MaximizeSpellId,
                FeatConstants.MediumArmorProficiencyId,
                FeatConstants.MobilityId,
                FeatConstants.MountedArcheryId,
                FeatConstants.MountedCombatId,
                FeatConstants.NaturalSpellId,
                FeatConstants.NegotiatorId,
                FeatConstants.NimbleFingersId,
                FeatConstants.OpportunistId,
                FeatConstants.PersuasiveId,
                FeatConstants.PointBlankShotId,
                FeatConstants.PowerAttackId,
                FeatConstants.PreciseShotId,
                FeatConstants.QuickDrawId,
                FeatConstants.QuickenSpellId,
                FeatConstants.RapidReloadId,
                FeatConstants.RapidShotId,
                FeatConstants.RideByAttackId,
                FeatConstants.RunId,
                FeatConstants.SelfSufficientId,
                FeatConstants.ShieldProficiencyId,
                FeatConstants.ShotOnTheRunId,
                FeatConstants.SilentSpellId,
                FeatConstants.SimpleWeaponProficiencyId,
                FeatConstants.SkillFocusId,
                FeatConstants.SkillMasteryId,
                FeatConstants.SlipperyMindId,
                FeatConstants.SnatchArrowsId,
                FeatConstants.SpellFocusId,
                FeatConstants.SpellMasteryId,
                FeatConstants.SpellPenetrationId,
                FeatConstants.SpiritedChargeId,
                FeatConstants.SpringAttackId,
                FeatConstants.StabilityId,
                FeatConstants.StealthyId,
                FeatConstants.StillSpellId,
                FeatConstants.StunningFistId,
                FeatConstants.ToughnessId,
                FeatConstants.TowerShieldProficiencyId,
                FeatConstants.TrackId,
                FeatConstants.TrampleId,
                FeatConstants.TwoWeaponDefenseId,
                FeatConstants.TwoWeaponFightingId,
                FeatConstants.WeaponFocusId,
                FeatConstants.WeaponFinesseId,
                FeatConstants.WeaponSpecializationId,
                FeatConstants.WhirlwindAttackId,
                FeatConstants.WidenSpellId
            };

            base.DistinctCollection(GroupConstants.Additional, featIds);
        }

        [TestCase(CharacterClassConstants.Ranger,
            FeatConstants.ImprovedCombatStyleId,
            FeatConstants.CombatStyleId)]
        [TestCase(CharacterClassConstants.Ranger,
            FeatConstants.CombatStyleMasteryId,
            FeatConstants.ImprovedCombatStyleId)]
        public void FeatGroupDependency(String name, String featId, String dependencyFeatId)
        {
            var collection = table[name].ToList();
            var featIndex = collection.IndexOf(featId);
            var dependencyIndex = collection.IndexOf(dependencyFeatId);

            Assert.That(featIndex, Is.Positive);
            Assert.That(dependencyIndex, Is.Positive);
            Assert.That(featIndex, Is.GreaterThan(dependencyIndex));
        }
    }
}