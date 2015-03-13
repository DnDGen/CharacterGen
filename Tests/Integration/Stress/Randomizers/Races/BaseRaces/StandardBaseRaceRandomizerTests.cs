using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class StandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(BaseRaceRandomizerTypeConstants.Standard)]
        public override IBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    RaceConstants.BaseRaces.RockGnomeId,
                    RaceConstants.BaseRaces.HalfElfId,
                    RaceConstants.BaseRaces.HalfOrcId,
                    RaceConstants.BaseRaces.HighElfId,
                    RaceConstants.BaseRaces.HillDwarfId,
                    RaceConstants.BaseRaces.HumanId,
                    RaceConstants.BaseRaces.LightfootHalflingId
                };
            }
        }

        [TestCase("StandardBaseRaceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }
    }
}