using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NonNeutralMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        [Inject, Named(MetaraceRandomizerTypeConstants.NonNeutral)]
        public override IForcableMetaraceRandomizer ForcableMetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.HalfDragonId,
                    RaceConstants.Metaraces.HalfFiendId,
                    RaceConstants.Metaraces.HalfCelestialId,
                    RaceConstants.Metaraces.WerebearId,
                    RaceConstants.Metaraces.WereratId,
                    RaceConstants.Metaraces.WerewolfId,
                    RaceConstants.Metaraces.NoneId
                };
            }
        }

        [TestCase("NonNeutralMetaraceRandomizer")]
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