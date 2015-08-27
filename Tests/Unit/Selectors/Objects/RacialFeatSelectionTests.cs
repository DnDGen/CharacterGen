using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Selectors.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class RacialFeatSelectionTests
    {
        private RacialFeatSelection selection;
        private Race race;
        private Dictionary<String, Stat> stats;

        [SetUp]
        public void Setup()
        {
            selection = new RacialFeatSelection();
            race = new Race();
            stats = new Dictionary<String, Stat>();

            stats["stat"] = new Stat { Value = 42 };
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
            Assert.That(selection.RequiredStat, Is.Empty);
            Assert.That(selection.RequiredStatMinimumValue, Is.EqualTo(0));
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
            selection.RequiredStat = "stat";
            selection.RequiredStatMinimumValue = 9266;

            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AllRequirementsMet()
        {
            race.Size = "big";

            selection.SizeRequirement = race.Size;
            selection.MinimumHitDieRequirement = 3;
            selection.MaximumHitDieRequirement = 5;
            selection.RequiredStat = "stat";
            selection.RequiredStatMinimumValue = 42;

            var met = selection.RequirementsMet(race, 4, stats);
            Assert.That(met, Is.True);
        }
    }
}