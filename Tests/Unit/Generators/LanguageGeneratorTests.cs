using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class LanguageGeneratorTests
    {
        private Mock<IDice> mockDice;
        private Mock<ILanguagesSelector> mockLanguageSelector;
        private ILanguageGenerator languageGenerator;

        private Race race;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockLanguageSelector = new Mock<ILanguagesSelector>();
            languageGenerator = new LanguageGenerator(mockDice.Object, mockLanguageSelector.Object);

            race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";
        }

        [Test]
        public void GeneratorGetsAutomaticLanguagesFromSelector()
        {
            var languages = languageGenerator.CreateWith(race, String.Empty, 0);
            mockLanguageSelector.Verify(p => p.GetAutomaticLanguagesFor(race), Times.Once);
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

                    var languages = languageGenerator.CreateWith(race, CharacterClassConstants.Druid, 0);
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

                            var languages = languageGenerator.CreateWith(race, className, 0);
                            Assert.That(languages.Contains(LanguageConstants.Druidic), Is.False);
                        }
                    }
                }
            }
        }

        [Test]
        public void GetBonusLanguagesFromSelector()
        {
            var className = "class";

            var languages = languageGenerator.CreateWith(race, className, 1);
            mockLanguageSelector.Verify(p => p.GetBonusLanguagesFor(race.BaseRace, className), Times.Once);
        }

        [Test]
        public void GetNumberOfBonusLanguagesEqualToIntelligenceModifier()
        {
            mockLanguageSelector.Setup(p => p.GetBonusLanguagesFor(race.BaseRace, It.IsAny<String>())).Returns(new[] { "lang 1", "lang 2" });

            var languages = languageGenerator.CreateWith(race, String.Empty, 1);
            mockDice.Verify(d => d.Roll(It.IsAny<String>()), Times.Exactly(1));
        }

        [Test]
        public void StopIfAllBonusLanguagesLearned()
        {
            mockLanguageSelector.Setup(p => p.GetBonusLanguagesFor(race.BaseRace, It.IsAny<String>())).Returns(new[] { "lang 1", "lang 2" });

            var languages = languageGenerator.CreateWith(race, String.Empty, 9266);
            Assert.That(languages.Count(), Is.EqualTo(2));
        }
    }
}