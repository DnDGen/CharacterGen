using CharacterGen.Randomizers.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected RaceRandomizer randomizer;
        protected abstract IEnumerable<string> metaraceNames { get; }

        [SetUp]
        public void MetaraceRandomizerTestsSetup()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(metaraceNames);

            foreach (var metarace in metaraceNames)
                adjustments[metarace] = 0;
        }
    }
}