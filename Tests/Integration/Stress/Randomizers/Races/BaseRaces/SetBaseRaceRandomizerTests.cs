using Ninject;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests : StressTests
    {
        [Inject]
        public SetBaseRaceRandomizer SetBaseRaceRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var data = GetNewDependentData();
            SetBaseRaceRandomizer.BaseRace = data.Race.BaseRace;

            var baseRace = SetBaseRaceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
            Assert.That(baseRace, Is.EqualTo(data.Race.BaseRace));
        }
    }
}