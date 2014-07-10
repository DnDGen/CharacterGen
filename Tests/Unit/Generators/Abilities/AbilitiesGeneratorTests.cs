using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorTests
    {
        private IAbilitiesGenerator abilitiesGenerator;
        private CharacterClass characterClass;
        private Race race;
        private Mock<IStatsRandomizer> mockStatsRandomizer;
        private Mock<IStatsGenerator> mockStatsGenerator;
        private Mock<ILanguageGenerator> mockLanguageGenerator;

        [SetUp]
        public void Setup()
        {
            characterClass = new CharacterClass();
            race = new Race();
            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockStatsGenerator = new Mock<IStatsGenerator>();
            mockLanguageGenerator = new Mock<ILanguageGenerator>();
            abilitiesGenerator = new AbilitiesGenerator(mockStatsGenerator.Object, mockLanguageGenerator.Object);

            characterClass.ClassName = "class name";
        }

        [Test]
        public void GetStatsFromStatsGenerator()
        {
            var stats = new Dictionary<String, Stat>();
            mockStatsGenerator.Setup(g => g.GenerateWith(mockStatsRandomizer.Object, characterClass, race)).Returns(stats);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object);
            Assert.That(ability.Stats, Is.EqualTo(stats));
        }

        [Test]
        public void GetLanguagesFromLanguageGenerator()
        {
            var stats = new Dictionary<String, Stat>();
            stats.Add(StatConstants.Intelligence, new Stat { Value = 9266 });
            mockStatsGenerator.Setup(g => g.GenerateWith(mockStatsRandomizer.Object, characterClass, race)).Returns(stats);

            var languages = new[] { "language 1", "language 2" };
            mockLanguageGenerator.Setup(g => g.GenerateWith(race, characterClass.ClassName, stats[StatConstants.Intelligence].Bonus))
                .Returns(languages);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object);
            Assert.That(ability.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void GetSkillsFromSkillGenerator()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFeatsFromFeatGenerator()
        {
            Assert.Fail();
        }

        [Test]
        public void AdjustSkillsByFeat()
        {
            Assert.Fail();
        }
    }
}