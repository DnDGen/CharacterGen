using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Providers
{
    [TestFixture]
    public class LanguageProviderTests
    {
        private ILanguageProvider provider;
        private Race race;
        private Mock<ILanguagesXmlParser> mockLanguagesXmlParser;

        [SetUp]
        public void Setup()
        {
            race = new Race();
            mockLanguagesXmlParser = new Mock<ILanguagesXmlParser>();
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
            var parsedLanguages = new Dictionary<String, IEnumerable<String>>();
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add("other base race", new[] { LanguageConstants.Draconic });
            mockLanguagesXmlParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(parsedLanguages);

            var languages = provider.GetAutomaticLanguagesFor(race);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.False);
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAutomaticLanguagesReturnsLanguagesForMetarace()
        {
            race.Metarace = "metarace";
            var parsedLanguages = new Dictionary<String, IEnumerable<String>>();
            parsedLanguages.Add(race.Metarace, new[] { LanguageConstants.Draconic });
            parsedLanguages.Add("other metarace", new[] { LanguageConstants.Celestial });
            mockLanguagesXmlParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(parsedLanguages);

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
            var parsedLanguages = new Dictionary<String, IEnumerable<String>>();
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add(race.Metarace, new[] { LanguageConstants.Draconic });
            mockLanguagesXmlParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(parsedLanguages);

            var languages = provider.GetAutomaticLanguagesFor(race);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetBonusLanguagesAccessesBonusLanguagesTable()
        {
            provider.GetAutomaticLanguagesFor(race);
            mockLanguagesXmlParser.Verify(p => p.Parse("BonusLanguages.xml"), Times.Once);
        }

        [Test]
        public void GetBonusLanguagesReturnsLanguages()
        {
            race.BaseRace = "base race";
            var parsedLanguages = new Dictionary<String, IEnumerable<String>>();
            parsedLanguages.Add(race.BaseRace, new[] { LanguageConstants.Common });
            parsedLanguages.Add("other base race", new[] { LanguageConstants.Draconic });
            mockLanguagesXmlParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(parsedLanguages);

            var languages = provider.GetBonusLanguagesFor(race);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(languages.Contains(LanguageConstants.Draconic), Is.False);
            Assert.That(languages.Count(), Is.EqualTo(1));
        }
    }
}