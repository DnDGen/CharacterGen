using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected IMetaraceRandomizer randomizer;

        [SetUp]
        public void MetaraceRandomizerTestsSetup()
        {
            var metaraces = RaceConstants.Metaraces.GetMetaraces().Union(new[] { RaceConstants.Metaraces.None });
            mockPercentileResultSelector.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(metaraces);

            foreach (var metarace in metaraces)
                adjustments.Add(metarace, 0);
        }

        protected override IEnumerable<String> GetResults()
        {
            return randomizer.GetAllPossibleResults(String.Empty, characterClass);
        }
    }
}