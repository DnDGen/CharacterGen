using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class GoodBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(BaseRaceRandomizerTypeConstants.Good)]
        public override IBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    RaceConstants.BaseRaces.AasimarId,
                    RaceConstants.BaseRaces.SvirfneblinId,
                    RaceConstants.BaseRaces.DeepDwarfId,
                    RaceConstants.BaseRaces.DeepHalflingId,
                    RaceConstants.BaseRaces.ForestGnomeId,
                    RaceConstants.BaseRaces.GrayElfId,
                    RaceConstants.BaseRaces.HalfElfId,
                    RaceConstants.BaseRaces.HalfOrcId,
                    RaceConstants.BaseRaces.HighElfId,
                    RaceConstants.BaseRaces.HillDwarfId,
                    RaceConstants.BaseRaces.HumanId,
                    RaceConstants.BaseRaces.LightfootHalflingId,
                    RaceConstants.BaseRaces.MountainDwarfId,
                    RaceConstants.BaseRaces.RockGnomeId,
                    RaceConstants.BaseRaces.TallfellowHalflingId,
                    RaceConstants.BaseRaces.WildElfId,
                    RaceConstants.BaseRaces.WoodElfId
                };
            }
        }

        [TestCase("GoodBaseRaceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }
    }
}