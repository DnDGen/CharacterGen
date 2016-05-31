using System;
using System.Linq;
using CharacterGen.Abilities;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Abilities
{
    [TestFixture]
    public class LanguageConstantsTests
    {
        [TestCase(LanguageConstants.Abyssal, "Abyssal")]
        [TestCase(LanguageConstants.Aquan, "Aquan")]
        [TestCase(LanguageConstants.Auran, "Auran")]
        [TestCase(LanguageConstants.Celestial, "Celestial")]
        [TestCase(LanguageConstants.Common, "Common")]
        [TestCase(LanguageConstants.Draconic, "Draconic")]
        [TestCase(LanguageConstants.Druidic, "Druidic")]
        [TestCase(LanguageConstants.Dwarven, "Dwarven")]
        [TestCase(LanguageConstants.Elven, "Elven")]
        [TestCase(LanguageConstants.Giant, "Giant")]
        [TestCase(LanguageConstants.Gnoll, "Gnoll")]
        [TestCase(LanguageConstants.Gnome, "Gnome")]
        [TestCase(LanguageConstants.Goblin, "Goblin")]
        [TestCase(LanguageConstants.Halfling, "Halfling")]
        [TestCase(LanguageConstants.Ignan, "Ignan")]
        [TestCase(LanguageConstants.Infernal, "Infernal")]
        [TestCase(LanguageConstants.Orc, "Orc")]
        [TestCase(LanguageConstants.Sylvan, "Sylvan")]
        [TestCase(LanguageConstants.Terran, "Terran")]
        [TestCase(LanguageConstants.Undercommon, "Undercommon")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void AllLanguages()
        {
            var languages = LanguageConstants.GetLanguages();

            Assert.That(languages, Contains.Item(LanguageConstants.Abyssal));
            Assert.That(languages, Contains.Item(LanguageConstants.Aquan));
            Assert.That(languages, Contains.Item(LanguageConstants.Auran));
            Assert.That(languages, Contains.Item(LanguageConstants.Celestial));
            Assert.That(languages, Contains.Item(LanguageConstants.Common));
            Assert.That(languages, Contains.Item(LanguageConstants.Draconic));
            Assert.That(languages, Contains.Item(LanguageConstants.Druidic));
            Assert.That(languages, Contains.Item(LanguageConstants.Dwarven));
            Assert.That(languages, Contains.Item(LanguageConstants.Elven));
            Assert.That(languages, Contains.Item(LanguageConstants.Giant));
            Assert.That(languages, Contains.Item(LanguageConstants.Gnoll));
            Assert.That(languages, Contains.Item(LanguageConstants.Gnome));
            Assert.That(languages, Contains.Item(LanguageConstants.Goblin));
            Assert.That(languages, Contains.Item(LanguageConstants.Halfling));
            Assert.That(languages, Contains.Item(LanguageConstants.Ignan));
            Assert.That(languages, Contains.Item(LanguageConstants.Infernal));
            Assert.That(languages, Contains.Item(LanguageConstants.Orc));
            Assert.That(languages, Contains.Item(LanguageConstants.Sylvan));
            Assert.That(languages, Contains.Item(LanguageConstants.Terran));
            Assert.That(languages, Contains.Item(LanguageConstants.Undercommon));
            Assert.That(languages.Count(), Is.EqualTo(20));
        }
    }
}