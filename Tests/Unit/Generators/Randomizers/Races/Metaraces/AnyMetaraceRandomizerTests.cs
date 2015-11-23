using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AnyMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<String> metaraceNames
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
            randomizer = new AnyMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object, generator);
        }

        [Test]
        public void AllMetaracesAllowed()
        {
            var allMetaraces = randomizer.GetAllPossible(alignment, characterClass);
            foreach (var metarace in metaraceNames)
                Assert.That(allMetaraces, Contains.Item(metarace));
        }
    }
}