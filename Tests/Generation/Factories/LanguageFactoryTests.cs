using System;
using System.Linq;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class LanguageFactoryTests
    {
        private Mock<IDice> mockDice;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            race = new Race();
            race.BaseRace = RaceConstants.BaseRaces.Human;
            race.Metarace = String.Empty;
        }

        [Test]
        public void FactoryGetsAutomaticLanguagesFromProvider()
        {
            var languages = LanguageFactory.CreateUsing(race, String.Empty, mockDice.Object, 0);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(1));
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

                    var languages = LanguageFactory.CreateUsing(race, CharacterClassConstants.Druid, mockDice.Object, 0);
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

                            var languages = LanguageFactory.CreateUsing(race, className, mockDice.Object, 0);
                            Assert.That(languages.Contains(LanguageConstants.Druidic), Is.False);
                        }
                    }
                }
            }
        }

        [Test]
        public void GetBonusLanguagesFromProvider()
        {
            var expectedLanguage = LanguageConstants.GetLanguages().ElementAt(0);

            var languages = LanguageFactory.CreateUsing(race, String.Empty, mockDice.Object, 1);
            Assert.That(languages.Contains(expectedLanguage), Is.True, expectedLanguage);
        }

        [Test]
        public void GetNumberOfBonusLanguagesEqualToIntelligenceModifier()
        {
            var languages = LanguageFactory.CreateUsing(race, String.Empty, mockDice.Object, 2);
            Assert.That(languages.Count(), Is.EqualTo(3)); //+1 for Human automatic language
        }

        [Test]
        public void StopIfAllBonusLanguagesLearned()
        {
            race.BaseRace = RaceConstants.BaseRaces.Minotaur;

            var languages = LanguageFactory.CreateUsing(race, String.Empty, mockDice.Object, 9266);
            Assert.That(languages.Count(), Is.EqualTo(5)); //Minotaur has 2 automatic languages
            mockDice.Verify(d => d.d20(1, -1), Times.Exactly(3));
        }
    }
}