using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats
{
    [TestFixture]
    public class RequiredFeatsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.RequiredFeats; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.AugmentSummoningId,
                FeatConstants.CleaveId,
                FeatConstants.CombatStyleMasteryId,
                FeatConstants.DeflectArrowsId,
                FeatConstants.DiehardId,
                FeatConstants.ExtraTurningId,
                FeatConstants.FarShotId,
                FeatConstants.HeavyArmorProficiencyId,
                FeatConstants.ImprovedCombatStyleId,
                FeatConstants.MediumArmorProficiencyId,
                FeatConstants.RapidShotId + "Ranger",
                FeatConstants.TwoWeaponFightingId + "Ranger",
                FeatConstants.ManyshotId + "Ranger",
                FeatConstants.ImprovedTwoWeaponFightingId + "Ranger",
                FeatConstants.ImprovedPreciseShotId + "Ranger",
                FeatConstants.GreaterTwoWeaponFightingId + "Ranger",
                FeatConstants.GreatCleaveId,
                FeatConstants.GreaterSpellFocusId,
                FeatConstants.GreaterSpellPenetrationId,
                FeatConstants.GreaterTwoWeaponFightingId,
                FeatConstants.GreaterWeaponFocusId,
                FeatConstants.GreaterWeaponSpecializationId,
                FeatConstants.ImprovedBullRushId,
                FeatConstants.ImprovedCriticalId,
                FeatConstants.ImprovedDisarmId,
                FeatConstants.ImprovedFeintId,
                FeatConstants.ImprovedGrappleId,
                FeatConstants.ImprovedOverrunId,
                FeatConstants.ImprovedPreciseShotId,
                FeatConstants.PreciseShotId,
                FeatConstants.ImprovedShieldBashId,
                FeatConstants.ImprovedSunderId,
                FeatConstants.ImprovedTripId,
                FeatConstants.ImprovedTurningId,
                FeatConstants.ImprovedTwoWeaponFightingId
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.CombatStyleMasteryId, FeatConstants.ImprovedCombatStyleId)]
        [TestCase(FeatConstants.ImprovedCombatStyleId, FeatConstants.CombatStyleId)]
        [TestCase(FeatConstants.HeavyArmorProficiencyId, FeatConstants.MediumArmorProficiencyId)]
        [TestCase(FeatConstants.MediumArmorProficiencyId, FeatConstants.LightArmorProficiencyId)]
        [TestCase(FeatConstants.CleaveId, FeatConstants.PowerAttackId)]
        [TestCase(FeatConstants.DeflectArrowsId, FeatConstants.ImprovedUnarmedStrikeId)]
        [TestCase(FeatConstants.DiehardId, FeatConstants.EnduranceId)]
        [TestCase(FeatConstants.ExtraTurningId, FeatConstants.TurnId)]
        [TestCase(FeatConstants.FarShotId, FeatConstants.PointBlankShotId)]
        [TestCase(FeatConstants.GreatCleaveId, FeatConstants.CleaveId)]
        [TestCase(FeatConstants.GreaterSpellFocusId, FeatConstants.SpellFocusId)]
        [TestCase(FeatConstants.GreaterSpellPenetrationId, FeatConstants.SpellPenetrationId)]
        [TestCase(FeatConstants.GreaterTwoWeaponFightingId, FeatConstants.ImprovedTwoWeaponFightingId)]
        [TestCase(FeatConstants.GreaterWeaponFocusId, FeatConstants.WeaponFocusId)]
        [TestCase(FeatConstants.GreaterWeaponSpecializationId, FeatConstants.GreaterWeaponFocusId, FeatConstants.WeaponSpecializationId)]
        [TestCase(FeatConstants.ImprovedBullRushId, FeatConstants.PowerAttackId)]
        [TestCase(FeatConstants.ImprovedCriticalId, GroupConstants.Proficiency)]
        [TestCase(FeatConstants.ImprovedDisarmId, FeatConstants.CombatExpertiseId)]
        [TestCase(FeatConstants.ImprovedFeintId, FeatConstants.CombatExpertiseId)]
        [TestCase(FeatConstants.ImprovedGrappleId, FeatConstants.ImprovedUnarmedStrikeId)]
        [TestCase(FeatConstants.ImprovedOverrunId, FeatConstants.PowerAttackId)]
        [TestCase(FeatConstants.ImprovedPreciseShotId, FeatConstants.PreciseShotId)]
        [TestCase(FeatConstants.PreciseShotId, FeatConstants.PointBlankShotId)]
        [TestCase(FeatConstants.ImprovedShieldBashId, FeatConstants.ShieldProficiencyId)]
        [TestCase(FeatConstants.ImprovedSunderId, FeatConstants.PowerAttackId)]
        [TestCase(FeatConstants.ImprovedTripId, FeatConstants.CombatExpertiseId)]
        [TestCase(FeatConstants.ImprovedTurningId, FeatConstants.TurnId)]
        [TestCase(FeatConstants.ImprovedTwoWeaponFightingId, FeatConstants.TwoWeaponFightingId)]
        public void RequiredFeats(String name, params String[] requiredFeatIds)
        {
            DistinctCollection(name, requiredFeatIds);
        }

        [TestCase(FeatConstants.AugmentSummoningId, FeatConstants.SpellFocusId, CharacterClassConstants.Schools.Conjuration)]
        [TestCase(FeatConstants.RapidShotId + "Ranger", FeatConstants.CombatStyleId, FeatConstants.Foci.Archery)]
        [TestCase(FeatConstants.TwoWeaponFightingId + "Ranger", FeatConstants.CombatStyleId, FeatConstants.TwoWeaponFightingId)]
        [TestCase(FeatConstants.ManyshotId + "Ranger", FeatConstants.ImprovedCombatStyleId, FeatConstants.Foci.Archery)]
        [TestCase(FeatConstants.ImprovedTwoWeaponFightingId + "Ranger", FeatConstants.ImprovedCombatStyleId, FeatConstants.TwoWeaponFightingId)]
        [TestCase(FeatConstants.ImprovedPreciseShotId + "Ranger", FeatConstants.CombatStyleMasteryId, FeatConstants.Foci.Archery)]
        [TestCase(FeatConstants.GreaterTwoWeaponFightingId + "Ranger", FeatConstants.CombatStyleMasteryId, FeatConstants.TwoWeaponFightingId)]
        public void RequiredFeat(String name, String requiredFeatId, String requiredFocus)
        {
            var collection = new[] { String.Format("{0}/{1}", requiredFeatId, requiredFocus) };
            DistinctCollection(name, collection);
        }
    }
}
