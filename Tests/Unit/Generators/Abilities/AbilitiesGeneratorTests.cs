using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Domain.Abilities;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorTests
    {
        private IAbilitiesGenerator abilitiesGenerator;
        private Mock<IStatsRandomizer> mockStatsRandomizer;
        private Mock<IStatsGenerator> mockStatsGenerator;
        private Mock<ILanguageGenerator> mockLanguageGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Stat> stats;
        private BaseAttack baseAttack;

        [SetUp]
        public void Setup()
        {
            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockStatsGenerator = new Mock<IStatsGenerator>();
            mockLanguageGenerator = new Mock<ILanguageGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            abilitiesGenerator = new AbilitiesGenerator(mockStatsGenerator.Object, mockLanguageGenerator.Object,
                mockSkillsGenerator.Object, mockFeatsGenerator.Object, mockCollectionsSelector.Object);
            stats = new Dictionary<String, Stat>();
            baseAttack = new BaseAttack();
            characterClass = new CharacterClass();
            race = new Race();

            characterClass.ClassName = "class name";
            stats[StatConstants.Intelligence] = new Stat { Value = 9266 };
            mockStatsGenerator.Setup(g => g.GenerateWith(mockStatsRandomizer.Object, characterClass, race)).Returns(stats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills)).Returns(new[] { "skill 1", "skill 2", "skill 3", "skill 4", "skill 5" });
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

        [Test]
        public void ApplyFeatThatGrantSkillBonusesToSkills()
        {
            var skills = new Dictionary<String, Skill>();
            skills["skill 1"] = new Skill();
            skills["skill 1"].Bonus = 1;
            skills["skill 2"] = new Skill();
            skills["skill 2"].Bonus = 2;
            skills["skill 3"] = new Skill();
            skills["skill 3"].Bonus = 3;
            skills["skill 4"] = new Skill();
            skills["skill 4"].Bonus = 4;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Strength = 1;
            feats[1].Name = "feat2";
            feats[1].Strength = 2;
            feats[2].Name = "feat3";
            feats[2].Strength = 3;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat3", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "feat3")).Returns(new[] { "skill 2", "skill 4" });

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].Bonus, Is.EqualTo(2));
            Assert.That(ability.Skills["skill 2"].Bonus, Is.EqualTo(5));
            Assert.That(ability.Skills["skill 3"].Bonus, Is.EqualTo(3));
            Assert.That(ability.Skills["skill 4"].Bonus, Is.EqualTo(7));
        }

        [Test]
        public void IfFocusIsSkill_ApplyBonusToThatSkill()
        {
            var skills = new Dictionary<String, Skill>();
            skills["skill 1"] = new Skill();
            skills["skill 1"].Bonus = 1;
            skills["skill 2"] = new Skill();
            skills["skill 2"].Bonus = 2;
            skills["skill 3"] = new Skill();
            skills["skill 3"].Bonus = 3;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Focus = "skill 2";
            feats[0].Strength = 1;
            feats[1].Name = "feat2";
            feats[1].Focus = "skill 3";
            feats[1].Strength = 2;
            feats[2].Name = "feat1";
            feats[2].Focus = "skill 2";
            feats[2].Strength = 3;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].Bonus, Is.EqualTo(1));
            Assert.That(ability.Skills["skill 2"].Bonus, Is.EqualTo(6));
            Assert.That(ability.Skills["skill 3"].Bonus, Is.EqualTo(5));
        }

        [Test]
        public void OnlyApplySkillFeatToSkillsIfSkillFocusIsPurelySkill()
        {
            var skills = new Dictionary<String, Skill>();
            skills["skill 1"] = new Skill();
            skills["skill 1"].Bonus = 1;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Focus = "skill 1 (with qualifiers)";
            feats[0].Strength = 1;

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].Bonus, Is.EqualTo(1));
        }

        [Test]
        public void NoCircumstantialBonusIfBonusApplied()
        {
            var skills = new Dictionary<String, Skill>();
            skills["skill 1"] = new Skill();
            skills["skill 1"].Bonus = 1;
            skills["skill 2"] = new Skill();
            skills["skill 2"].Bonus = 2;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Strength = 1;
            feats[1].Name = "feat2";
            feats[1].Focus = "skill 2";
            feats[1].Strength = 2;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].Bonus, Is.EqualTo(2));
            Assert.That(ability.Skills["skill 1"].CircumstantialBonus, Is.False);
            Assert.That(ability.Skills["skill 2"].Bonus, Is.EqualTo(4));
            Assert.That(ability.Skills["skill 2"].CircumstantialBonus, Is.False);
        }

        [Test]
        public void IfSkillBonusFocusIsNotPurelySkill_MarkSkillAsHavingCircumstantialBonus()
        {
            var skills = new Dictionary<String, Skill>();
            skills["skill 1"] = new Skill();
            skills["skill 1"].Bonus = 1;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Focus = "skill 1 (with qualifiers)";
            feats[0].Strength = 1;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);


            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].CircumstantialBonus, Is.True);
        }
    }
}