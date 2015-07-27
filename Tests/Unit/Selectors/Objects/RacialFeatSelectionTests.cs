using CharacterGen.Common.Races;
using CharacterGen.Selectors.Objects;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class RacialFeatSelectionTests
    {
        private RacialFeatSelection selection;
        private Race race;

        [SetUp]
        public void Setup()
        {
            selection = new RacialFeatSelection();
            race = new Race();
        }

        [Test]
        public void RacialFeatSelectionInitialization()
        {
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.Strength, Is.EqualTo(0));
            Assert.That(selection.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(selection.SizeRequirement, Is.Empty);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Frequency, Is.Not.Null);
            Assert.That(selection.MaximumHitDieRequirement, Is.EqualTo(0));
        }

        [Test]
        public void RequirementsMetIfNoRequirements()
        {
            var met = selection.RequirementsMet(race, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfWrongSize()
        {
            race.Size = "big";
            selection.SizeRequirement = "small";

            var met = selection.RequirementsMet(race, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsNotMetIfBelowHitDiceRange()
        {
            selection.MinimumHitDieRequirement = 3;

            var met = selection.RequirementsMet(race, 2);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsNotMetIfAboveHitDiceRange()
        {
            selection.MaximumHitDieRequirement = 3;

            var met = selection.RequirementsMet(race, 4);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfAboveMaximumOfZero()
        {
            var met = selection.RequirementsMet(race, 4);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetForMinimumHitDice()
        {
            selection.MinimumHitDieRequirement = 4;

            var met = selection.RequirementsMet(race, 4);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetForMaximumHitDice()
        {
            selection.MaximumHitDieRequirement = 4;

            var met = selection.RequirementsMet(race, 4);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AllRequirementsMet()
        {
            race.Size = "big";

            selection.SizeRequirement = race.Size;
            selection.MinimumHitDieRequirement = 3;
            selection.MaximumHitDieRequirement = 5;

            var met = selection.RequirementsMet(race, 4);
            Assert.That(met, Is.True);
        }
    }
}