using System;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Selectors.Objects;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Selectors.Objects
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
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Frequency, Is.Not.Null);
            Assert.That(selection.MinimumLevel, Is.EqualTo(0));
            Assert.That(selection.RequiredFeats, Is.Empty);
            Assert.That(selection.Strength, Is.EqualTo(0));
            Assert.That(selection.MaximumLevel, Is.EqualTo(0));
            Assert.That(selection.FrequencyQuantityStat, Is.Empty);
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
    }
}