using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class FeatsGeneratorTests
    {
        private IFeatsGenerator featsGenerator;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            featsGenerator = new FeatsGenerator();
            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
        }

        [Test]
        public void GetBaseRacialFeats()
        {
            race.BaseRace = "base race";
            var racialFeats = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", "base race")).Returns(racialFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills);

            foreach (var feat in racialFeats)
                Assert.That(feats, Contains.Item(feat));
        }

        [Test]
        public void GetMetaRacialFeats()
        {
            race.Metarace = "metarace";
            var racialFeats = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", "metarace")).Returns(racialFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills);

            foreach (var feat in racialFeats)
                Assert.That(feats, Contains.Item(feat));
        }

        [Test]
        public void GetClassFeatsWithMatchingLevelRequirement()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;
            var classFeats = new[] { "feat 1", "feat 2", "feat 3", "feat 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassFeats", "class name")).Returns(classFeats);

            var levelRequirements = new Dictionary<String, Int32>();
            levelRequirements["feat 1"] = 1;
            levelRequirements["feat 2"] = 1;
            levelRequirements["feat 3"] = 2;
            levelRequirements["feat 4"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("class nameFeatLevelRequirements")).Returns(levelRequirements);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills);

            Assert.That(feats, Contains.Item("feat 1"));
            Assert.That(feats, Contains.Item("feat 2"));
            Assert.That(feats, Contains.Item("feat 3"));
            Assert.That(feats, Is.Not.Contains("feat 4"));
        }

        [Test]
        public void GetAdditionalFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void HumansGetAdditionalFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void FightersGetBonusFighterFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void DoNotGetFeatWithUnmetFeatPrerequisite()
        {
            Assert.Fail();
        }

        [Test]
        public void DoNotGetFeatWithUnmetStatPrerequisites()
        {
            Assert.Fail();
        }

        [Test]
        public void DoNotGetFeatWithUnmetSkillRankPrerequisites()
        {
            Assert.Fail();
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            Assert.Fail();
        }
    }
}