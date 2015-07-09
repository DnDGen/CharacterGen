using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Additional
{
    [TestFixture]
    public class AdditionalFeatDataTests : DataTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.AdditionalFeatData; }
        }
        protected override void PopulateIndices(IEnumerable<String> collection)
        {
            indices[DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex] = "BaseAttackRequirement";
            indices[DataIndexConstants.AdditionalFeatData.FocusTypeIndex] = "FocusType";
            indices[DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex] = "FrequencyQuantity";
            indices[DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex] = "FrequencyTimePeriod";
            indices[DataIndexConstants.AdditionalFeatData.StrengthIndex] = "Strength";
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.AcrobaticId,
                FeatConstants.AgileId,
                FeatConstants.AlertnessId,
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

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.AcrobaticId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.AgileId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.AlertnessId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.AnimalAffinityId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.HeavyArmorProficiencyId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.MediumArmorProficiencyId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.LightArmorProficiencyId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.AthleticId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.AugmentSummoningId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.BlindFightId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.CleaveId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.CombatCastingId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.CombatExpertiseId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.CombatReflexesId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.CripplingStrikeId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.DeceitfulId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.DefensiveRollId, 0, "", 1, FeatConstants.Frequencies.Day, 0)]
        [TestCase(FeatConstants.DeflectArrowsId, 0, "", 1, FeatConstants.Frequencies.Round, 0)]
        [TestCase(FeatConstants.DeftHandsId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.DiehardId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.DiligentId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.DodgeId, 0, "", 0, "", 1)]
        [TestCase(FeatConstants.EmpowerSpellId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.EnduranceId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.EnlargeSpellId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.EschewMaterialsId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ExoticWeaponProficiencyId, 1, FeatConstants.ExoticWeaponProficiencyId, 0, "", 0)]
        [TestCase(FeatConstants.ExtendSpellId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ExtraTurningId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.FarShotId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.GreatCleaveId, 4, "", 0, "", 0)]
        [TestCase(FeatConstants.GreaterSpellFocusId, 0, GroupConstants.SchoolsOfMagic, 0, "", 1)]
        [TestCase(FeatConstants.GreaterSpellPenetrationId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.GreaterTwoWeaponFightingId, 11, "", 0, "", 0)]
        [TestCase(FeatConstants.GreaterWeaponFocusId, 0, GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, 0, "", 1)]
        [TestCase(FeatConstants.GreaterWeaponSpecializationId, 0, GroupConstants.WeaponsWithUnarmedAndGrapple, 0, "", 2)]
        [TestCase(FeatConstants.GreatFortitudeId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.HeightenSpellId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedBullRushId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedCounterspellId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedCriticalId, 8, GroupConstants.Proficiency, 0, "", 0)]
        [TestCase(FeatConstants.ImprovedDisarmId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedEvasionId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedFamiliarId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedFeintId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedGrappleId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedInitiativeId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedOverrunId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedPreciseShotId, 11, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedShieldBashId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedSunderId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedTripId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedTurningId, 0, "", 0, "", 4)]
        [TestCase(FeatConstants.ImprovedTwoWeaponFightingId, 6, "", 0, "", 0)]
        [TestCase(FeatConstants.ImprovedUnarmedStrikeId, 0, "", 0, "", 0)]
        [TestCase(FeatConstants.InvestigatorId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.IronWillId, 0, "", 0, "", 2)]
        [TestCase(FeatConstants.LeadershipId, 0, "", 0, "", 0)]
        public void AdditionalFeatData(String name, Int32 baseAttackRequirement, String focusType, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 strength)
        {
            var data = new List<String>();
            for (var i = 0; i < 5; i++)
                data.Add(String.Empty);

            data[DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex] = Convert.ToString(baseAttackRequirement);
            data[DataIndexConstants.AdditionalFeatData.FocusTypeIndex] = focusType;
            data[DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.AdditionalFeatData.StrengthIndex] = Convert.ToString(strength);

            base.Data(name, data);
        }
    }
}