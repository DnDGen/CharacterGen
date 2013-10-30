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
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            race = new Race();
            characterClass = new CharacterClass();
        }

        [Test]
        public void FactoryGetsAutomaticLanguagesFromProvider()
        {
            race.BaseRace = RaceConstants.BaseRaces.Human;
            var languages = LanguageFactory.CreateUsing(race, characterClass, mockDice.Object);
            Assert.That(languages.Contains(LanguageConstants.Common), Is.True);
        }
    }
}