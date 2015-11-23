using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class UndeadMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        [Inject, Named(RaceRandomizerTypeConstants.Metarace.UndeadMeta)]
        public override IForcableMetaraceRandomizer ForcableMetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.Vampire,
                    RaceConstants.Metaraces.Ghost,
                    RaceConstants.Metaraces.Lich,
                    RaceConstants.Metaraces.None
                };
            }
        }

        [TestCase("UndeadMetaraceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        [Test]
        public override void MetaraceForced()
        {
            AssertForcedMetarace();
        }

        [Test]
        public override void MetaraceNotForced()
        {
            AssertUnforcedMetarace();
        }
    }
}
