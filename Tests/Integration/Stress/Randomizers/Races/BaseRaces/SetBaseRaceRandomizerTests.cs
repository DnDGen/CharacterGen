using Ninject;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests : StressTests
    {
        [Inject]
        public SetBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        [Test]
        public void SetBaseRaceRandomizerAlwaysReturnsSetBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                BaseRaceRandomizer.BaseRace = data.Race.BaseRace;

                var baseRace = BaseRaceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(baseRace, Is.EqualTo(BaseRaceRandomizer.BaseRace));
            }

            AssertIterations();
        }
    }
}