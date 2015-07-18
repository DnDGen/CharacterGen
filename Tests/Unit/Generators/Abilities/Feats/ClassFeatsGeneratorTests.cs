using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class ClassFeatsGeneratorTests
    {
        private Mock<IFeatsSelector> mockFeatsSelector;
        private Mock<IFeatFocusGenerator> mockFeatFocusGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private IClassFeatsGenerator classFeatsGenerator;
        private CharacterClass characterClass;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, List<CharacterClassFeatSelection>> classFeatSelections;
        private List<Feat> racialFeats;
        private Dictionary<String, Skill> skills;

        [SetUp]
        public void Setup()
        {
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockFeatFocusGenerator = new Mock<IFeatFocusGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            classFeatsGenerator = new ClassFeatsGenerator(mockFeatsSelector.Object, mockFeatFocusGenerator.Object, mockCollectionsSelector.Object);
            characterClass = new CharacterClass();
            stats = new Dictionary<String, Stat>();
            stats[StatConstants.Intelligence] = new Stat();
            classFeatSelections = new Dictionary<String, List<CharacterClassFeatSelection>>();
            racialFeats = new List<Feat>();
            skills = new Dictionary<String, Skill>();

            mockFeatsSelector.Setup(s => s.SelectClass(It.IsAny<String>())).Returns(Enumerable.Empty<CharacterClassFeatSelection>());

            characterClass.ClassName = "class name";
            characterClass.Level = 1;
        }

        [Test]
        public void GetClassFeatsForClass()
        {
            AddClassFeat(characterClass.ClassName, "class feat 1", strength: 9266);
            AddClassFeat(characterClass.ClassName, "class feat 2", frequencyPeriod: "fortnight", frequencyQuantity: 600);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("class feat 1"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("class feat 2"));
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(600));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetClassFeatsForSpecializations()
        {
            characterClass.SpecialistFields = new[] { "specialist 1", "specialist 2" };

            AddClassFeat("specialist 1", "specialist feat 1", strength: 9266);
            AddClassFeat("specialist 1", "specialist feat 2", frequencyPeriod: "fortnight", frequencyQuantity: 600);
            AddClassFeat("specialist 2", "specialist feat 3", strength: 42);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var feat1 = feats.First(f => f.Name == "specialist feat 1");
            var feat2 = feats.First(f => f.Name == "specialist feat 2");
            var feat3 = feats.First(f => f.Name == "specialist feat 3");

            Assert.That(feat1.Name, Is.EqualTo("specialist feat 1"));
            Assert.That(feat1.Strength, Is.EqualTo(9266));
            Assert.That(feat1.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat1.Frequency.TimePeriod, Is.Empty);

            Assert.That(feat2.Name, Is.EqualTo("specialist feat 2"));
            Assert.That(feat2.Strength, Is.EqualTo(0));
            Assert.That(feat2.Frequency.Quantity, Is.EqualTo(600));
            Assert.That(feat2.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feat3.Name, Is.EqualTo("specialist feat 3"));
            Assert.That(feat3.Strength, Is.EqualTo(42));
            Assert.That(feat3.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat3.Frequency.TimePeriod, Is.Empty);

            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GetAllClassFeats()
        {
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat(characterClass.ClassName, "class feat", strength: 9266);
            AddClassFeat("specialist", "specialist feat", strength: 42);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetClassFeatsWithMatchingRequirements()
        {
            characterClass.Level = 2;

            AddClassFeat(characterClass.ClassName, "feat 1");
            AddClassFeat(characterClass.ClassName, "feat 2", minimumLevel: 2);
            AddClassFeat(characterClass.ClassName, "feat 3", minimumLevel: 3);
            AddClassFeat(characterClass.ClassName, "feat 4", maximumLevel: 1);
            AddClassFeat(characterClass.ClassName, "feat 5", maximumLevel: 2);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("feat 2"));
            Assert.That(featIds, Contains.Item("feat 5"));
            Assert.That(featIds.Count(), Is.EqualTo(3));
        }

        private void AddClassFeat(String className, String feat, String focusType = "", Int32 minimumLevel = 1, Int32 maximumLevel = 0, Int32 frequencyQuantity = 0, String frequencyPeriod = "", Int32 strength = 0, params String[] requiredFeatIds)
        {
            var selection = new CharacterClassFeatSelection();
            selection.Feat = feat;
            selection.FocusType = focusType;
            selection.MinimumLevel = minimumLevel;
            selection.RequiredFeats = requiredFeatIds.Select(f => new RequiredFeat { Feat = f });
            selection.Frequency.Quantity = frequencyQuantity;
            selection.Frequency.TimePeriod = frequencyPeriod;
            selection.MaximumLevel = maximumLevel;
            selection.Strength = strength;

            if (classFeatSelections.ContainsKey(className) == false)
            {
                classFeatSelections[className] = new List<CharacterClassFeatSelection>();
                mockFeatsSelector.Setup(s => s.SelectClass(className)).Returns(classFeatSelections[className]);
            }

            classFeatSelections[className].Add(selection);
        }

        [Test]
        public void DoNotGetClassFeatsIfNone()
        {
            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetClassFeatsIfNoneMatchRequirements()
        {
            characterClass.Level = 2;

            AddClassFeat(characterClass.ClassName, "feat 2", minimumLevel: 3);
            AddClassFeat(characterClass.ClassName, "feat 2", maximumLevel: 1);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void FeatFociAreFilled()
        {
            AddClassFeat(characterClass.ClassName, "feat1", focusType: "focus type");
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat1", "focus type", skills, classFeatSelections[characterClass.ClassName][0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus");

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(classFeatSelections[characterClass.ClassName][0].Feat));
            Assert.That(onlyFeat.Focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FeatFociAreFilledIndividually()
        {
            AddClassFeat(characterClass.ClassName, "feat1", focusType: "focus type");
            AddClassFeat(characterClass.ClassName, "feat2");

            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat1", "focus type", skills, classFeatSelections[characterClass.ClassName][0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus");
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat2", String.Empty, skills, classFeatSelections[characterClass.ClassName][1].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns(String.Empty);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo(classFeatSelections[characterClass.ClassName][0].Feat));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(last.Name, Is.EqualTo(classFeatSelections[characterClass.ClassName][1].Feat));
            Assert.That(last.Focus, Is.Empty);
            mockFeatFocusGenerator.Verify(g => g.GenerateAllowingFocusOfAllFrom(It.IsAny<String>(), It.IsAny<String>(), skills, It.IsAny<IEnumerable<RequiredFeat>>(), It.IsAny<IEnumerable<Feat>>(), It.IsAny<CharacterClass>()), Times.Exactly(2));
            mockFeatFocusGenerator.Verify(g => g.GenerateAllowingFocusOfAllFrom("feat1", "focus type", skills, classFeatSelections[characterClass.ClassName][0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass), Times.Once);
            mockFeatFocusGenerator.Verify(g => g.GenerateAllowingFocusOfAllFrom("feat2", String.Empty, skills, classFeatSelections[characterClass.ClassName][1].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass), Times.Once);
        }

        [Test]
        public void FeatsWithFociCanBeFilledMoreThanOnce()
        {
            characterClass.Level = 2;
            AddClassFeat(characterClass.ClassName, "feat1", focusType: "focus type");
            AddClassFeat(characterClass.ClassName, "feat1", focusType: "focus type", minimumLevel: 2);

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateAllowingFocusOfAllFrom("feat1", "focus type", skills, classFeatSelections[characterClass.ClassName][0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus 1").Returns("focus 2");

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat1"));
            Assert.That(firstFeat.Focus, Is.EqualTo("focus 1"));
            Assert.That(lastFeat.Name, Is.EqualTo("feat1"));
            Assert.That(lastFeat.Focus, Is.EqualTo("focus 2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HonorFociInClassRequirements()
        {
            AddClassFeat(characterClass.ClassName, "feat1", focusType: "focus type");
            AddClassFeat(characterClass.ClassName, "feat2", focusType: "focus type", requiredFeatIds: "feat1");

            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat1", "focus type", skills, classFeatSelections[characterClass.ClassName][0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus 1");
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat2", "focus type", skills, classFeatSelections[characterClass.ClassName][1].RequiredFeats, It.Is<IEnumerable<Feat>>(fs => fs.Any(f => f.Name == "feat1")), characterClass))
                .Returns("focus 1");

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat1"));
            Assert.That(firstFeat.Focus, Is.EqualTo("focus 1"));
            Assert.That(lastFeat.Name, Is.EqualTo("feat2"));
            Assert.That(lastFeat.Focus, Is.EqualTo("focus 1"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void StatBasedFrequenciesAreSet()
        {
            AddClassFeat(characterClass.ClassName, "feat1", frequencyQuantity: 1);
            classFeatSelections[characterClass.ClassName][0].FrequencyQuantityStat = "stat";

            stats["stat"] = new Stat();
            stats["stat"].Value = 15;

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var onlyFeat = feats.Single();
            Assert.That(onlyFeat.Frequency.Quantity, Is.EqualTo(3));
        }

        [TestCase(1, 2)]
        [TestCase(2, 4)]
        [TestCase(3, 6)]
        [TestCase(4, 8)]
        [TestCase(5, 10)]
        public void RangerImprovesStrengthOfFavoredEnemyFeat(Int32 numberOfFavoredEnemies, Int32 strength)
        {
            characterClass.ClassName = CharacterClassConstants.Ranger;

            while (numberOfFavoredEnemies-- > 0)
                AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, strength: 2);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var feat1 = feats.First();
            Assert.That(feat1.Strength, Is.EqualTo(strength));
        }

        [Test]
        public void RangerFavoredEnemyImprovementsAreRandomForMultipleFavoredEnemies()
        {
            characterClass.ClassName = CharacterClassConstants.Ranger;

            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<Feat>>(fs => fs.All(f => f.Name == FeatConstants.FavoredEnemy))))
                .Returns("focus 2").Returns("focus 2").Returns("focus 5").Returns("focus 1");

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateAllowingFocusOfAllFrom(FeatConstants.FavoredEnemy, "focus type", skills, It.IsAny<IEnumerable<RequiredFeat>>(), It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus 1").Returns("focus 2").Returns("focus 3").Returns("focus 4").Returns("focus 5");

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            Assert.That(feats.First(f => f.Focus == "focus 1").Strength, Is.EqualTo(4));
            Assert.That(feats.First(f => f.Focus == "focus 2").Strength, Is.EqualTo(6));
            Assert.That(feats.First(f => f.Focus == "focus 3").Strength, Is.EqualTo(2));
            Assert.That(feats.First(f => f.Focus == "focus 4").Strength, Is.EqualTo(2));
            Assert.That(feats.First(f => f.Focus == "focus 5").Strength, Is.EqualTo(4));
        }

        [Test]
        public void NonRangersDoNotGetToImproveFavoredEnemies()
        {
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);
            AddClassFeat(characterClass.ClassName, FeatConstants.FavoredEnemy, focusType: "focus type", strength: 2);

            mockDice.SetupSequence(d => d.Roll(1).d(5)).Returns(2).Returns(2).Returns(5).Returns(1);

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateAllowingFocusOfAllFrom(FeatConstants.FavoredEnemy, "focus type", skills, It.IsAny<IEnumerable<RequiredFeat>>(), It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus 1").Returns("focus 2").Returns("focus 3").Returns("focus 4").Returns("focus 5");

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            Assert.That(feats.First(f => f.Focus == "focus 1").Strength, Is.EqualTo(2));
            Assert.That(feats.First(f => f.Focus == "focus 2").Strength, Is.EqualTo(2));
            Assert.That(feats.First(f => f.Focus == "focus 3").Strength, Is.EqualTo(2));
            Assert.That(feats.First(f => f.Focus == "focus 4").Strength, Is.EqualTo(2));
            Assert.That(feats.First(f => f.Focus == "focus 5").Strength, Is.EqualTo(2));
        }

        [Test]
        public void RacialFeatAreNotAltered()
        {
            AddClassFeat(characterClass.ClassName, "class feat 1", strength: 9266);
            AddClassFeat(characterClass.ClassName, "class feat 2", frequencyPeriod: "fortnight", frequencyQuantity: 600);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            Assert.That(racialFeats, Is.Empty);
        }
    }
}