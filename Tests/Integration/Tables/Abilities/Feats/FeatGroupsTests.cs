using System;
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

        [TestCase(CharacterClassConstants.Cleric,
            FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.LightArmorProficiencyId,
            FeatConstants.MediumArmorProficiencyId,
            FeatConstants.HeavyArmorProficiencyId,
            FeatConstants.ShieldProficiencyId,
            FeatConstants.TurnUndeadId)]
        [TestCase(CharacterClassConstants.Wizard,
            FeatConstants.ScribeScrollId,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Club,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Dagger,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.HeavyCrossbow,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Quarterstaff)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId,
            FeatConstants.SaveBonusId + "All",
            FeatConstants.SaveBonusId + "Fear",
            FeatConstants.AttackBonusId + "ThrowOrSling",
            FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId + SkillConstants.Appraise,
            FeatConstants.DarkvisionId,
            FeatConstants.StonecunningId)]
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
        [TestCase(RaceConstants.BaseRaces.HumanId)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId,
            FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.ShieldProficiencyId,
            FeatConstants.NaturalArmorId,
            FeatConstants.HoldBreathId,
            FeatConstants.NaturalWeaponId + "Claw",
            FeatConstants.NaturalWeaponId + "Bite")]
        [TestCase(RaceConstants.Metaraces.NoneId)]
        [TestCase("")]
        [TestCase(TableNameConstants.Set.Collection.Groups.TakenMultipleTimes,
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

        [Test]
        public void RogueFeatSelections()
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
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.HandCrossbow,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Sap,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.ShortSword,
                FeatConstants.LightArmorProficiencyId
            };

            base.DistinctCollection(CharacterClassConstants.Rogue, featIds);
        }

        [Test]
        public void AdditionalFeatSelections()
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

            base.DistinctCollection(TableNameConstants.Set.Collection.Groups.Additional, featIds);
        }
    }
}