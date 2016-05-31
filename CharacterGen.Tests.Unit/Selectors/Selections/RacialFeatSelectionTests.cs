using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Selectors.Selections;
using CharacterGen.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class RacialFeatSelectionTests
    {
        private RacialFeatSelection selection;
        private Race race;
        private Dictionary<string, Stat> stats;

        [SetUp]
        public void Setup()
        {
            selection = new RacialFeatSelection();
            race = new Race();
            stats = new Dictionary<string, Stat>();

            stats["stat"] = new Stat { Value = 42 };
        }

        [Test]
        public void RacialFeatSelectionInitialization()
        {
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.Power, Is.EqualTo(0));
            Assert.That(selection.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(selection.SizeRequirement, Is.Empty);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Frequency, Is.Not.Null);
            Assert.That(selection.MaximumHitDieRequirement, Is.EqualTo(0));
            Assert.That(selection.MinimumStats, Is.Empty);
        }

        [Test]
        public void RequirementsMetIfNoRequirements()
        {
            var met = selection.RequirementsMet(race, 0, stats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfWrongSize()
        {
            race.Size = "big";
            selection.SizeRequirement = "small";

            var met = selection.RequirementsMet(race, 0, stats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsNotMetIfBelowHitDiceRange()
        {
            selection.MinimumHitDieRequirement = 3;

            var met = selection.RequirementsMet(race, 2, stats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsNotMetIfAboveHitDiceRange()
        {
            selection.MaximumHitDieRequirement = 3;

            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfAboveMaximumOfZero()
        {
            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetForMinimumHitDice()
        {
            selection.MinimumHitDieRequirement = 4;

            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetForMaximumHitDice()
        {
            selection.MaximumHitDieRequirement = 4;

            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfDoesNotHaveMinimumStat()
        {
            selection.MinimumStats["stat"] = 9266;

            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfAnyMinimumStatIsMet()
        {
            selection.MinimumStats["stat"] = 9266;
            selection.MinimumStats["stat 2"] = 600;

            stats["stat 2"] = new Stat { Value = 600 };

            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AllRequirementsMet()
        {
            race.Size = "big";

            selection.SizeRequirement = race.Size;
            selection.MinimumHitDieRequirement = 3;
            selection.MaximumHitDieRequirement = 5;
            selection.MinimumStats["stat"] = 42;

            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.True);
        }
    }
}