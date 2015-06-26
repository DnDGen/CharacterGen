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
        }

        [Test]
        public void GetClassFeatsForClass()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;

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
        }

        [Test]
        public void GetClassFeatForSpecializations()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetAllClassFeats()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetClassFeatsWithMatchingLevelRequirement()
        {
            characterClass.ClassName = "class name";
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
            characterClass.ClassName = "class name";
            characterClass.Level = 1;

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetClassFeatsIfNoneMatchRequirements()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 1;

            AddClassFeat(characterClass.ClassName, "feat 2", minimumLevel: 2);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetFeatWithSpecializationFromSpecialistFields()
        {
            var weapons = new[] { "battleaxe" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "weapon")).Returns(weapons);

            characterClass.ClassName = "class name";
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