using System;
using NPCGen.Common.Abilities.Feats;
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

        [TestCase(CharacterClassConstants.Rogue,
            FeatConstants.SneakAttackId,
            FeatConstants.TrapfindingId,
            FeatConstants.EvasionId,
            FeatConstants.UncannyDodgeId,
            FeatConstants.TrapSenseId,
            FeatConstants.ImprovedUncannyDodgeId)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId,
            FeatConstants.SaveBonusId,
            FeatConstants.AttackBonusId,
            FeatConstants.SkillBonusId,
            FeatConstants.DarkvisionId,
            FeatConstants.StonecunningId)]
        [TestCase(RaceConstants.BaseRaces.GoblinId,
            FeatConstants.DarkvisionId,
            FeatConstants.SkillBonusId)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId,
            FeatConstants.ImmuneToEffectId,
            FeatConstants.SaveBonusId,
            FeatConstants.LowLightVisionId,
            FeatConstants.ElvenBloodId,
            FeatConstants.SkillBonusId)]
        [TestCase(RaceConstants.BaseRaces.HighElfId,
            FeatConstants.ImmuneToEffectId,
            FeatConstants.SaveBonusId,
            FeatConstants.LowLightVisionId,
            FeatConstants.MartialWeaponProficiencyId,
            FeatConstants.SkillBonusId)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId,
            FeatConstants.DarkvisionId,
            FeatConstants.StonecunningId,
            FeatConstants.WeaponFamiliarityId,
            FeatConstants.StabilityId,
            FeatConstants.SaveBonusId,
            FeatConstants.AttackBonusId,
            FeatConstants.DodgeBonusId,
            FeatConstants.SkillBonusId)]
        [TestCase(RaceConstants.BaseRaces.HumanId)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId,
            FeatConstants.NaturalArmorId,
            FeatConstants.HoldBreathId,
            FeatConstants.NaturalWeaponId)]
        [TestCase(TableNameConstants.Set.Collection.Groups.TakenMultipleTimes,
            FeatConstants.SpellMasteryId,
            FeatConstants.ToughnessId)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
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