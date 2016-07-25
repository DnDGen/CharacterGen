using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Domain.Generators.Abilities.Feats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Stats;
using Moq;
using NUnit.Framework;
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
        private Dictionary<string, Stat> stats;
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
            stats = new Dictionary<string, Stat>();
            baseAttack = new BaseAttack();
            characterClass = new CharacterClass();
            race = new Race();

            characterClass.Name = "class name";
            stats[StatConstants.Intelligence] = new Stat(StatConstants.Intelligence);
            stats[StatConstants.Intelligence].Value = 9266;
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
            mockLanguageGenerator.Setup(g => g.GenerateWith(race, characterClass.Name, stats[StatConstants.Intelligence].Bonus))
                .Returns(languages);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void GetSkillsFromSkillGenerator()
        {
            var skills = new Dictionary<string, Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GetFeatsFromFeatGenerator()
        {
            var skills = new Dictionary<string, Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void ApplyFeatThatGrantSkillBonusesToSkills()
        {
            var skills = new Dictionary<string, Skill>();
            var baseStat = new Stat("base stat");

            skills["skill 1"] = new Skill("skill 1", baseStat, 1);
            skills["skill 1"].Bonus = 1;
            skills["skill 2"] = new Skill("skill 2", baseStat, 1);
            skills["skill 2"].Bonus = 2;
            skills["skill 3"] = new Skill("skill 3", baseStat, 1);
            skills["skill 3"].Bonus = 3;
            skills["skill 4"] = new Skill("skill 4", baseStat, 1);
            skills["skill 4"].Bonus = 4;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Power = 2;
            feats[2].Name = "feat3";
            feats[2].Power = 3;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat3", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus))
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
            var skills = new Dictionary<string, Skill>();
            var baseStat = new Stat("base stat");

            skills["skill 1"] = new Skill("skill 1", baseStat, 1);
            skills["skill 1"].Bonus = 1;
            skills["skill 2"] = new Skill("skill 2", baseStat, 1);
            skills["skill 2"].Bonus = 2;
            skills["skill 3"] = new Skill("skill 3", baseStat, 1);
            skills["skill 3"].Bonus = 3;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 2", "skill 3", "non-skill focus" };
            feats[0].Power = 4;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 3", "non-skill focus" };
            feats[1].Power = 1;
            feats[2].Name = "feat1";
            feats[2].Foci = new[] { "skill 2", "non-skill focus" };
            feats[2].Power = 3;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].Bonus, Is.EqualTo(1));
            Assert.That(ability.Skills["skill 2"].Bonus, Is.EqualTo(9));
            Assert.That(ability.Skills["skill 3"].Bonus, Is.EqualTo(8));
        }

        [Test]
        public void OnlyApplySkillFeatToSkillsIfSkillFocusIsPurelySkill()
        {
            var skills = new Dictionary<string, Skill>();
            var baseStat = new Stat("base stat");

            skills["skill 1"] = new Skill("skill 1", baseStat, 1);
            skills["skill 1"].Bonus = 1;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1 (with qualifiers)", "non-skill focus" };
            feats[0].Power = 1;

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].Bonus, Is.EqualTo(1));
        }

        [Test]
        public void NoCircumstantialBonusIfBonusApplied()
        {
            var skills = new Dictionary<string, Skill>();
            var baseStat = new Stat("base stat");

            skills["skill 1"] = new Skill("skill 1", baseStat, 1);
            skills["skill 1"].Bonus = 1;
            skills["skill 2"] = new Skill("skill 2", baseStat, 1);
            skills["skill 2"].Bonus = 2;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 2", "non-skill focus" };
            feats[1].Power = 2;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus))
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
            var skills = new Dictionary<string, Skill>();
            var baseStat = new Stat("base stat");

            skills["skill 1"] = new Skill("skill 1", baseStat, 1);
            skills["skill 1"].Bonus = 1;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1 (with qualifiers)", "non-skill focus" };
            feats[0].Power = 1;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].CircumstantialBonus, Is.True);
        }

        [Test]
        public void MarkSkillWithCircumstantialBonusWhenOtherFociDoNotHaveCircumstantialBonus()
        {
            var skills = new Dictionary<string, Skill>();
            var baseStat = new Stat("base stat");

            skills["skill 1"] = new Skill("skill 1", baseStat, 1);
            skills["skill 1"].Bonus = 1;
            skills["skill 2"] = new Skill("skill 2", baseStat, 1);
            skills["skill 2"].Bonus = 2;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1 (with qualifiers)", "skill 2" };
            feats[0].Power = 1;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].CircumstantialBonus, Is.True);
            Assert.That(ability.Skills["skill 2"].CircumstantialBonus, Is.False);
        }

        [Test]
        public void CircumstantialBonusIsNotOverwritten()
        {
            var skills = new Dictionary<string, Skill>();
            var baseStat = new Stat("base stat");

            skills["skill 1"] = new Skill("skill 1", baseStat, 1);
            skills["skill 1"].Bonus = 1;
            skills["skill 2"] = new Skill("skill 2", baseStat, 1);
            skills["skill 2"].Bonus = 2;
            mockSkillsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1 (with qualifiers)", "skill 2" };
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 1" };
            feats[1].Power = 1;

            mockFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            var ability = abilitiesGenerator.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack);
            Assert.That(ability.Skills["skill 1"].CircumstantialBonus, Is.True);
            Assert.That(ability.Skills["skill 2"].CircumstantialBonus, Is.False);
        }
    }
}