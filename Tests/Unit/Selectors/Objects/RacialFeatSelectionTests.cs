using System;
using System.Linq;
using NPCGen.Common.Races;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
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
            Assert.That(selection.BaseRaceRequirements, Is.Empty);
            Assert.That(selection.FeatName, Is.Empty);
            Assert.That(selection.FeatStrength, Is.EqualTo(0));
            Assert.That(selection.HitDieRequirements, Is.Empty);
            Assert.That(selection.MetaraceRequirements, Is.Empty);
            Assert.That(selection.MetaraceSpeciesRequirements, Is.Empty);
            Assert.That(selection.SizeRequirement, Is.Empty);
        }

        [Test]
        public void RequirementsMetIfNoRequirements()
        {
            var met = selection.RequirementsMet(race, 0);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfWrongBaseRace()
        {
            race.BaseRace.Id = "baserace";
            selection.BaseRaceRequirements = new[] { "otherbaserace" };

            var met = selection.RequirementsMet(race, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsNotMetIfWrongMetarace()
        {
            race.Metarace.Id = "metarace";
            selection.MetaraceRequirements = new[] { "othermetarace" };

            var met = selection.RequirementsMet(race, 0);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsNotMetIfWrongMetaraceSpecies()
        {
            race.MetaraceSpecies = "metarace species";
            selection.MetaraceRequirements = new[] { "othermetaracespecies" };

            var met = selection.RequirementsMet(race, 0);
            Assert.That(met, Is.False);
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
            selection.HitDieRequirements = Enumerable.Range(3, 3);

            var met = selection.RequirementsMet(race, 2);
            Assert.That(met, Is.False);
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void RequirementsMetIfInHitDiceRange(Int32 hitDice)
        {
            selection.HitDieRequirements = Enumerable.Range(3, 3);

            var met = selection.RequirementsMet(race, hitDice);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfAboveHitDiceRange()
        {
            selection.HitDieRequirements = Enumerable.Range(3, 3);

            var met = selection.RequirementsMet(race, 6);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AllRequirementsMet()
        {
            race.BaseRace.Id = "baserace";
            race.Metarace.Id = "metarace";
            race.MetaraceSpecies = "metarace species";
            race.Size = "big";

            selection.BaseRaceRequirements = new[] { race.BaseRace.Id, "other" };
            selection.MetaraceRequirements = new[] { race.Metarace.Id, "other" };
            selection.MetaraceSpeciesRequirements = new[] { race.MetaraceSpecies, "other" };
            selection.SizeRequirement = race.Size;
            selection.HitDieRequirements = Enumerable.Range(3, 3);

            var met = selection.RequirementsMet(race, 4);
            Assert.That(met, Is.True);
        }
    }
}