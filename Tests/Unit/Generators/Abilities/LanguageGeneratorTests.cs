using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class LanguageGeneratorTests
    {
        private Mock<IDice> mockDice;
        private Mock<ILanguageCollectionsSelector> mockLanguageSelector;
        private ILanguageGenerator languageGenerator;
        private Race race;
        private String className;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockLanguageSelector = new Mock<ILanguageCollectionsSelector>();
            languageGenerator = new LanguageGenerator(mockDice.Object, mockLanguageSelector.Object);
            race = new Race();

            race.BaseRace = "baserace";
            race.Metarace = "metarace";
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
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
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

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
            mockDice.Verify(d => d.Roll(1).d(It.IsAny<Int32>()), Times.Never);
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