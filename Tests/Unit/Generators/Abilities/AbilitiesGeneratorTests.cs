using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Abilities.Feats;
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
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private BaseAttack baseAttack;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Dictionary<String, Stat> stats;

        [SetUp]
        public void Setup()
        {
            characterClass = new CharacterClass();
            race = new Race();
            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockStatsGenerator = new Mock<IStatsGenerator>();
            mockLanguageGenerator = new Mock<ILanguageGenerator>();
            baseAttack = new BaseAttack();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            abilitiesGenerator = new AbilitiesGenerator(mockStatsGenerator.Object, mockLanguageGenerator.Object,
                mockSkillsGenerator.Object, mockFeatsGenerator.Object);
            stats = new Dictionary<String, Stat>();

            characterClass.ClassName = "class name";
            stats[StatConstants.Intelligence] = new Stat { Value = 9266 };
            mockStatsGenerator.Setup(g => g.GenerateWith(mockStatsRandomizer.Object, characterClass, race)).Returns(stats);
        }

        [Test]
        public void GetStatsFromStatsGenerator()
        {
            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Stats, Is.EqualTo(stats));
        }

        [Test]
        public void GetLanguagesFromLanguageGenerator()
        {
            var languages = new[] { "language 1", "language 2" };
            mockLanguageGenerator.Setup(g => g.GenerateWith(race, characterClass.ClassName, stats[StatConstants.Intelligence].Bonus))
                .Returns(languages);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void GetSkillsFromSkillGenerator()
        {
            var skills = new Dictionary<String, Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GetFeatsFromFeatGenerator()
        {
            var skills = new Dictionary<String, Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Feats, Is.EqualTo(feats));
        }
    }
}