using Ninject;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests : StressTest
    {
        [Inject]
        public SetMetaraceRandomizer MetaraceRandomizer { get; set; }

        [Test]
        public void SetBaseRaceRandomizerAlwaysReturnsSetBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                MetaraceRandomizer.Metarace = data.Race.Metarace;

                var metarace = MetaraceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(metarace, Is.EqualTo(MetaraceRandomizer.Metarace));
            }

            AssertIterations();
        }
    }
}