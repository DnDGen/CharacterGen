using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class ForcableMetaraceRandomizerTests : StressTests
    {
        public virtual IForcableMetaraceRandomizer ForcableMetaraceRandomizer { get; set; }

        public override RaceRandomizer MetaraceRandomizer
        {
            get { return ForcableMetaraceRandomizer; }
            set { base.MetaraceRandomizer = value; }
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
            ForcableMetaraceRandomizer.ForceMetarace = true;

            var metarace = GenerateMetarace();
            Assert.That(allowedMetaraces, Contains.Item(metarace));
            Assert.That(metarace, Is.Not.EqualTo(RaceConstants.Metaraces.None));
        }
    }
}