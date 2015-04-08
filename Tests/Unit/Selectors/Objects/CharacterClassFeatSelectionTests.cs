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

            characterClass.ClassName = "class";
            characterClass.Level = 1;
        }

        [Test]
        public void SelectionIsInitialized()
        {
            Assert.That(selection.FeatName, Is.Empty);
            Assert.That(selection.LevelRequirements, Is.Empty);
            Assert.That(selection.Strength, Is.EqualTo(0));
        }

        [Test]
        public void RequirementsMetIfCharacterClassCanHaveFeat()
        {
            selection.LevelRequirements["class"] = 1;
            var requirementsMet = selection.RequirementsSatisfied(characterClass);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfCharacterClassCannotHaveFeat()
        {
            selection.LevelRequirements["other class"] = 1;
            var requirementsMet = selection.RequirementsSatisfied(characterClass);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void RequirementsMetIfCharacterClassIsSufficientLevel()
        {
            selection.LevelRequirements["class"] = 2;
            characterClass.Level = 2;

            var requirementsMet = selection.RequirementsSatisfied(characterClass);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfCharacterClassIsNotSufficientLevel()
        {
            selection.LevelRequirements["class"] = 2;
            characterClass.Level = 1;

            var requirementsMet = selection.RequirementsSatisfied(characterClass);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void RequirementsMetIfSpecialistClassMatches()
        {
            selection.LevelRequirements["specialist"] = 0;
            characterClass.SpecialistFields = new[] { "specialist" };

            var requirementsMet = selection.RequirementsSatisfied(characterClass);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfSpecialistClassDoesNotMatch()
        {
            selection.LevelRequirements["other specialist"] = 0;
            characterClass.SpecialistFields = new[] { "specialist" };

            var requirementsMet = selection.RequirementsSatisfied(characterClass);
            Assert.That(requirementsMet, Is.False);
        }
    }
}