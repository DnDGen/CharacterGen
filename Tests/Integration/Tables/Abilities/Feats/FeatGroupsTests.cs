﻿using System;
using NPCGen.Common.Abilities.Feats;
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

        [TestCase(TableNameConstants.Set.Collection.Groups.Additional,
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
            FeatConstants.DeceitfulId,
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
            FeatConstants.WidenSpellId)]
        [TestCase(TableNameConstants.Set.Collection.Groups.OverwrittenStrengths,
            FeatConstants.DarkvisionId)]
        [TestCase(TableNameConstants.Set.Collection.Groups.CumulativeStrengths,
            FeatConstants.SpellLikeAbilityId)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Racial,
            FeatConstants.DarkvisionId,
            FeatConstants.SpellLikeAbilityId,
            FeatConstants.SneakAttackId,
            FeatConstants.ScentId)]
        [TestCase(TableNameConstants.Set.Collection.Groups.CharacterClasses)]
        [TestCase(TableNameConstants.Set.Collection.Groups.NaturalArmor)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Deflection)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Dodge)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}