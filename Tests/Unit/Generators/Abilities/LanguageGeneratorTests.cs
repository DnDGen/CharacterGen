using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities;
using NPCGen.Common.CharacterClasses;
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
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
        }

        [Test]
        public void GetAutomaticLanguagesFromSelector()
        {
            mockLanguageSelector.Setup(s => s.SelectAutomaticLanguagesFor(race)).Returns(new[] { "lang 1", "lang 2" });

            var languages = languageGenerator.GenerateWith(race, String.Empty, 0);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages.Count(), Is.EqualTo(2));
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

                    var languages = languageGenerator.GenerateWith(race, CharacterClassConstants.Druid, 0);
                    Assert.That(languages, Contains.Item(LanguageConstants.Druidic));
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

                            var languages = languageGenerator.GenerateWith(race, className, 0);
                            Assert.That(languages, Is.Not.Contains(LanguageConstants.Druidic));
                        }
                    }
                }
            }
        }

        [Test]
        public void GetNumberOfBonusLanguagesEqualToIntelligenceModifier()
        {
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, "class")).Returns(new[] { "lang 1", "lang 2", "lang 3" });

            var languages = languageGenerator.GenerateWith(race, "class", 2);
            mockDice.Verify(d => d.Roll(1).d(It.IsAny<Int32>()), Times.Exactly(2));
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllBonusLanguagesIfIntelligenceBonusIsHigher()
        {
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, "class")).Returns(new[] { "lang 1", "lang 2" });

            var languages = languageGenerator.GenerateWith(race, "class", 9266);
            mockDice.Verify(d => d.Roll(1).d(It.IsAny<Int32>()), Times.Exactly(0));
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void LanguagesContainAutomaticLanguagesAndBonusLanguages()
        {
            mockLanguageSelector.Setup(s => s.SelectAutomaticLanguagesFor(race)).Returns(new[] { "automatic language" });
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, "class")).Returns(new[] { "bonus language" });

            var languages = languageGenerator.GenerateWith(race, "class", 1);
            Assert.That(languages, Contains.Item("automatic language"));
            Assert.That(languages, Contains.Item("bonus language"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DruidsHaveAutomaticLanguagesAndBonusLanguagesAndDruidic()
        {
            mockLanguageSelector.Setup(s => s.SelectAutomaticLanguagesFor(race)).Returns(new[] { "automatic language" });
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, CharacterClassConstants.Druid)).Returns(new[] { "bonus language" });

            var languages = languageGenerator.GenerateWith(race, CharacterClassConstants.Druid, 1);
            Assert.That(languages, Contains.Item("automatic language"));
            Assert.That(languages, Contains.Item("bonus language"));
            Assert.That(languages, Contains.Item(LanguageConstants.Druidic));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void LanguagesAreNotDuplicated()
        {
            mockLanguageSelector.Setup(s => s.SelectAutomaticLanguagesFor(race)).Returns(new[] { "automatic language" });
            mockLanguageSelector.Setup(p => p.SelectBonusLanguagesFor(race.BaseRace, CharacterClassConstants.Druid))
                .Returns(new[] { "automatic language", LanguageConstants.Druidic, "bonus language" });

            var languages = languageGenerator.GenerateWith(race, CharacterClassConstants.Druid, 1);
            Assert.That(languages, Contains.Item("automatic language"));
            Assert.That(languages, Contains.Item("bonus language"));
            Assert.That(languages, Contains.Item(LanguageConstants.Druidic));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }
    }
}