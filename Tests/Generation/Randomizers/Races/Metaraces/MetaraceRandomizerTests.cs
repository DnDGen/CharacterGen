using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;
using System;

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

            return randomizer.Randomize(new Alignment(), String.Empty);
        }
    }
}