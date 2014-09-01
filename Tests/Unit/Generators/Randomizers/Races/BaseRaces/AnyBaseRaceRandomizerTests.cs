using System;
using System.Collections.Generic;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<String> baseRaces
        {
            get { return RaceConstants.BaseRaces.GetBaseRaces(); }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new AnyBaseRaceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object);
        }

        [Test]
        public void AllBaseRacesAllowed()
        {
            var allBaseRaces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            foreach (var baseRace in baseRaces)
                Assert.That(allBaseRaces, Contains.Item(baseRace));
        }
    }
}