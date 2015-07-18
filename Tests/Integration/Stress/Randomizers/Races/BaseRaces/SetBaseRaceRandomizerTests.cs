using System;
using System.Linq;
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

        [TestCase("SetBaseRaceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var baseRaces = BaseRaceRandomizer.GetAllPossibles(alignment.Goodness, characterClass);
            var baseRaceCount = baseRaces.Count();
            var randomIndex = Random.Next(baseRaceCount);
            SetBaseRaceRandomizer.SetBaseRace = baseRaces.ElementAt(randomIndex);

            var baseRace = SetBaseRaceRandomizer.Randomize(alignment.Goodness, characterClass);
            Assert.That(baseRace, Is.EqualTo(SetBaseRaceRandomizer.SetBaseRace));
        }
    }
}