using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class ClassFeatsGeneratorTests
    {
        private IClassFeatsGenerator classFeatsGenerator;
        private CharacterClass characterClass;
        private Dictionary<String, Stat> stats;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private Mock<IDice> mockDice;
        private Dictionary<String, List<CharacterClassFeatSelection>> classFeatSelections;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockDice = new Mock<IDice>();
            classFeatsGenerator = new ClassFeatsGenerator(mockCollectionsSelector.Object, mockFeatsSelector.Object, mockDice.Object);
            characterClass = new CharacterClass();
            stats = new Dictionary<String, Stat>();
            stats[StatConstants.Intelligence] = new Stat();
            classFeatSelections = new Dictionary<String, List<CharacterClassFeatSelection>>();

            mockFeatsSelector.Setup(s => s.SelectClass(It.IsAny<String>())).Returns(Enumerable.Empty<CharacterClassFeatSelection>());
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);

            characterClass.ClassName = "class name";
            characterClass.Level = 1;
        }

        [Test]
        public void GetClassFeatsForClass()
        {
            var classFeats = new[]
            {
                new CharacterClassFeatSelection { FeatId = "class feat 1", Strength = 9266, Frequency = new Frequency { Quantity = 0, TimePeriod = "constant" } },
                new CharacterClassFeatSelection { FeatId = "class feat 2", Frequency = new Frequency { Quantity = 600, TimePeriod = "fortnight" } }
            };

            mockFeatsSelector.Setup(s => s.SelectClass("class name")).Returns(classFeats);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("class feat 1"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("constant"));

            Assert.That(last.Name.Id, Is.EqualTo("class feat 2"));
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(600));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetClassFeatsForSpecializations()
        {
            characterClass.SpecialistFields = new[] { "specialist 1", "specialist 2" };

            var specialist1Feats = new[]
            {
                new CharacterClassFeatSelection { FeatId = "specialist feat 1", Strength = 9266 },
                new CharacterClassFeatSelection { FeatId = "specialist feat 2", Frequency = new Frequency { Quantity = 600, TimePeriod = "fortnight" } }
            };

            var specialist2Feats = new[]
            {
                new CharacterClassFeatSelection { FeatId = "specialist feat 3", Strength = 42 }
            };

            mockFeatsSelector.Setup(s => s.SelectClass("specialist 1")).Returns(specialist1Feats);
            mockFeatsSelector.Setup(s => s.SelectClass("specialist 2")).Returns(specialist2Feats);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var feat1 = feats.First(f => f.Name.Id == "specialist feat 1");
            var feat2 = feats.First(f => f.Name.Id == "specialist feat 2");
            var feat3 = feats.First(f => f.Name.Id == "specialist feat 3");

            Assert.That(feat1.Name.Id, Is.EqualTo("specialist feat 1"));
            Assert.That(feat1.Strength, Is.EqualTo(9266));
            Assert.That(feat1.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat1.Frequency.TimePeriod, Is.Empty);

            Assert.That(feat2.Name.Id, Is.EqualTo("specialist feat 2"));
            Assert.That(feat2.Strength, Is.EqualTo(0));
            Assert.That(feat2.Frequency.Quantity, Is.EqualTo(600));
            Assert.That(feat2.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feat3.Name.Id, Is.EqualTo("specialist feat 3"));
            Assert.That(feat3.Strength, Is.EqualTo(42));
            Assert.That(feat3.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(feat3.Frequency.TimePeriod, Is.Empty);

            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GetAllClassFeats()
        {
            characterClass.SpecialistFields = new[] { "specialist" };

            var classFeats = new[]
            {
                new CharacterClassFeatSelection { FeatId = "class feat", Strength = 9266 }
            };

            var specialistFeats = new[]
            {
                new CharacterClassFeatSelection { FeatId = "specialist feat", Strength = 42 }
            };

            mockFeatsSelector.Setup(s => s.SelectClass("class name")).Returns(classFeats);
            mockFeatsSelector.Setup(s => s.SelectClass("specialist")).Returns(specialistFeats);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetClassFeatsWithMatchingLevelRequirement()
        {
            characterClass.Level = 2;

            AddClassFeat(characterClass.ClassName, "feat 1");
            AddClassFeat(characterClass.ClassName, "feat 2", minimumLevel: 2);
            AddClassFeat(characterClass.ClassName, "feat 3", minimumLevel: 3);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("feat 2"));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        private void AddClassFeat(String className, String featId, String focusType = "", Int32 minimumLevel = 1, params String[] requiredFeatIds)
        {
            var selection = new CharacterClassFeatSelection();
            selection.FeatId = featId;
            selection.FocusType = focusType;
            selection.MinimumLevel = minimumLevel;
            selection.RequiredFeatIds = requiredFeatIds;

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
            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetClassFeatsIfNoneMatchRequirements()
        {
            AddClassFeat(characterClass.ClassName, "feat 2", minimumLevel: 2);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetFeatWithSpecializationFromSpecialistFields()
        {
            var weapons = new[] { "battleaxe" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "weapon")).Returns(weapons);

            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("specialist", "class feat", "weapon");

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var feat = feats.First(f => f.Name.Id == "class feat");

            Assert.That(feat.Focus, Is.EqualTo("battleaxe"));
        }

        [Test]
        public void GetFeatWithSpecializationAndPrerequisiteFromSpecialistFields()
        {
            var weapons = new[] { "battleaxe", "kitten", "stick" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "weapon")).Returns(weapons);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("specialist", "class feat 1", "weapon");
            AddClassFeat("specialist", "class feat 2", "weapon", requiredFeatIds: new[] { "class feat 1" });

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var firstFeat = feats.First(f => f.Name.Id == "class feat 1");
            var secondFeat = feats.Last(f => f.Name.Id == "class feat 2");

            Assert.That(firstFeat.Focus, Is.EqualTo("stick"));
            Assert.That(secondFeat.Focus, Is.EqualTo("stick"));
        }
    }
}