using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Selectors;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
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

            var metaraces = MetaraceRandomizer.GetAllPossible(alignment, characterClass);
            SetMetaraceRandomizer.SetMetarace = CollectionsSelector.SelectRandomFrom(metaraces);

            var metarace = SetMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(SetMetaraceRandomizer.SetMetarace));
        }
    }
}