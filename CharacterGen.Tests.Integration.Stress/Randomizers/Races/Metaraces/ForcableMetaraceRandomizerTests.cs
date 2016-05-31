using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class ForcableMetaraceRandomizerTests : StressTests
    {
        protected IForcableMetaraceRandomizer forcableMetaraceRandomizer
        {
            get { return MetaraceRandomizer as IForcableMetaraceRandomizer; }
            set { MetaraceRandomizer = value; }
        }

        protected abstract IEnumerable<string> allowedMetaraces { get; }

        protected override void MakeAssertions()
        {
            var metarace = GenerateMetarace();
            Assert.That(allowedMetaraces, Contains.Item(metarace));
        }

        private string GenerateMetarace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            return MetaraceRandomizer.Randomize(alignment, characterClass);
        }

        public abstract void StressForcedMetarace();

        protected void AssertForcedMetarace()
        {
            forcableMetaraceRandomizer.ForceMetarace = true;

            var metarace = GenerateMetarace();
            Assert.That(allowedMetaraces, Contains.Item(metarace));
            Assert.That(metarace, Is.Not.EqualTo(RaceConstants.Metaraces.None));
        }
    }
}