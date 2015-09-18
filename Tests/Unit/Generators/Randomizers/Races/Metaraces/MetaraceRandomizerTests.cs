using System;
using System.Collections.Generic;
using Moq;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected RaceRandomizer randomizer;
        protected abstract IEnumerable<String> metaraceIds { get; }

        [SetUp]
        public void MetaraceRandomizerTestsSetup()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(metaraceIds);

            foreach (var metarace in metaraceIds)
                adjustments[metarace] = 0;
        }
    }
}