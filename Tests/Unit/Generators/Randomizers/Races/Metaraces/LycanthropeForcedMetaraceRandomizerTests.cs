using System;
using Moq;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class LycanthropeForcedMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new LycanthropeForcedMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object,
                mockCollectionsSelector.Object);

            mockCollectionsSelector.Setup(s => s.SelectFrom("MetaraceGroups", "Lycanthrope")).Returns(new[] { "genetic metarace" });
        }

        [TestCase("lycanthrope metarace")]
        public void Allowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(metaraces, Contains.Item(metarace));
        }

        [TestCase("genetic metarace")]
        [TestCase(RaceConstants.Metaraces.None)]
        public void NotAllowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(metaraces, Is.Not.Contains(metarace));
        }
    }
}