using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AnyMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [Inject, Named(MetaraceRandomizerTypeConstants.Any)]
        public override IMetaraceRandomizer MetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[] {
                    RaceConstants.Metaraces.HalfCelestialId,
                    RaceConstants.Metaraces.HalfDragonId,
                    RaceConstants.Metaraces.HalfFiendId,
                    RaceConstants.Metaraces.WerebearId,
                    RaceConstants.Metaraces.WereboarId,
                    RaceConstants.Metaraces.WereratId,
                    RaceConstants.Metaraces.WeretigerId,
                    RaceConstants.Metaraces.WerewolfId,
                    RaceConstants.Metaraces.NoneId
                };
            }
        }

        [TestCase("AnyMetaraceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }
    }
}