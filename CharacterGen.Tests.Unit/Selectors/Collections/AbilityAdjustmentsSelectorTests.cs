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
    public class AbilityAdjustmentsSelectorTests
    {
        private IAbilityAdjustmentsSelector selector;
        private Race race;
        private Mock<IAdjustmentsSelector> mockInnerSelector;
        private Dictionary<string, Dictionary<string, int>> allAdjustments;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private List<string> abilityNames;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            selector = new AbilityAdjustmentsSelector(mockInnerSelector.Object, mockCollectionsSelector.Object);
            race = new Race();
            allAdjustments = new Dictionary<string, Dictionary<string, int>>();
            abilityNames = new List<string>();

            race.BaseRace = "base race";
            race.Metarace = "metarace";
            race.Age.Description = "super old";

            allAdjustments[race.Age.Description] = new Dictionary<string, int>();

            abilityNames.Add("first ability");
            abilityNames.Add("second ability");

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.AGEAbilityAdjustments, race.Age.Description);
            mockInnerSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(allAdjustments[race.Age.Description]);

            foreach (var ability in abilityNames)
            {
                allAdjustments[ability] = new Dictionary<string, int>();
                allAdjustments[ability][race.BaseRace] = 0;
                allAdjustments[ability][race.Metarace] = 0;
                allAdjustments[race.Age.Description][ability] = 0;

                tableName = string.Format(TableNameConstants.Formattable.Adjustments.ABILITYAbilityAdjustments, ability);
                mockInnerSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(allAdjustments[ability]);
            }

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AbilityGroups, GroupConstants.All)).Returns(abilityNames);
        }

        [Test]
        public void AdjustmentsContainAllAbilities()
        {
            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(abilityNames));
        }

        [Test]
        public void ApplyBaseRaceAdjustmentsToAbilityAdjustments()
        {
            var adjustment = 1;
            foreach (var ability in abilityNames)
            {
                allAdjustments[ability][race.BaseRace] = adjustment++;
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(abilityNames));

            for (var i = 0; i < abilityNames.Count; i++)
            {
                Assert.That(adjustments[abilityNames[i]], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void ApplyMetaraceAdjustmentsToAbilityAdjustments()
        {
            var adjustment = 1;
            foreach (var ability in abilityNames)
            {
                allAdjustments[ability][race.Metarace] = adjustment++;
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(abilityNames));

            for (var i = 0; i < abilityNames.Count; i++)
            {
                Assert.That(adjustments[abilityNames[i]], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void ApplyAgingEffectsToAbilityAdjustments()
        {
            var adjustment = 1;
            foreach (var ability in abilityNames)
            {
                allAdjustments[race.Age.Description][ability] = adjustment++;
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(abilityNames));

            for (var i = 0; i < abilityNames.Count; i++)
            {
                Assert.That(adjustments[abilityNames[i]], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void ApplyAllAdjustmentsToAbilityAdjustments()
        {
            var adjustment = 1;
            foreach (var ability in abilityNames)
            {
                allAdjustments[ability][race.BaseRace] = adjustment++;
                allAdjustments[ability][race.Metarace] = adjustment++;
                allAdjustments[race.Age.Description][ability] = adjustment++;
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(abilityNames));

            for (var i = 0; i < abilityNames.Count; i++)
            {
                var expected = 9 * i + 6;
                Assert.That(adjustments[abilityNames[i]], Is.EqualTo(expected));
            }
        }

        [Test]
        public void ApplyPositiveAndNegativeAdjustmentsToAbilityAdjustments()
        {
            var adjustment = 1;
            foreach (var ability in abilityNames)
            {
                allAdjustments[ability][race.BaseRace] = adjustment++ * Convert.ToInt32(Math.Pow(-1, adjustment));
                allAdjustments[ability][race.Metarace] = adjustment++ * Convert.ToInt32(Math.Pow(-1, adjustment));
                allAdjustments[race.Age.Description][ability] = adjustment++ * Convert.ToInt32(Math.Pow(-1, adjustment));
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments.Keys, Is.EquivalentTo(abilityNames));

            for (var i = 0; i < abilityNames.Count; i++)
            {
                var expected = (3 * i + 2) * Math.Pow(-1, i);
                Assert.That(adjustments[abilityNames[i]], Is.EqualTo(expected));
            }
        }
    }
}