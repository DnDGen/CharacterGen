using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected IMetaraceRandomizer randomizer;
        protected abstract IEnumerable<String> metaraces { get; }

        [SetUp]
        public void MetaraceRandomizerTestsSetup()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(metaraces);

            foreach (var metarace in metaraces)
                adjustments[metarace] = 0;
        }
    }
}