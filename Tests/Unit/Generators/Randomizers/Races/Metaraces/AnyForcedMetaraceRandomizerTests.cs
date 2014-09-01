using System;
using System.Collections.Generic;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AnyForcedMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<String> metaraces
        {
            get { return RaceConstants.Metaraces.GetMetaraces(); }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new AnyForcedMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object);
        }

        [Test]
        public void AllMetaracesAllowed()
        {
            var allMetaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            foreach (var metarace in metaraces)
                Assert.That(allMetaraces, Contains.Item(metarace));
        }

        [Test]
        public void NoMetaraceNotAllowed()
        {
            var allMetaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(allMetaraces, Is.Not.Contains(RaceConstants.Metaraces.None));
        }
    }
}