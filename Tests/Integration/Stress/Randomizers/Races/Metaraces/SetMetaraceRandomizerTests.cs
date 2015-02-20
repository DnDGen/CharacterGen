using System;
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
            SetMetaraceRandomizer.SetMetarace = Random.Next().ToString();
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var metarace = SetMetaraceRandomizer.Randomize(alignment.Goodness, characterClass);
            Assert.That(metarace, Is.EqualTo(SetMetaraceRandomizer.SetMetarace));
        }
    }
}