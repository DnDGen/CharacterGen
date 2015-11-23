using System;
using System.Collections.Generic;
using Ninject;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class GoodMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        [Inject, Named(RaceRandomizerTypeConstants.Metarace.GoodMeta)]
        public override IForcableMetaraceRandomizer ForcableMetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.HalfDragon,
                    RaceConstants.Metaraces.HalfCelestial,
                    RaceConstants.Metaraces.Werebear,
                    RaceConstants.Metaraces.Ghost,
                    RaceConstants.Metaraces.None
                };
            }
        }

        [TestCase("GoodMetaraceRandomizer")]
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