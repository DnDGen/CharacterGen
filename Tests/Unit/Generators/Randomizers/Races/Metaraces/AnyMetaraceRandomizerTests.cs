using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AnyMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<String> metaraces
        {
            get { return RaceConstants.Metaraces.GetMetaraces(); }
        }

        [[SetUp]
        public void Setup()
        {
            randomizer = new AnyMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object);
        }

        [Test]
        public void AllMetaracesAllowed()
        {
            var allMetaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            foreach (var metarace in metaraces)
                Assert.That(allMetaraces, Contains.Item(metarace));
        }
    }
}