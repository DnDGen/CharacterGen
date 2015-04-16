﻿using System;
using NPCGen.Common.Abilities.Feats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities.Feats
{
    [TestFixture]
    public class FeatConstantsTests
    {
        [TestCase(FeatConstants.Acrobatic, "Acrobatic")]
        [TestCase(FeatConstants.AcrobaticId, "Acrobatic")]
        [TestCase(FeatConstants.Agile, "Agile")]
        [TestCase(FeatConstants.AgileId, "Agile")]
        [TestCase(FeatConstants.Alertness, "Alertness")]
        [TestCase(FeatConstants.AlertnessId, "Alertness")]
        [TestCase(FeatConstants.AlternateForm, "Alternate Form")]
        [TestCase(FeatConstants.AlternateFormId, "AlternateForm")]
        [TestCase(FeatConstants.Ambidexterity, "Ambidexterity")]
        [TestCase(FeatConstants.AmbidexterityId, "Ambidexterity")]
        [TestCase(FeatConstants.AnimalAffinity, "Animal Affinity")]
        [TestCase(FeatConstants.AnimalAffinityId, "AnimalAffinity")]
        [TestCase(FeatConstants.Athletic, "Athletic")]
        [TestCase(FeatConstants.AthleticId, "Athletic")]
        [TestCase(FeatConstants.AttackBonus, "Attack Bonus")]
        [TestCase(FeatConstants.AttackBonusId, "AttackBonus")]
        [TestCase(FeatConstants.AugmentSummoning, "Augment Summoning")]
        [TestCase(FeatConstants.AugmentSummoningId, "AugmentSummoning")]
        [TestCase(FeatConstants.BlindFight, "Blind-Fight")]
        [TestCase(FeatConstants.BlindFightId, "BlindFight")]
        [TestCase(FeatConstants.ChangeShape, "Change Shape")]
        [TestCase(FeatConstants.ChangeShapeId, "ChangeShape")]
        [TestCase(FeatConstants.Cleave, "Cleave")]
        [TestCase(FeatConstants.CleaveId, "Cleave")]
        [TestCase(FeatConstants.CombatCasting, "Combat Casting")]
        [TestCase(FeatConstants.CombatCastingId, "CombatCasting")]
        [TestCase(FeatConstants.CombatExpertise, "Combat Expertise")]
        [TestCase(FeatConstants.CombatExpertiseId, "CombatExpertise")]
        [TestCase(FeatConstants.CombatReflexes, "Combat Reflexes")]
        [TestCase(FeatConstants.CombatReflexesId, "CombatReflexes")]
        [TestCase(FeatConstants.DamageReduction, "Damage Reduction")]
        [TestCase(FeatConstants.DamageReductionId, "DamageReduction")]
        [TestCase(FeatConstants.Darkvision, "Darkvision")]
        [TestCase(FeatConstants.DarkvisionId, "Darkvision")]
        [TestCase(FeatConstants.Deceitful, "Deceitful")]
        [TestCase(FeatConstants.DeceitfulId, "Deceitful")]
        [TestCase(FeatConstants.DeflectArrows, "Deflect Arrows")]
        [TestCase(FeatConstants.DeflectArrowsId, "DeflectArrows")]
        [TestCase(FeatConstants.DeftHands, "Deft Hands")]
        [TestCase(FeatConstants.DeftHandsId, "DeftHands")]
        [TestCase(FeatConstants.Diehard, "Diehard")]
        [TestCase(FeatConstants.DiehardId, "Diehard")]
        [TestCase(FeatConstants.Diligent, "Diligent")]
        [TestCase(FeatConstants.DiligentId, "Diligent")]
        [TestCase(FeatConstants.Dodge, "Dodge")]
        [TestCase(FeatConstants.DodgeId, "Dodge")]
        [TestCase(FeatConstants.DodgeBonus, "Dodge Bonus")]
        [TestCase(FeatConstants.DodgeBonusId, "DodgeBonus")]
        [TestCase(FeatConstants.ElvenBlood, "Elven Blood")]
        [TestCase(FeatConstants.ElvenBloodId, "ElvenBlood")]
        [TestCase(FeatConstants.Empathy, "Empathy")]
        [TestCase(FeatConstants.EmpathyId, "Empathy")]
        [TestCase(FeatConstants.EmpowerSpell, "Empower Spell")]
        [TestCase(FeatConstants.EmpowerSpellId, "EmpowerSpell")]
        [TestCase(FeatConstants.Endurance, "Endurance")]
        [TestCase(FeatConstants.EnduranceId, "Endurance")]
        [TestCase(FeatConstants.EnlargeSpell, "Enlarge Spell")]
        [TestCase(FeatConstants.EnlargeSpellId, "EnlargeSpell")]
        [TestCase(FeatConstants.EschewMaterials, "Eschew Materials")]
        [TestCase(FeatConstants.EschewMaterialsId, "EschewMaterials")]
        [TestCase(FeatConstants.ExoticWeaponProficiency, "Exotic Weapon Proficiency")]
        [TestCase(FeatConstants.ExoticWeaponProficiencyId, "ExoticWeaponProficiency")]
        [TestCase(FeatConstants.ExtendSpell, "Extend Spell")]
        [TestCase(FeatConstants.ExtendSpellId, "ExtendSpell")]
        [TestCase(FeatConstants.ExtraTurning, "Extra Turning")]
        [TestCase(FeatConstants.ExtraTurningId, "ExtraTurning")]
        [TestCase(FeatConstants.FarShot, "Far Shot")]
        [TestCase(FeatConstants.FarShotId, "FarShot")]
        [TestCase(FeatConstants.Ferocity, "Ferocity")]
        [TestCase(FeatConstants.FerocityId, "Ferocity")]
        [TestCase(FeatConstants.GreatCleave, "Great Cleave")]
        [TestCase(FeatConstants.GreatCleaveId, "GreatCleave")]
        [TestCase(FeatConstants.GreaterSpellFocus, "WeaponFamiliarity")]
        [TestCase(FeatConstants.GreaterSpellFocusId, "WeaponFamiliarity")]
        [TestCase(FeatConstants.GreaterSpellPenetration, "Greater Spell Penetration")]
        [TestCase(FeatConstants.GreaterSpellPenetrationId, "GreaterSpellPenetration")]
        [TestCase(FeatConstants.GreaterTwoWeaponFighting, "Greater Two-Weapon Fighting")]
        [TestCase(FeatConstants.GreaterTwoWeaponFightingId, "GreaterTwoWeaponFighting")]
        [TestCase(FeatConstants.GreaterWeaponFocus, "Greater Weapon Focus")]
        [TestCase(FeatConstants.GreaterWeaponFocusId, "GreaterWeaponFocus")]
        [TestCase(FeatConstants.GreaterWeaponSpecialization, "Greater Weapon Specialization")]
        [TestCase(FeatConstants.GreaterWeaponSpecializationId, "GreaterWeaponSpecialization")]
        [TestCase(FeatConstants.GreatFortitude, "Great Fortitude")]
        [TestCase(FeatConstants.GreatFortitudeId, "GreatFortitude")]
        [TestCase(FeatConstants.HeavyArmorProficiency, "Heavy Armor Proficiency")]
        [TestCase(FeatConstants.HeavyArmorProficiencyId, "HeavyArmorProficiency")]
        [TestCase(FeatConstants.HeightenSpell, "Heighten Spell")]
        [TestCase(FeatConstants.HeightenSpellId, "HeightenSpell")]
        [TestCase(FeatConstants.HoldBreath, "Hold Breath")]
        [TestCase(FeatConstants.HoldBreathId, "HoldBreath")]
        [TestCase(FeatConstants.ImmuneToEffect, "Immune to Effect")]
        [TestCase(FeatConstants.ImmuneToEffectId, "ImmuneToEffect")]
        [TestCase(FeatConstants.ImprovedBullRush, "Improved Bull Rush")]
        [TestCase(FeatConstants.ImprovedBullRushId, "ImprovedBullRush")]
        [TestCase(FeatConstants.ImprovedCounterspell, "Improved Counterspell")]
        [TestCase(FeatConstants.ImprovedCounterspellId, "ImprovedCounterspell")]
        [TestCase(FeatConstants.ImprovedCritical, "Improved Critical")]
        [TestCase(FeatConstants.ImprovedCriticalId, "ImprovedCritical")]
        [TestCase(FeatConstants.ImprovedDisarm, "Improved Disarm")]
        [TestCase(FeatConstants.ImprovedDisarmId, "ImprovedDisarm")]
        [TestCase(FeatConstants.ImprovedFamiliar, "Improved Familiar")]
        [TestCase(FeatConstants.ImprovedFamiliarId, "ImprovedFamiliar")]
        [TestCase(FeatConstants.ImprovedFeint, "Improved Feint")]
        [TestCase(FeatConstants.ImprovedFeintId, "ImprovedFeint")]
        [TestCase(FeatConstants.ImprovedGrab, "Improved Grab")]
        [TestCase(FeatConstants.ImprovedGrabId, "ImprovedGrab")]
        [TestCase(FeatConstants.ImprovedGrapple, "Improved Grapple")]
        [TestCase(FeatConstants.ImprovedGrappleId, "ImprovedGrapple")]
        [TestCase(FeatConstants.ImprovedInitiative, "Improved Initiative")]
        [TestCase(FeatConstants.ImprovedInitiativeId, "ImprovedInitiative")]
        [TestCase(FeatConstants.ImprovedOverrun, "Improved Overrun")]
        [TestCase(FeatConstants.ImprovedOverrunId, "ImprovedOverrun")]
        [TestCase(FeatConstants.ImprovedPreciseShot, "Improved Precise Shot")]
        [TestCase(FeatConstants.ImprovedPreciseShotId, "ImprovedPreciseShot")]
        [TestCase(FeatConstants.ImprovedShieldBash, "Improved Shield Bash")]
        [TestCase(FeatConstants.ImprovedShieldBashId, "ImprovedShieldBash")]
        [TestCase(FeatConstants.ImprovedSunder, "Improved Sunder")]
        [TestCase(FeatConstants.ImprovedSunderId, "ImprovedSunder")]
        [TestCase(FeatConstants.ImprovedTrip, "Improved Trip")]
        [TestCase(FeatConstants.ImprovedTripId, "ImprovedTrip")]
        [TestCase(FeatConstants.ImprovedTurning, "Improved Turning")]
        [TestCase(FeatConstants.ImprovedTurningId, "ImprovedTurning")]
        [TestCase(FeatConstants.ImprovedTwoWeaponFighting, "Improved Two-Weapon Fighting")]
        [TestCase(FeatConstants.ImprovedTwoWeaponFightingId, "ImprovedTwoWeaponFighting")]
        [TestCase(FeatConstants.ImprovedUnarmedStrike, "Improved Unarmed Strike")]
        [TestCase(FeatConstants.ImprovedUnarmedStrikeId, "ImprovedUnarmedStrike")]
        [TestCase(FeatConstants.Investigator, "Investigator")]
        [TestCase(FeatConstants.InvestigatorId, "Investigator")]
        [TestCase(FeatConstants.IronWill, "Iron Will")]
        [TestCase(FeatConstants.IronWillId, "IronWill")]
        [TestCase(FeatConstants.Leadership, "Leadership")]
        [TestCase(FeatConstants.LeadershipId, "Leadership")]
        [TestCase(FeatConstants.LightArmorProficiency, "Light Armor Proficiency")]
        [TestCase(FeatConstants.LightArmorProficiencyId, "LightArmorProficiency")]
        [TestCase(FeatConstants.LightningReflexes, "Lightning Reflexes")]
        [TestCase(FeatConstants.LightningReflexesId, "LightningReflexes")]
        [TestCase(FeatConstants.LightSensitivity, "Light Sensitivity")]
        [TestCase(FeatConstants.LightSensitivityId, "LightSensitivity")]
        [TestCase(FeatConstants.LowLightVision, "Low-Light Vision")]
        [TestCase(FeatConstants.LowLightVisionId, "LowLightVision")]
        [TestCase(FeatConstants.Lycanthropy, "Lycanthropy")]
        [TestCase(FeatConstants.LycanthropyId, "Lycanthropy")]
        [TestCase(FeatConstants.Madness, "Madness")]
        [TestCase(FeatConstants.MadnessId, "Madness")]
        [TestCase(FeatConstants.MagicalAptitude, "Magical Aptitude")]
        [TestCase(FeatConstants.MagicalAptitudeId, "MagicalAptitude")]
        [TestCase(FeatConstants.MagicNaturalWeapons, "Magic Natural Weapons")]
        [TestCase(FeatConstants.MagicNaturalWeaponsId, "MagicNaturalWeapons")]
        [TestCase(FeatConstants.Manyshot, "Manyshot")]
        [TestCase(FeatConstants.ManyshotId, "Manyshot")]
        [TestCase(FeatConstants.MartialWeaponProficiency, "Martial Weapon Proficiency")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId, "MartialWeaponProficiency")]
        [TestCase(FeatConstants.MaximizeSpell, "Maximize Spell")]
        [TestCase(FeatConstants.MaximizeSpellId, "MaximizeSpell")]
        [TestCase(FeatConstants.MediumArmorProficiency, "Medium Armor Proficiency")]
        [TestCase(FeatConstants.MediumArmorProficiencyId, "MediumArmorProficiency")]
        [TestCase(FeatConstants.Mobility, "Mobility")]
        [TestCase(FeatConstants.MobilityId, "Mobility")]
        [TestCase(FeatConstants.MountedArchery, "Mounted Archery")]
        [TestCase(FeatConstants.MountedArcheryId, "MountedArchery")]
        [TestCase(FeatConstants.MountedCombat, "Mounted Combat")]
        [TestCase(FeatConstants.MountedCombatId, "MountedCombat")]
        [TestCase(FeatConstants.NaturalArmor, "Natural Armor")]
        [TestCase(FeatConstants.NaturalArmorId, "NaturalArmor")]
        [TestCase(FeatConstants.NaturalSpell, "Natural Spell")]
        [TestCase(FeatConstants.NaturalSpellId, "NaturalSpell")]
        [TestCase(FeatConstants.NaturalWeapon, "Natural Weapon")]
        [TestCase(FeatConstants.NaturalWeaponId, "NaturalWeapon")]
        [TestCase(FeatConstants.Negotiator, "Negotiator")]
        [TestCase(FeatConstants.NegotiatorId, "Negotiator")]
        [TestCase(FeatConstants.NimbleFingers, "Nimble Fingers")]
        [TestCase(FeatConstants.NimbleFingersId, "NimbleFingers")]
        [TestCase(FeatConstants.OrcBlood, "Orc Blood")]
        [TestCase(FeatConstants.OrcBloodId, "OrcBlood")]
        [TestCase(FeatConstants.PassWithoutTrace, "Pass Without Trace")]
        [TestCase(FeatConstants.PassWithoutTraceId, "PassWithoutTrace")]
        [TestCase(FeatConstants.Persuasive, "Persuasive")]
        [TestCase(FeatConstants.PersuasiveId, "Persuasive")]
        [TestCase(FeatConstants.PointBlankShot, "Point Blank Shot")]
        [TestCase(FeatConstants.PointBlankShotId, "PointBlankShot")]
        [TestCase(FeatConstants.PoisonUse, "Poison Use")]
        [TestCase(FeatConstants.PoisonUseId, "PoisonUse")]
        [TestCase(FeatConstants.PowerAttack, "Power Attack")]
        [TestCase(FeatConstants.PowerAttackId, "PowerAttack")]
        [TestCase(FeatConstants.PreciseShot, "Precise Shot")]
        [TestCase(FeatConstants.PreciseShotId, "PreciseShot")]
        [TestCase(FeatConstants.QuickDraw, "Quick Draw")]
        [TestCase(FeatConstants.QuickDrawId, "QuickDraw")]
        [TestCase(FeatConstants.QuickenSpell, "Quicken Spell")]
        [TestCase(FeatConstants.QuickenSpellId, "QuickenSpell")]
        [TestCase(FeatConstants.RapidReload, "Rapid Reload")]
        [TestCase(FeatConstants.RapidReloadId, "RapidReload")]
        [TestCase(FeatConstants.RapidShot, "Rapid Shot")]
        [TestCase(FeatConstants.RapidShotId, "RapidShot")]
        [TestCase(FeatConstants.ResistanceToAcid, "Resistance to Acid")]
        [TestCase(FeatConstants.ResistanceToAcidId, "ResistanceToAcid")]
        [TestCase(FeatConstants.ResistanceToCold, "Resistance to Cold")]
        [TestCase(FeatConstants.ResistanceToColdId, "ResistanceToCold")]
        [TestCase(FeatConstants.ResistanceToElectricity, "Resistance to Electricity")]
        [TestCase(FeatConstants.ResistanceToElectricityId, "ResistanceToElectricity")]
        [TestCase(FeatConstants.RideByAttack, "Ride-By Attack")]
        [TestCase(FeatConstants.RideByAttackId, "RideByAttack")]
        [TestCase(FeatConstants.Run, "Run")]
        [TestCase(FeatConstants.RunId, "Run")]
        [TestCase(FeatConstants.SaveBonus, "Save Bonus")]
        [TestCase(FeatConstants.SaveBonusId, "SaveBonus")]
        [TestCase(FeatConstants.Scent, "Scent")]
        [TestCase(FeatConstants.ScentId, "Scent")]
        [TestCase(FeatConstants.SelfSufficient, "Self-Sufficient")]
        [TestCase(FeatConstants.SelfSufficientId, "SelfSufficient")]
        [TestCase(FeatConstants.ShieldProficiency, "Shield Proficiency")]
        [TestCase(FeatConstants.ShieldProficiencyId, "ShieldProficiency")]
        [TestCase(FeatConstants.ShotOnTheRun, "Shot On The Run")]
        [TestCase(FeatConstants.ShotOnTheRunId, "ShotOnTheRun")]
        [TestCase(FeatConstants.SilentSpell, "Silent Spell")]
        [TestCase(FeatConstants.SilentSpellId, "SilentSpell")]
        [TestCase(FeatConstants.SimpleWeaponProficiency, "Simple Weapon Proficiency")]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId, "SimpleWeaponProficiency")]
        [TestCase(FeatConstants.SkillBonus, "Skill Bonus")]
        [TestCase(FeatConstants.SkillBonusId, "SkillBonus")]
        [TestCase(FeatConstants.SkillFocus, "Skill Focus")]
        [TestCase(FeatConstants.SkillFocusId, "SkillFocus")]
        [TestCase(FeatConstants.SmiteEvil, "Smite Evil")]
        [TestCase(FeatConstants.SmiteEvilId, "SmiteEvil")]
        [TestCase(FeatConstants.SmiteGood, "Smite Good")]
        [TestCase(FeatConstants.SmiteGoodId, "SmiteGood")]
        [TestCase(FeatConstants.SnatchArrows, "Snatch Arrows")]
        [TestCase(FeatConstants.SnatchArrowsId, "SnatchArrows")]
        [TestCase(FeatConstants.SneakAttack, "Sneak Attack")]
        [TestCase(FeatConstants.SneakAttackId, "SneakAttack")]
        [TestCase(FeatConstants.SpellFocus, "Spell Focus")]
        [TestCase(FeatConstants.SpellFocusId, "SpellFocus")]
        [TestCase(FeatConstants.SpellLikeAbility, "Spell-Like Ability")]
        [TestCase(FeatConstants.SpellLikeAbilityId, "SpellLikeAbility")]
        [TestCase(FeatConstants.SpellMastery, "Spell Mastery")]
        [TestCase(FeatConstants.SpellMasteryId, "SpellMastery")]
        [TestCase(FeatConstants.SpellPenetration, "Spell Penetration")]
        [TestCase(FeatConstants.SpellPenetrationId, "SpellPenetration")]
        [TestCase(FeatConstants.SpellResistance, "Spell Resistance")]
        [TestCase(FeatConstants.SpellResistanceId, "SpellResistance")]
        [TestCase(FeatConstants.SpellStrength, "Spell Strength")]
        [TestCase(FeatConstants.SpellStrengthId, "SpellStrength")]
        [TestCase(FeatConstants.SpiritedCharge, "Spirited Charge")]
        [TestCase(FeatConstants.SpiritedChargeId, "SpiritedCharge")]
        [TestCase(FeatConstants.SpringAttack, "Spring Attack")]
        [TestCase(FeatConstants.SpringAttackId, "SpringAttack")]
        [TestCase(FeatConstants.Stability, "Stability")]
        [TestCase(FeatConstants.StabilityId, "Stability")]
        [TestCase(FeatConstants.Stealthy, "Stealthy")]
        [TestCase(FeatConstants.StealthyId, "Stealthy")]
        [TestCase(FeatConstants.StillSpell, "Still Spell")]
        [TestCase(FeatConstants.StillSpellId, "StillSpell")]
        [TestCase(FeatConstants.Stonecunning, "Stonecunning")]
        [TestCase(FeatConstants.StonecunningId, "Stonecunning")]
        [TestCase(FeatConstants.StunningFist, "Stunning Fist")]
        [TestCase(FeatConstants.StunningFistId, "StunningFist")]
        [TestCase(FeatConstants.Toughness, "Toughness")]
        [TestCase(FeatConstants.ToughnessId, "Toughness")]
        [TestCase(FeatConstants.TowerShieldProficiency, "Tower Shield Proficiency")]
        [TestCase(FeatConstants.TowerShieldProficiencyId, "TowerShieldProficiency")]
        [TestCase(FeatConstants.Track, "Track")]
        [TestCase(FeatConstants.TrackId, "Track")]
        [TestCase(FeatConstants.Trample, "Trample")]
        [TestCase(FeatConstants.TrampleId, "Trample")]
        [TestCase(FeatConstants.TwoWeaponFighting, "Two-Weapon Fighting")]
        [TestCase(FeatConstants.TwoWeaponFightingId, "TwoWeaponFighting")]
        [TestCase(FeatConstants.TwoWeaponDefense, "Two-Weapon Defense")]
        [TestCase(FeatConstants.TwoWeaponDefenseId, "TwoWeaponDefense")]
        [TestCase(FeatConstants.VulnerabilityToSunlight, "Vulnerability to Sunlight")]
        [TestCase(FeatConstants.VulnerabilityToSunlightId, "VulnerabilityToSunlight")]
        [TestCase(FeatConstants.WeaponFamiliarity, "Weapon Familiarity")]
        [TestCase(FeatConstants.WeaponFamiliarityId, "WeaponFamiliarity")]
        [TestCase(FeatConstants.WeaponFinesse, "Weapon Finesse")]
        [TestCase(FeatConstants.WeaponFinesseId, "WeaponFinesse")]
        [TestCase(FeatConstants.WeaponFocus, "Weapon Focus")]
        [TestCase(FeatConstants.WeaponFocusId, "WeaponFocus")]
        [TestCase(FeatConstants.WeaponProficiency, "Weapon Proficiency")]
        [TestCase(FeatConstants.WeaponProficiencyId, "WeaponProficiency")]
        [TestCase(FeatConstants.WeaponSpecialization, "Weapon Specialization")]
        [TestCase(FeatConstants.WeaponSpecializationId, "WeaponSpecialization")]
        [TestCase(FeatConstants.WidenSpell, "Widen Spell")]
        [TestCase(FeatConstants.WidenSpellId, "WidenSpell")]
        [TestCase(FeatConstants.WhirlwindAttack, "Whirlwind Attack")]
        [TestCase(FeatConstants.WhirlwindAttackId, "WhirlwindAttack")]
        [TestCase(FeatConstants.Frequencies.Constant, "Constant")]
        [TestCase(FeatConstants.Frequencies.AtWill, "At Will")]
        [TestCase(FeatConstants.Frequencies.Day, "Day")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}