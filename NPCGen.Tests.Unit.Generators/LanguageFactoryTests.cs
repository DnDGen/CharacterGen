using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Core.Data;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generators;
using NPCGen.Core.Generators.Interfaces;
using NPCGen.Core.Generators.Providers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class LanguageFactoryTests
    {
        private Mock<IDice> mockDice;
        private Mock<ILanguageProvider> mockLanguageProvider;
        private ILanguageFactory languageFactory;

        private Race race;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockLanguageProvider = new Mock<ILanguageProvider>();
            languageFactory = new LanguageFactory(mockDice.Object, mockLanguageProvider.Object);

            race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";
        }

        [Test]
        public void FactoryGetsAutomaticLanguagesFromProvider()
        {
            var languages = languageFactory.CreateWith(race, String.Empty, 0);
            mockLanguageProvider.Verify(p => p.GetAutomaticLanguagesFor(race), Times.Once);
        }

        [Test]
        public void DruidsGetDruidic()
        {
            foreach (var baseRace in RaceConstants.BaseRaces.GetBaseRaces())
            {
                foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
                {
                    race.BaseRace = baseRace;
                    race.Metarace = metarace;

                    var languages = languageFactory.CreateWith(race, CharacterClassConstants.Druid, 0);
                    Assert.That(languages.Contains(LanguageConstants.Druidic), Is.True);
                }
            }
        }

        [Test]
        public void NonDruidsDoNotGetDruidic()
        {
            foreach (var baseRace in RaceConstants.BaseRaces.GetBaseRaces())
            {
                foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
                {
                    foreach (var className in CharacterClassConstants.GetClassNames())
                    {
                        if (className != CharacterClassConstants.Druid)
                        {
                            race.BaseRace = baseRace;
                            race.Metarace = metarace;

                            var languages = languageFactory.CreateWith(race, className, 0);
                            Assert.That(languages.Contains(LanguageConstants.Druidic), Is.False);
                        }
                    }
                }
            }
        }

        [Test]
        public void GetBonusLanguagesFromProvider()
        {
            var className = "class";

            var languages = languageFactory.CreateWith(race, className, 1);
            mockLanguageProvider.Verify(p => p.GetBonusLanguagesFor(race.BaseRace, className), Times.Once);
        }

        [Test]
        public void GetNumberOfBonusLanguagesEqualToIntelligenceModifier()
        {
            mockLanguageProvider.Setup(p => p.GetBonusLanguagesFor(race.BaseRace, It.IsAny<String>())).Returns(new[] { "lang 1", "lang 2" });

            var languages = languageFactory.CreateWith(race, String.Empty, 1);
            mockDice.Verify(d => d.Roll(It.IsAny<String>()), Times.Exactly(1));
        }

        [Test]
        public void StopIfAllBonusLanguagesLearned()
        {
            mockLanguageProvider.Setup(p => p.GetBonusLanguagesFor(race.BaseRace, It.IsAny<String>())).Returns(new[] { "lang 1", "lang 2" });

            var languages = languageFactory.CreateWith(race, String.Empty, 9266);
            Assert.That(languages.Count(), Is.EqualTo(2));
        }
    }
}