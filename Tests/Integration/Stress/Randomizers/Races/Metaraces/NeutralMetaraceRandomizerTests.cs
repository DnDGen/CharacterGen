using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NeutralMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        [Inject, Named(MetaraceRandomizerTypeConstants.Neutral)]
        public override IForcableMetaraceRandomizer ForcableMetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.WereboarId,
                    RaceConstants.Metaraces.WeretigerId,
                    RaceConstants.Metaraces.NoneId
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