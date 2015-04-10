using System;
using NPCGen.Common.Abilities.Feats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities.Feats
{
    [TestFixture]
    public class FeatConstantsTests
    {
        [TestCase(FeatConstants.AasimarDaylight, "Aasimar Special Attack: Daylight")]
        [TestCase(FeatConstants.AasimarDaylightId, "AasimarDaylight")]
        [TestCase(FeatConstants.Ambidexterity, "Ambidexterity")]
        [TestCase(FeatConstants.AmbidexterityId, "Ambidexterity")]
        [TestCase(FeatConstants.Darkvision, "Darkvision")]
        [TestCase(FeatConstants.DarkvisionId, "Darkvision")]
        [TestCase(FeatConstants.LightArmorProficiency, "Light Armor Proficiency")]
        [TestCase(FeatConstants.LightArmorProficiencyId, "LightArmorProficiency")]
        [TestCase(FeatConstants.ResistanceToAcid, "Resistance to Acid")]
        [TestCase(FeatConstants.ResistanceToAcidId, "ResistanceToAcid")]
        [TestCase(FeatConstants.ResistanceToCold, "Resistance to Cold")]
        [TestCase(FeatConstants.ResistanceToColdId, "ResistanceToCold")]
        [TestCase(FeatConstants.ResistanceToElectricity, "Resistance to Electricity")]
        [TestCase(FeatConstants.ResistanceToElectricityId, "ResistanceToElectricity")]
        [TestCase(FeatConstants.Scent, "Scent")]
        [TestCase(FeatConstants.ScentId, "Scent")]
        [TestCase(FeatConstants.ShieldProficiency, "Shield Proficiency")]
        [TestCase(FeatConstants.ShieldProficiencyId, "ShieldProficiency")]
        [TestCase(FeatConstants.SkillFocus, "Skill Focus")]
        [TestCase(FeatConstants.SkillFocusId, "SkillFocus")]
        [TestCase(FeatConstants.SpellMastery, "Spell Mastery")]
        [TestCase(FeatConstants.SpellMasteryId, "SpellMastery")]
        [TestCase(FeatConstants.Stability, "Stability")]
        [TestCase(FeatConstants.StabilityId, "Stability")]
        [TestCase(FeatConstants.Stonecunning, "Stonecunning")]
        [TestCase(FeatConstants.StonecunningId, "Stonecunning")]
        [TestCase(FeatConstants.WeaponFamiliarity, "Weapon Familiarity")]
        [TestCase(FeatConstants.WeaponFamiliarityId, "WeaponFamiliarity")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}