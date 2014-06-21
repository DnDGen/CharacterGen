﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class LanguagesSelectorTests
    {
        private ILanguagesSelector Selector;
        private Race race;
        private Mock<ICollectionsMapper> mockLanguagesXmlMapper;
        private Dictionary<String, IEnumerable<String>> parsedLanguages;

        [SetUp]
        public void Setup()
        {
            race = new Race();
            race.BaseRace = String.Empty;
            race.Metarace = String.Empty;

            parsedLanguages = new Dictionary<String, IEnumerable<String>>();
            parsedLanguages.Add(String.Empty, Enumerable.Empty<String>());
            mockLanguagesXmlMapper = new Mock<ICollectionsMapper>();
            mockLanguagesXmlMapper.Setup(p => p.Map(It.IsAny<String>())).Returns(parsedLanguages);

            Selector = new LanguagesSelector(mockLanguagesXmlMapper.Object);
        }

        [Test]
        public void GetAutomaticLanguagesAccessesAutomaticLanguagesTable()
        {
            Selector.GetAutomaticLanguagesFor(race);
            mockLanguagesXmlMapper.Verify(p => p.Map("AutomaticLanguages"), Times.Once);
        }

        [Test]
        public void GetAutomaticLanguagesReturnsLanguagesForBaseRace()
        {
            race.BaseRace = "base race";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add("other base race", new[] { LanguageConstants.Draconic });

            var languages = Selector.GetAutomaticLanguagesFor(race);
            Assert.That(languages, Contains.Item(LanguageConstants.Common));
            Assert.That(languages, Is.Not.Contains(LanguageConstants.Draconic));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAutomaticLanguagesReturnsLanguagesForMetarace()
        {
            race.Metarace = "metarace";
            parsedLanguages.Add(race.Metarace, new[] { LanguageConstants.Draconic });
            parsedLanguages.Add("other metarace", new[] { LanguageConstants.Celestial });

            var languages = Selector.GetAutomaticLanguagesFor(race);
            Assert.That(languages, Contains.Item(LanguageConstants.Draconic));
            Assert.That(languages, Is.Not.Contains(LanguageConstants.Celestial));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAutomaticLanguagesReturnsBothBaseRaceAndMetaraceLanguages()
        {
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add(race.Metarace, new[] { LanguageConstants.Draconic });

            var languages = Selector.GetAutomaticLanguagesFor(race);
            Assert.That(languages, Contains.Item(LanguageConstants.Draconic));
            Assert.That(languages, Contains.Item(LanguageConstants.Common));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAutomaticLanguagesFiltersOutDuplicateLanguages()
        {
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add(race.Metarace, new[] { LanguageConstants.Common });

            var languages = Selector.GetAutomaticLanguagesFor(race);
            Assert.That(languages, Contains.Item(LanguageConstants.Common));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetBonusLanguagesAccessesBonusLanguagesTable()
        {
            Selector.GetBonusLanguagesFor(String.Empty, String.Empty);
            mockLanguagesXmlMapper.Verify(p => p.Map("BonusLanguages"), Times.Once);
        }

        [Test]
        public void GetBonusLanguagesReturnsLanguages()
        {
            race.BaseRace = "base race";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add("other base race", new[] { LanguageConstants.Draconic });

            var languages = Selector.GetBonusLanguagesFor(race.BaseRace, String.Empty);
            Assert.That(languages, Contains.Item(LanguageConstants.Common));
            Assert.That(languages, Is.Not.Contains(LanguageConstants.Draconic));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ClericBonusLanguages()
        {
            var languages = Selector.GetBonusLanguagesFor(String.Empty, CharacterClassConstants.Cleric);
            Assert.That(languages, Contains.Item(LanguageConstants.Abyssal));
            Assert.That(languages, Contains.Item(LanguageConstants.Celestial));
            Assert.That(languages, Contains.Item(LanguageConstants.Infernal));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void WizardBonusLanguages()
        {
            var languages = Selector.GetBonusLanguagesFor(String.Empty, CharacterClassConstants.Wizard);
            Assert.That(languages, Contains.Item(LanguageConstants.Draconic));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DruidBonusLanguages()
        {
            var languages = Selector.GetBonusLanguagesFor(String.Empty, CharacterClassConstants.Druid);
            Assert.That(languages, Contains.Item(LanguageConstants.Sylvan));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DuplicateBonusLanguagesAreFilteredOut()
        {
            race.BaseRace = "base race";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Abyssal });

            var languages = Selector.GetBonusLanguagesFor(race.BaseRace, CharacterClassConstants.Cleric);
            Assert.That(languages, Contains.Item(LanguageConstants.Abyssal));
            Assert.That(languages, Contains.Item(LanguageConstants.Celestial));
            Assert.That(languages, Contains.Item(LanguageConstants.Infernal));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }
    }
}