using System;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests : StressTests
    {
        [Inject]
        public ISetMetaraceRandomizer SetMetaraceRandomizer { get; set; }
        [Inject]
        public ICollectionsSelector CollectionsSelector { get; set; }

        [TestCase("SetMetaraceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var metaraces = BaseRaceRandomizer.GetAllPossibles(alignment.Goodness, characterClass);
            SetMetaraceRandomizer.SetMetarace = CollectionsSelector.SelectRandomFrom(metaraces);

            var metarace = SetMetaraceRandomizer.Randomize(alignment.Goodness, characterClass);
            Assert.That(metarace, Is.EqualTo(SetMetaraceRandomizer.SetMetarace));
        }
    }
}