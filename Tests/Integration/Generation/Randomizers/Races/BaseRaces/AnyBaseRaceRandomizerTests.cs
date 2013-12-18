using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests : IntegrationTest
    {
        [Inject]
        public AnyBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }
        [Inject]
        public CharacterClassPrototype CharacterClassPrototype { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void AnyBaseRaceSingleRandomization()
        {
            BaseRaceRandomizer.Randomize(Alignment.Goodness, CharacterClassPrototype);
        }

        [Test]
        public void AnyBaseRaceRandomizationReturnsBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var baseRace = BaseRaceRandomizer.Randomize(Alignment.Goodness, CharacterClassPrototype);
                Assert.That(baseRace, Is.Not.Null);
                Assert.That(baseRace, Is.Not.Empty);
            }
        }
    }
}