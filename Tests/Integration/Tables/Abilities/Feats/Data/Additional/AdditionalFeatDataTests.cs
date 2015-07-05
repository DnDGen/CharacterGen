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

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.AcrobaticId,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.AgileId,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.AlertnessId,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.AnimalAffinityId,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.HeavyArmorProficiencyId,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.MediumArmorProficiencyId,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.LightArmorProficiencyId,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.AthleticId,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.AugmentSummoningId,
            0,
            "",
            0,
            "",
            4)]
        public void Data(String name, Int32 baseAttackRequirement, String focusType, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 strength)
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