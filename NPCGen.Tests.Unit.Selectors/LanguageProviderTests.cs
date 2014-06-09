using System;
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
    public class LanguageProviderTests
    {
        private ILanguageProvider provider;
        private Race race;
        private Mock<ILanguagesXmlParser> mockLanguagesXmlParser;
        private Dictionary<String, IEnumerable<String>> parsedLanguages;

        [SetUp]
        public void Setup()
        {
            race = new Race();
            race.BaseRace = String.Empty;
            race.Metarace = String.Empty;

            parsedLanguages = new Dictionary<String, IEnumerable<String>>();
            parsedLanguages.Add(String.Empty, Enumerable.Empty<String>());
            mockLanguagesXmlParser = new Mock<ILanguagesXmlParser>();
            mockLanguagesXmlParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(parsedLanguages);

            provider = new LanguagesProvider(mockLanguagesXmlParser.Object);
        }

        [Test]
        public void GetAutomaticLanguagesAccessesAutomaticLanguagesTable()
        {
            provider.GetAutomaticLanguagesFor(race);
            mockLanguagesXmlParser.Verify(p => p.Parse("AutomaticLanguages.xml"), Times.Once);
        }

        [Test]
        public void GetAutomaticLanguagesReturnsLanguagesForBaseRace()
        {
            race.BaseRace = "base race";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add("other base race", new[] { LanguageConstants.Draconic });

            var languages = provider.GetAutomaticLanguagesFor(race);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.False);
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAutomaticLanguagesReturnsLanguagesForMetarace()
        {
            race.Metarace = "metarace";
            parsedLanguages.Add(race.Metarace, new[] { LanguageConstants.Draconic });
            parsedLanguages.Add("other metarace", new[] { LanguageConstants.Celestial });

            var languages = provider.GetAutomaticLanguagesFor(race);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Celestial), Is.False);
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAutomaticLanguagesReturnsBothBaseRaceAndMetaraceLanguages()
        {
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add(race.Metarace, new[] { LanguageConstants.Draconic });

            var languages = provider.GetAutomaticLanguagesFor(race);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAutomaticLanguagesFiltersOutDuplicateLanguages()
        {
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add(race.Metarace, new[] { LanguageConstants.Common });

            var languages = provider.GetAutomaticLanguagesFor(race);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetBonusLanguagesAccessesBonusLanguagesTable()
        {
            provider.GetBonusLanguagesFor(String.Empty, String.Empty);
            mockLanguagesXmlParser.Verify(p => p.Parse("BonusLanguages.xml"), Times.Once);
        }

        [Test]
        public void GetBonusLanguagesReturnsLanguages()
        {
            race.BaseRace = "base race";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add("other base race", new[] { LanguageConstants.Draconic });

            var languages = provider.GetBonusLanguagesFor(race.BaseRace, String.Empty);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.False);
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ClericBonusLanguages()
        {
            var languages = provider.GetBonusLanguagesFor(String.Empty, CharacterClassConstants.Cleric);
            Assert.That(languages.Contains(LanguageConstants.Abyssal), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Celestial), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Infernal), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void WizardBonusLanguages()
        {
            var languages = provider.GetBonusLanguagesFor(String.Empty, CharacterClassConstants.Wizard);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DruidBonusLanguages()
        {
            var languages = provider.GetBonusLanguagesFor(String.Empty, CharacterClassConstants.Druid);
            Assert.That(languages.Contains(LanguageConstants.Sylvan), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DuplicateBonusLanguagesAreFilteredOut()
        {
            race.BaseRace = "base race";
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Abyssal });

            var languages = provider.GetBonusLanguagesFor(race.BaseRace, CharacterClassConstants.Cleric);
            Assert.That(languages.Contains(LanguageConstants.Abyssal), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Celestial), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Infernal), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(3));
        }
    }
}