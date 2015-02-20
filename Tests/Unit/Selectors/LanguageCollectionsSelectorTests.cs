using System;
using System.Linq;
using Moq;
using NPCGen.Common.Races;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class LanguageCollectionsSelectorTests
    {
        private ILanguageCollectionsSelector selector;
        private Race race;
        private Mock<ICollectionsSelector> mockInnerSelector;
        private String className;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<ICollectionsSelector>();
            selector = new LanguageCollectionsSelector(mockInnerSelector.Object);
            race = new Race();

            race.BaseRace = "base race";
            race.Metarace = "metarace";
            className = "class name";
        }

        [Test]
        public void GetAutomaticLanguagesForBaseRace()
        {
            var baseRaceLanguages = new[] { "lang 1", "lang 2", "lang 5" };
            var metaraceLanguages = Enumerable.Empty<String>();
            var classLanguages = Enumerable.Empty<String>();
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.Metarace)).Returns(metaraceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, className)).Returns(classLanguages);

            var languages = selector.SelectAutomaticLanguagesFor(race, className);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 5"));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GetAutomaticLanguagesForMetarace()
        {
            var baseRaceLanguages = Enumerable.Empty<String>();
            var metaraceLanguages = new[] { "lang 3", "lang 4" };
            var classLanguages = Enumerable.Empty<String>();
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.Metarace)).Returns(metaraceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, className)).Returns(classLanguages);

            var languages = selector.SelectAutomaticLanguagesFor(race, className);
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages, Contains.Item("lang 4"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAutomaticLanguagesForClassName()
        {
            var baseRaceLanguages = Enumerable.Empty<String>();
            var metaraceLanguages = Enumerable.Empty<String>();
            var classLanguages = new[] { "lang 6", "lang 7" };
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.Metarace)).Returns(metaraceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, className)).Returns(classLanguages);

            var languages = selector.SelectAutomaticLanguagesFor(race, className);
            Assert.That(languages, Contains.Item("lang 6"));
            Assert.That(languages, Contains.Item("lang 7"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAutomaticLanguagesForBaseRaceAndMetaraceAndClassName()
        {
            var baseRaceLanguages = new[] { "lang 1", "lang 2", "lang 5" };
            var metaraceLanguages = new[] { "lang 3", "lang 4" };
            var classLanguages = new[] { "lang 6", "lang 7" };
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.Metarace)).Returns(metaraceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, className)).Returns(classLanguages);

            var languages = selector.SelectAutomaticLanguagesFor(race, className);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages, Contains.Item("lang 4"));
            Assert.That(languages, Contains.Item("lang 5"));
            Assert.That(languages, Contains.Item("lang 6"));
            Assert.That(languages, Contains.Item("lang 7"));
            Assert.That(languages.Count(), Is.EqualTo(7));
        }

        [Test]
        public void AutomaticLanguagesAreUnique()
        {
            var baseRaceLanguages = new[] { "lang 1", "lang 2", "lang 4" };
            var metaraceLanguages = new[] { "lang 1", "lang 2", "lang 3" };
            var classLanguages = new[] { "lang 1", "lang 3", "lang 4" };
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.Metarace)).Returns(metaraceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, className)).Returns(classLanguages);

            var languages = selector.SelectAutomaticLanguagesFor(race, className);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages, Contains.Item("lang 4"));
            Assert.That(languages.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GetBonusLanguagesForBaseRace()
        {
            var baseRaceLanguages = new[] { "lang 1", "lang 2", "lang 5" };
            var classLanguages = Enumerable.Empty<String>();
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, "class name")).Returns(classLanguages);

            var languages = selector.SelectBonusLanguagesFor(race.BaseRace, className);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 5"));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GetBonusLanguagesForClass()
        {
            var baseRaceLanguages = Enumerable.Empty<String>();
            var classLanguages = new[] { "lang 3", "lang 4" };
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, "class name")).Returns(classLanguages);

            var languages = selector.SelectBonusLanguagesFor(race.BaseRace, className);
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages, Contains.Item("lang 4"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetBonusLanguagesForBaseRaceAndClass()
        {
            var baseRaceLanguages = new[] { "lang 1", "lang 2", "lang 5" };
            var classLanguages = new[] { "lang 3", "lang 4" };
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, "class name")).Returns(classLanguages);

            var languages = selector.SelectBonusLanguagesFor(race.BaseRace, className);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages, Contains.Item("lang 4"));
            Assert.That(languages, Contains.Item("lang 5"));
            Assert.That(languages.Count(), Is.EqualTo(5));
        }

        [Test]
        public void BonusLanguagesAreUnique()
        {
            var baseRaceLanguages = new[] { "lang 1", "lang 2" };
            var classLanguages = new[] { "lang 1", "lang 3" };
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, race.BaseRace)).Returns(baseRaceLanguages);
            mockInnerSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, "class name")).Returns(classLanguages);

            var languages = selector.SelectBonusLanguagesFor(race.BaseRace, className);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }
    }
}