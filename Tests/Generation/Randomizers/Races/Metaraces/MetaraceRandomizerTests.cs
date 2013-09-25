using System;
using Moq;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected IMetaraceRandomizer randomizer;

        protected override String GetResult(String metarace, String controlCase)
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(metarace)
                .Returns(controlCase);

            return randomizer.Randomize(alignment, String.Empty);
        }
    }
}