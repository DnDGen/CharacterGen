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
                    RaceConstants.Metaraces.HalfCelestialId,
                    RaceConstants.Metaraces.HalfDragonId,
                    RaceConstants.Metaraces.HalfFiendId,
                    RaceConstants.Metaraces.WerebearId,
                    RaceConstants.Metaraces.WereboarId,
                    RaceConstants.Metaraces.WereratId,
                    RaceConstants.Metaraces.WeretigerId,
                    RaceConstants.Metaraces.WerewolfId,
                    RaceConstants.Metaraces.NoneId
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new AnyMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object, mockNameSelector.Object);
        }

        [Test]
        public void AllMetaracesAllowed()
        {
            var allMetaraces = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            foreach (var metarace in metaraceIds)
                Assert.That(allMetaraces, Contains.Item(metarace));
        }
    }
}