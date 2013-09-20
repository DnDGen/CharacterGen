using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests
    {
        [Test]
        public void AllBaseRacesAlwaysAllowed()
        {
            var mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("base race");
            var randomizer = new AnyBaseRaceRandomizer(mockPercentileResultProvider.Object);

            var baseRace = randomizer.Randomize(new Alignment(), String.Empty);
            Assert.That(baseRace, Is.EqualTo("base race"));
        }
    }
}