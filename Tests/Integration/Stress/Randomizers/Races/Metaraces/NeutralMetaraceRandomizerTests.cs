using System;
using System.Collections.Generic;
using Ninject;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NeutralMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        [Inject, Named(RaceRandomizerTypeConstants.Metarace.NeutralMeta)]
        public override IForcableMetaraceRandomizer ForcableMetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.Wereboar,
                    RaceConstants.Metaraces.Weretiger,
                    RaceConstants.Metaraces.Ghost,
                    RaceConstants.Metaraces.None
                };
            }
        }

        [TestCase("NeutralMetaraceRandomizerTests")]
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