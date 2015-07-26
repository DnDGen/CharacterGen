using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Domain.Abilities;
using CharacterGen.Selectors;
using Moq;
using NUnit.Framework;
using System;
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
        private String className;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockLanguageSelector = new Mock<ILanguageCollectionsSelector>();
            languageGenerator = new LanguageGenerator(mockLanguageSelector.Object, mockCollectionsSelector.Object);
            race = new Race();

            race.BaseRace = "baserace";
            race.Metarace = "metarace";
            className = "class name";
        }

        [Test]
        public void GetAutomaticLanguagesFromSelector()
        {
            var automaticLanguages = new[] { "lang 1", "lang 2" };
            mockLanguageSelector.Setup(s => s.SelectAutomaticLanguagesFor(race, className)).Returns(automaticLanguages);

            var languages = languageGenerator.GenerateWith(race, className, 0);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetNumberOfBonusLanguagesEqualToIntelligenceModifier()
        {
            var bonusLanguages = new[] { "lang 1", "lang 2", "lang 3" };
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, className)).Returns(bonusLanguages);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(bonusLanguages)).Returns("lang 1");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<String>>(ls => ls.Count() == 2 && ls.Contains("lang 1") == false)))
                .Returns("lang 3");

            var languages = languageGenerator.GenerateWith(race, className, 2);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllBonusLanguagesIfIntelligenceBonusIsHigher()
        {
            var bonusLanguages = new[] { "lang 1", "lang 2" };
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, className)).Returns(bonusLanguages);

            var languages = languageGenerator.GenerateWith(race, className, 9266);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void LanguagesContainAutomaticLanguagesAndBonusLanguages()
        {
            var automaticLanguages = new[] { "automatic language" };
            var bonusLanguages = new[] { "bonus language" };
            mockLanguageSelector.Setup(s => s.SelectAutomaticLanguagesFor(race, className)).Returns(automaticLanguages);
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, className)).Returns(bonusLanguages);

            var languages = languageGenerator.GenerateWith(race, className, 1);
            Assert.That(languages, Contains.Item("automatic language"));
            Assert.That(languages, Contains.Item("bonus language"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void LanguagesAreNotDuplicated()
        {
            var automaticLanguages = new[] { "automatic language", "other language" };
            var bonusLanguages = new[] { "bonus language", "automatic language" };
            mockLanguageSelector.Setup(s => s.SelectAutomaticLanguagesFor(race, className)).Returns(automaticLanguages);
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, className)).Returns(bonusLanguages);

            var languages = languageGenerator.GenerateWith(race, className, 2);
            Assert.That(languages, Contains.Item("automatic language"));
            Assert.That(languages, Contains.Item("bonus language"));
            Assert.That(languages, Contains.Item("other language"));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }
    }
}