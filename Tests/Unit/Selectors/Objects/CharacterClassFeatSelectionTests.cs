using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class CharacterClassFeatSelectionTests
    {
        private CharacterClassFeatSelection selection;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            selection = new CharacterClassFeatSelection();
            characterClass = new CharacterClass();
        }

        [Test]
        public void SelectionIsInitialized()
        {
            Assert.That(selection.FeatId, Is.Empty);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Frequency, Is.Not.Null);
            Assert.That(selection.MinimumLevel, Is.EqualTo(0));
            Assert.That(selection.RequiredFeatIds, Is.Empty);
            Assert.That(selection.Strength, Is.EqualTo(0));
            Assert.That(selection.MaximumLevel, Is.EqualTo(0));
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        public void CharacterLevelMustBeInBoundsToMeetRequirement(Int32 characterLevel, Boolean met)
        {
            characterClass.Level = characterLevel;
            selection.MaximumLevel = 4;
            selection.MinimumLevel = 2;

            var requirementsMet = selection.RequirementsMet(characterClass);
            Assert.That(requirementsMet, Is.EqualTo(met));
        }

        [Test]
        public void RequirementsMetWithNoMaximumLevel()
        {
            characterClass.Level = 5;
            selection.MinimumLevel = 2;

            var requirementsMet = selection.RequirementsMet(characterClass);
            Assert.That(requirementsMet, Is.True);
        }

        [TestCase(CharacterClassFeatSelection.FeatIdIndex, 0)]
        [TestCase(CharacterClassFeatSelection.MinimumLevelRequirementIndex, 1)]
        [TestCase(CharacterClassFeatSelection.FocusTypeIndex, 2)]
        [TestCase(CharacterClassFeatSelection.StrengthIndex, 3)]
        [TestCase(CharacterClassFeatSelection.FrequencyQuantityIndex, 4)]
        [TestCase(CharacterClassFeatSelection.FrequencyTimePeriodIndex, 5)]
        [TestCase(CharacterClassFeatSelection.MaximumLevelRequirementIndex, 6)]
        public void IndexConstant(Int32 constant, Int32 value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}