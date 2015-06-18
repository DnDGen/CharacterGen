using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities;
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
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private Mock<IDice> mockDice;
        private List<CharacterClassFeatSelection> classFeatSelections;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockDice = new Mock<IDice>();
            classFeatsGenerator = new ClassFeatsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockFeatsSelector.Object, mockDice.Object, mockNameSelector.Object);
            characterClass = new CharacterClass();
            stats = new Dictionary<String, Stat>();
            stats[StatConstants.Intelligence] = new Stat();
            classFeatSelections = new List<CharacterClassFeatSelection>();

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
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("never"));

            Assert.That(last.Name.Id, Is.EqualTo("class feat 2"));
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(600));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetClassFeatForSpecializations()
        {
            Assert.Fail();
        }

        [Test]
        public void GetAllClassFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void GetClassFeatsWithMatchingLevelRequirement()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;

            AddClassFeat("feat 1");
            AddClassFeat("feat 2", minimumLevel: 2);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("feat 2"));
        }

        private void AddClassFeat(String featId, String focusType = "", Int32 minimumLevel = 1)
        {
            var selection = new CharacterClassFeatSelection();
            selection.FeatId = featId;
            selection.FocusType = focusType;
            selection.MinimumLevel = minimumLevel;


            classFeatSelections.Add(selection);
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
            AddFeatSelections(1);

            characterClass.ClassName = "class name";
            characterClass.Level = 2;

            AddClassFeat("class feat 1", "other class", 1, 1);
            AddClassFeat("class feat 2", characterClass.ClassName, 3, 1);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Is.Not.Contains("class feat 1"));
            Assert.That(featNames, Is.Not.Contains("class feat 2"));
        }

        [Test]
        public void GetFeatFromSpecialistFields()
        {
            AddFeatSelections(2);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("feat 1", "specialist", 0, 0);
            AddClassFeat("feat 2", "specialist", 0, 0);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("feat 2"));
        }

        [Test]
        public void GetFeatWithSpecializationFromSpecialistFields()
        {
            AddFeatSelections(1);

            var weapons = new[] { "battleaxe" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "weapon")).Returns(weapons);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("class feat", "specialist", 0, 0);
            additionalFeatSelections[1].FocusType = "weapon";

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var feat = feats.First(f => f.Name.Id == "class feat");

            Assert.That(feat.Focus, Is.EqualTo("battleaxe"));
        }

        [Test]
        public void GetFeatWithSpecializationAndPrerequisiteFromSpecialistFields()
        {
            AddFeatSelections(1);

            var weapons = new[] { "battleaxe", "kitten", "stick" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "weapon")).Returns(weapons);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("class feat 1", "specialist", 0, 0);
            AddClassFeat("class feat 2", "specialist", 0, 0);
            additionalFeatSelections[1].FocusType = "weapon";
            additionalFeatSelections[2].RequiredFeatIds = new[] { additionalFeatSelections[1].Name.Id };
            additionalFeatSelections[2].FocusType = "weapon";

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var firstFeat = feats.First(f => f.Name.Id == "class feat 1");
            var secondFeat = feats.Last(f => f.Name.Id == "class feat 2");

            Assert.That(firstFeat.Focus, Is.EqualTo("stick"));
            Assert.That(secondFeat.Focus, Is.EqualTo("stick"));
        }

        [Test]
        public void DoNotGetFeatFromSpecialistFieldsIfNoneThatMatchRequirements()
        {
            AddFeatSelections(1);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("class feat 1", "other specialist", 0, 0);
            AddClassFeat("class feat 2", "other class", 1, 0);

            var feats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Is.Not.Contains("class feat 1"));
            Assert.That(featNames, Is.Not.Contains("class feat 2"));
        }
    }
}