using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Races;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
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

            race.BaseRace.Id = "base race";
            race.Metarace.Id = "metarace";
            defaultAdjustments[race.BaseRace.Id] = 0;
            defaultAdjustments[race.Metarace.Id] = 0;
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

            allAdjustments[StatConstants.Charisma][race.BaseRace.Id] = 9266;
            allAdjustments[StatConstants.Charisma][race.Metarace.Id] = 42;
            allAdjustments[StatConstants.Constitution][race.BaseRace.Id] = 90210;
            allAdjustments[StatConstants.Constitution][race.Metarace.Id] = -9266;
            allAdjustments[StatConstants.Dexterity][race.BaseRace.Id] = -42;
            allAdjustments[StatConstants.Dexterity][race.Metarace.Id] = -90210;
            allAdjustments[StatConstants.Intelligence][race.BaseRace.Id] = 92;
            allAdjustments[StatConstants.Intelligence][race.Metarace.Id] = 66;
            allAdjustments[StatConstants.Strength][race.BaseRace.Id] = 600;
            allAdjustments[StatConstants.Strength][race.Metarace.Id] = -92;
            allAdjustments[StatConstants.Wisdom][race.BaseRace.Id] = -66;
            allAdjustments[StatConstants.Wisdom][race.Metarace.Id] = -400;

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