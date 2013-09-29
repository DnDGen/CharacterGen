using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : RaceRandomizerTests
    {
        protected IBaseRaceRandomizer randomizer;

        protected override String GetResult(String baseRace, String controlCase)
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(baseRace)
                .Returns(controlCase);

            return randomizer.Randomize(new Alignment(), String.Empty);
        }
    }
}