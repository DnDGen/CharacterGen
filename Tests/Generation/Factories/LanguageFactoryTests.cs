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
        }

        [Test]
        public void DruidsGetDruidic()
        {
            var languages = LanguageFactory.CreateUsing(race, CharacterClassConstants.Druid, mockDice.Object, 0);
            Assert.That(languages.Contains(LanguageConstants.Druidic), Is.True);
        }

        [Test]
        public void OnlyDruidsGetDruidic()
        {
            foreach (var className in CharacterClassConstants.GetClassNames())
            {
                if (className != CharacterClassConstants.Druid)
                {
                    var languages = LanguageFactory.CreateUsing(race, className, mockDice.Object, 0);
                    Assert.That(languages.Contains(LanguageConstants.Druidic), Is.False);
                }
            }
        }

        [Test]
        public void GetBonusLanguagesFromProvider()
        {
            mockDice.Setup(d => d.d20(1, 0)).Returns(1);
            var languages = LanguageFactory.CreateUsing(race, String.Empty, mockDice.Object, 1);
            Assert.That(languages.Contains(LanguageConstants.Abyssal), Is.True);
        }
    }
}