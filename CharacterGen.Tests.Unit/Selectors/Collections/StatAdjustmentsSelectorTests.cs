using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class StatAdjustmentsSelectorTests
    {
        private IStatAdjustmentsSelector selector;
        private Race race;
        private Mock<IAdjustmentsSelector> mockInnerSelector;
        private Dictionary<string, Dictionary<string, int>> allAdjustments;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private List<string> statNames;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            selector = new StatAdjustmentsSelector(mockInnerSelector.Object, mockCollectionsSelector.Object);
            race = new Race();
            allAdjustments = new Dictionary<string, Dictionary<string, int>>();
            statNames = new List<string>();

            race.BaseRace = "base race";
            race.Metarace = "metarace";
            race.Age.Description = "super old";

            allAdjustments[race.Age.Description] = new Dictionary<string, int>();

            statNames.Add("first stat");
            statNames.Add("second stat");

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.AGEStatAdjustments, race.Age.Description);
            mockInnerSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(allAdjustments[race.Age.Description]);

            foreach (var stat in statNames)
            {
                allAdjustments[stat] = new Dictionary<string, int>();
                allAdjustments[stat][race.BaseRace] = 0;
                allAdjustments[stat][race.Metarace] = 0;
                allAdjustments[race.Age.Description][stat] = 0;

                tableName = string.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, stat);
                mockInnerSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(allAdjustments[stat]);
            }

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.StatGroups, GroupConstants.All)).Returns(statNames);
        }

        [Test]
        public void AdjustmentsContainAllStats()
        {
            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(statNames));
        }

        [Test]
        public void ApplyBaseRaceAdjustmentsToStatAdjustments()
        {
            var adjustment = 1;
            foreach (var stat in statNames)
            {
                allAdjustments[stat][race.BaseRace] = adjustment++;
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(statNames));

            for (var i = 0; i < statNames.Count; i++)
            {
                Assert.That(adjustments[statNames[i]], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void ApplyMetaraceAdjustmentsToStatAdjustments()
        {
            var adjustment = 1;
            foreach (var stat in statNames)
            {
                allAdjustments[stat][race.Metarace] = adjustment++;
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(statNames));

            for (var i = 0; i < statNames.Count; i++)
            {
                Assert.That(adjustments[statNames[i]], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void ApplyAgingEffectsToStatAdjustments()
        {
            var adjustment = 1;
            foreach (var stat in statNames)
            {
                allAdjustments[race.Age.Description][stat] = adjustment++;
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(statNames));

            for (var i = 0; i < statNames.Count; i++)
            {
                Assert.That(adjustments[statNames[i]], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void ApplyAllAdjustmentsToStatAdjustments()
        {
            var adjustment = 1;
            foreach (var stat in statNames)
            {
                allAdjustments[stat][race.BaseRace] = adjustment++;
                allAdjustments[stat][race.Metarace] = adjustment++;
                allAdjustments[race.Age.Description][stat] = adjustment++;
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(statNames));

            for (var i = 0; i < statNames.Count; i++)
            {
                var expected = 9 * i + 6;
                Assert.That(adjustments[statNames[i]], Is.EqualTo(expected));
            }
        }

        [Test]
        public void ApplyPositiveAndNegativeAdjustmentsToStatAdjustments()
        {
            var adjustment = 1;
            foreach (var stat in statNames)
            {
                allAdjustments[stat][race.BaseRace] = adjustment++ * Convert.ToInt32(Math.Pow(-1, adjustment));
                allAdjustments[stat][race.Metarace] = adjustment++ * Convert.ToInt32(Math.Pow(-1, adjustment));
                allAdjustments[race.Age.Description][stat] = adjustment++ * Convert.ToInt32(Math.Pow(-1, adjustment));
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(statNames));

            for (var i = 0; i < statNames.Count; i++)
            {
                var expected = (3 * i + 2) * Math.Pow(-1, i);
                Assert.That(adjustments[statNames[i]], Is.EqualTo(expected));
            }
        }
    }
}