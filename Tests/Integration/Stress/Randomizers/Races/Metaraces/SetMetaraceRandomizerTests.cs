using System;
using System.Linq;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests : StressTests
    {
        [Inject]
        public ISetMetaraceRandomizer SetMetaraceRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        [TestCase("SetMetaraceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var metaraceIds = BaseRaceRandomizer.GetAllPossibles(alignment.Goodness, characterClass);
            var metaraceCount = metaraceIds.Count();
            var randomIndex = Random.Next(metaraceCount);
            SetMetaraceRandomizer.SetMetarace = metaraceIds.ElementAt(randomIndex);

            var metarace = SetMetaraceRandomizer.Randomize(alignment.Goodness, characterClass);
            Assert.That(metarace, Is.EqualTo(SetMetaraceRandomizer.SetMetarace));
        }
    }
}