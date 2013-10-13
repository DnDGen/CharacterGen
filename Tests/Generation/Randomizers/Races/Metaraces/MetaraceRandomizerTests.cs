using Moq;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected IMetaraceRandomizer randomizer;

        protected override String GetResult(String metarace, String controlCase)
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(metarace)
                .Returns(controlCase);

            return randomizer.Randomize(String.Empty, String.Empty);
        }
    }
}