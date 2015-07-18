using System;
using System.Collections.Generic;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AnyMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<String> metaraceIds
        {
            get
            {
                return new[] {
                    RaceConstants.Metaraces.HalfCelestial,
                    RaceConstants.Metaraces.HalfDragon,
                    RaceConstants.Metaraces.HalfFiend,
                    RaceConstants.Metaraces.Werebear,
                    RaceConstants.Metaraces.Wereboar,
                    RaceConstants.Metaraces.Wererat,
                    RaceConstants.Metaraces.Weretiger,
                    RaceConstants.Metaraces.Werewolf,
                    RaceConstants.Metaraces.None
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new AnyMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object);
        }

        [Test]
        public void AllMetaracesAllowed()
        {
            var allMetaraces = randomizer.GetAllPossible(String.Empty, characterClass);
            foreach (var metarace in metaraceIds)
                Assert.That(allMetaraces, Contains.Item(metarace));
        }
    }
}