using System;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests : StressTests
    {
        [Inject]
        public ISetBaseRaceRandomizer SetBaseRaceRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            SetBaseRaceRandomizer.SetBaseRace = Random.Next().ToString();
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var baseRace = SetBaseRaceRandomizer.Randomize(alignment.Goodness, characterClass);
            Assert.That(baseRace, Is.EqualTo(SetBaseRaceRandomizer.SetBaseRace));
        }
    }
}