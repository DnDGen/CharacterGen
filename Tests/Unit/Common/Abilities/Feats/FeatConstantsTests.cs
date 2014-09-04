using System;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities.Feats
{
    [TestFixture]
    public class FeatConstantsTests
    {
        [TestCase(FeatConstants.AasimarDaylight, "Aasimar Special Attack: Daylight")]
        [TestCase(FeatConstants.Ambidexterity, "Ambidexterity")]
        [TestCase(FeatConstants.Darkvision, "Darkvision")]
        [TestCase(FeatConstants.LightArmorProficiency, "Light Armor Proficiency")]
        [TestCase(FeatConstants.ResistanceToAcid, "Resistance to Acid")]
        [TestCase(FeatConstants.ResistanceToCold, "Resistance to Cold")]
        [TestCase(FeatConstants.ResistanceToElectricity, "Resistance to Electricity")]
        [TestCase(FeatConstants.Scent, "Scent")]
        [TestCase(FeatConstants.ShieldProficiency, "Shield Proficiency")]
        [TestCase(FeatConstants.SkillFocus, "Skill Focus")]
        [TestCase(FeatConstants.SpellMastery, "Spell Mastery")]
        [TestCase(FeatConstants.Stability, "Stability")]
        [TestCase(FeatConstants.Stonecunning, "Stonecunning")]
        [TestCase(FeatConstants.WeaponFamiliarity, "Weapon Familiarity")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void AllFeats()
        {
            var feats = FeatConstants.GetAllFeats();

            Assert.That(feats, Contains.Item(FeatConstants.AasimarDaylight));
            Assert.That(feats, Contains.Item(FeatConstants.Ambidexterity));
            Assert.That(feats, Contains.Item(FeatConstants.Darkvision));
            Assert.That(feats, Contains.Item(FeatConstants.LightArmorProficiency));
            Assert.That(feats, Contains.Item(FeatConstants.ResistanceToAcid));
            Assert.That(feats, Contains.Item(FeatConstants.ResistanceToCold));
            Assert.That(feats, Contains.Item(FeatConstants.ResistanceToElectricity));
            Assert.That(feats, Contains.Item(FeatConstants.Scent));
            Assert.That(feats, Contains.Item(FeatConstants.ShieldProficiency));
            Assert.That(feats, Contains.Item(FeatConstants.SkillFocus));
            Assert.That(feats, Contains.Item(FeatConstants.SpellMastery));
            Assert.That(feats, Contains.Item(FeatConstants.Stability));
            Assert.That(feats, Contains.Item(FeatConstants.Stonecunning));
            Assert.That(feats, Contains.Item(FeatConstants.WeaponFamiliarity));
            Assert.That(feats.Count(), Is.EqualTo(14));
        }
    }
}