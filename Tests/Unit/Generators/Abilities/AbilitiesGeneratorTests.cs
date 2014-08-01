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
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Selectors.Interfaces;
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
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

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
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            abilitiesGenerator = new AbilitiesGenerator(mockStatsGenerator.Object, mockLanguageGenerator.Object,
                mockSkillsGenerator.Object, mockFeatsGenerator.Object, mockAdjustmentsSelector.Object);
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
            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills)).Returns(feats);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void AdjustSkillsByFeat()
        {
            var skills = new Dictionary<String, Skill>();
            skills["skill 1"] = new Skill();
            skills["skill 2"] = new Skill();
            skills["skill 3"] = new Skill();
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            var featWithNoSkills = new Feat { Name = "no adjust" };
            var featWithSkills = new Feat { Name = "adjust" };
            feats.Add(featWithNoSkills);
            feats.Add(featWithSkills);
            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills)).Returns(feats);

            var noAdjustments = new Dictionary<String, Int32>();
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("no adjustSkillAdjustments")).Returns(noAdjustments);

            var adjustments = new Dictionary<String, Int32>();
            adjustments["skill 1"] = 92;
            adjustments["skill 2"] = 66;
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("adjustSkillAdjustments")).Returns(adjustments);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].Bonus, Is.EqualTo(adjustments["skill 1"]));
            Assert.That(ability.Skills["skill 2"].Bonus, Is.EqualTo(adjustments["skill 2"]));
            Assert.That(ability.Skills["skill 3"].Bonus, Is.EqualTo(0));
        }
    }
}