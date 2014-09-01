using System;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities.Feats
{
    [TestFixture]
    public class FeatConstantsTests
    {
        [TestCase(FeatConstants.Ambidexterity, "Ambidexterity")]
        [TestCase(FeatConstants.LightArmorProficiency, "Light Armor Proficiency")]
        [TestCase(FeatConstants.ShieldProficiency, "Shield Proficiency")]
        [TestCase(FeatConstants.SkillFocus, "Skill Focus")]
        [TestCase(FeatConstants.SpellMastery, "Spell Mastery")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void AllFeats()
        {
            var feats = FeatConstants.GetAllFeats();

            Assert.That(feats, Contains.Item(FeatConstants.Ambidexterity));
            Assert.That(feats, Contains.Item(FeatConstants.LightArmorProficiency));
            Assert.That(feats, Contains.Item(FeatConstants.ShieldProficiency));
            Assert.That(feats, Contains.Item(FeatConstants.SkillFocus));
            Assert.That(feats, Contains.Item(FeatConstants.SpellMastery));
            Assert.That(feats.Count(), Is.EqualTo(5));
        }
    }
}