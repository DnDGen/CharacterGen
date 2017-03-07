using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class LanguageGeneratorTests
    {
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<ILanguageCollectionsSelector> mockLanguageSelector;
        private ILanguageGenerator languageGenerator;
        private Race race;
        private string className;
        private List<Skill> skills;
        private List<string> automaticLanguages;
        private List<string> bonusLanguages;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockLanguageSelector = new Mock<ILanguageCollectionsSelector>();
            languageGenerator = new LanguageGenerator(mockLanguageSelector.Object, mockCollectionsSelector.Object);
            race = new Race();
            skills = new List<Skill>();
            automaticLanguages = new List<string>();
            bonusLanguages = new List<string>();

            race.BaseRace = "baserace";
            race.Metarace = "metarace";
            className = "class name";
            AddSkill("skill 1");
            AddSkill("skill 2");
            automaticLanguages.Add("lang 1");
            automaticLanguages.Add("lang 2");
            bonusLanguages.Add("lang 1");
            bonusLanguages.Add("lang 2");
            bonusLanguages.Add("lang 3");

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
            mockLanguageSelector.Setup(s => s.SelectAutomaticLanguagesFor(race, className)).Returns(automaticLanguages);
            mockLanguageSelector.Setup(s => s.SelectBonusLanguagesFor(race.BaseRace, className)).Returns(bonusLanguages);
        }

        private void AddSkill(string skillName, string focus = "")
        {
            var stat = new Stat("base state");
            var skill = new Skill(skillName, stat, 0, focus);

            skills.Add(skill);
        }

        [Test]
        public void GetAutomaticLanguagesFromSelector()
        {
            bonusLanguages.Clear();

            var languages = languageGenerator.GenerateWith(race, className, 0, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetNumberOfBonusLanguagesEqualToIntelligenceModifier()
        {
            automaticLanguages.Clear();

            var languages = languageGenerator.GenerateWith(race, className, 2, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllBonusLanguagesIfIntelligenceBonusIsHigher()
        {
            automaticLanguages.Clear();

            var languages = languageGenerator.GenerateWith(race, className, 9266, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void LanguagesContainAutomaticLanguagesAndBonusLanguages()
        {
            automaticLanguages.Clear();
            bonusLanguages.Clear();

            automaticLanguages.Add("automatic language");
            bonusLanguages.Add("bonus language");

            var languages = languageGenerator.GenerateWith(race, className, 1, skills);
            Assert.That(languages, Contains.Item("automatic language"));
            Assert.That(languages, Contains.Item("bonus language"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void LanguagesAreNotDuplicated()
        {
            var languages = languageGenerator.GenerateWith(race, className, 2, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void InterpreterGainsExtraLanguage()
        {
            automaticLanguages.Clear();

            AddSkill(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter);

            var languages = languageGenerator.GenerateWith(race, className, 1, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void InterpreterGainsExtraLanguageEvenIfIntelligenceBonusIs0()
        {
            automaticLanguages.Clear();

            AddSkill(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter);

            var languages = languageGenerator.GenerateWith(race, className, 0, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void InterpreterGainsExtraLanguageEvenIfIntelligenceBonusIsNegative()
        {
            automaticLanguages.Clear();

            AddSkill(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter);

            var languages = languageGenerator.GenerateWith(race, className, -1, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void NonInterpreterGainsNoExtraLanguage()
        {
            automaticLanguages.Clear();

            AddSkill(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser);

            var languages = languageGenerator.GenerateWith(race, className, 1, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void NoProfessionGainsNoExtraLanguage()
        {
            automaticLanguages.Clear();

            AddSkill("other skill", SkillConstants.Foci.Profession.Adviser);

            var languages = languageGenerator.GenerateWith(race, className, 1, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }
    }
}