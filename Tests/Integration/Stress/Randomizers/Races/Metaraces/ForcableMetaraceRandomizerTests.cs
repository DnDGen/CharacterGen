using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;
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

        protected abstract IEnumerable<String> allowedMetaraces { get; }

        protected override void MakeAssertions()
        {
            var metarace = GenerateMetarace();
            Assert.That(allowedMetaraces, Contains.Item(metarace), testType);
        }

        private String GenerateMetarace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            return MetaraceRandomizer.Randomize(alignment, characterClass);
        }

        public abstract void MetaraceForced();

        public abstract void MetaraceNotForced();

        protected void AssertForcedMetarace()
        {
            ForcableMetaraceRandomizer.ForceMetarace = true;
            var metarace = GenerateMetarace();
            Assert.That(metarace, Is.Not.EqualTo(RaceConstants.Metaraces.None));
        }

        protected void AssertUnforcedMetarace()
        {
            ForcableMetaraceRandomizer.ForceMetarace = false;

            var metarace = Generate(GenerateMetarace,
                m => m == RaceConstants.Metaraces.None);

            Assert.That(metarace, Is.EqualTo(RaceConstants.Metaraces.None));
        }
    }
}