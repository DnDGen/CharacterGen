using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class StatAdjustmentsSelectorTests
    {
        private IStatAdjustmentsSelector selector;
        private Race race;
        private Mock<IAdjustmentsSelector> mockInnerSelector;
        private Dictionary<String, Int32> defaultAdjustments;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<IAdjustmentsSelector>();
            selector = new StatAdjustmentsSelector(mockInnerSelector.Object);
            race = new Race();
            defaultAdjustments = new Dictionary<String, Int32>();

            race.BaseRace = "base race";
            race.Metarace = "metarace";
            defaultAdjustments[race.BaseRace] = 0;
            defaultAdjustments[race.Metarace] = 0;
            mockInnerSelector.Setup(s => s.SelectFrom(It.IsAny<String>())).Returns(defaultAdjustments);
        }

        [Test]
        public void AdjustmentsContainAllStats()
        {
            var adjustments = selector.SelectFor(race);

            foreach (var stat in StatConstants.GetStats())
                Assert.That(adjustments.Keys, Contains.Item(stat));
        }

        [Test]
        public void EachStatAdjustmentIsIndividual()
        {
            var allAdjustments = new Dictionary<String, Dictionary<String, Int32>>();
            foreach (var stat in StatConstants.GetStats())
                allAdjustments[stat] = new Dictionary<String, Int32>();

            allAdjustments[StatConstants.Charisma][race.BaseRace] = 9266;
            allAdjustments[StatConstants.Charisma][race.Metarace] = 42;
            allAdjustments[StatConstants.Constitution][race.BaseRace] = 90210;
            allAdjustments[StatConstants.Constitution][race.Metarace] = -9266;
            allAdjustments[StatConstants.Dexterity][race.BaseRace] = -42;
            allAdjustments[StatConstants.Dexterity][race.Metarace] = -90210;
            allAdjustments[StatConstants.Intelligence][race.BaseRace] = 92;
            allAdjustments[StatConstants.Intelligence][race.Metarace] = 66;
            allAdjustments[StatConstants.Strength][race.BaseRace] = 600;
            allAdjustments[StatConstants.Strength][race.Metarace] = -92;
            allAdjustments[StatConstants.Wisdom][race.BaseRace] = -66;
            allAdjustments[StatConstants.Wisdom][race.Metarace] = -400;

            foreach (var stat in StatConstants.GetStats())
            {
                var tableName = String.Format("{0}StatAdjustments", stat);
                mockInnerSelector.Setup(s => s.SelectFrom(tableName)).Returns(allAdjustments[stat]);
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments[StatConstants.Charisma], Is.EqualTo(9308));
            Assert.That(adjustments[StatConstants.Constitution], Is.EqualTo(80944));
            Assert.That(adjustments[StatConstants.Dexterity], Is.EqualTo(-90252));
            Assert.That(adjustments[StatConstants.Intelligence], Is.EqualTo(158));
            Assert.That(adjustments[StatConstants.Strength], Is.EqualTo(508));
            Assert.That(adjustments[StatConstants.Wisdom], Is.EqualTo(-466));
        }
    }
}