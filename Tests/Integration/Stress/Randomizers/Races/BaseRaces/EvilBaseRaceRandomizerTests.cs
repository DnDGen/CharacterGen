using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class EvilBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(BaseRaceRandomizerTypeConstants.Evil)]
        public override IBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    RaceConstants.BaseRaces.BugbearId,
                    RaceConstants.BaseRaces.DerroId,
                    RaceConstants.BaseRaces.DrowId,
                    RaceConstants.BaseRaces.DuergarDwarfId,
                    RaceConstants.BaseRaces.GnollId,
                    RaceConstants.BaseRaces.GoblinId,
                    RaceConstants.BaseRaces.HobgoblinId,
                    RaceConstants.BaseRaces.KoboldId,
                    RaceConstants.BaseRaces.OgreId,
                    RaceConstants.BaseRaces.OgreMageId,
                    RaceConstants.BaseRaces.OrcId,
                    RaceConstants.BaseRaces.TroglodyteId,
                    RaceConstants.BaseRaces.MindFlayerId,
                    RaceConstants.BaseRaces.MinotaurId,
                    RaceConstants.BaseRaces.TieflingId,
                    RaceConstants.BaseRaces.LizardfolkId,
                    RaceConstants.BaseRaces.DeepDwarfId,
                    RaceConstants.BaseRaces.DeepHalflingId,
                    RaceConstants.BaseRaces.HalfElfId,
                    RaceConstants.BaseRaces.HalfOrcId,
                    RaceConstants.BaseRaces.HighElfId,
                    RaceConstants.BaseRaces.HillDwarfId,
                    RaceConstants.BaseRaces.HumanId,
                    RaceConstants.BaseRaces.LightfootHalflingId,
                    RaceConstants.BaseRaces.TallfellowHalflingId,
                    RaceConstants.BaseRaces.WildElfId,
                    RaceConstants.BaseRaces.WoodElfId
                };
            }
        }

        [TestCase("EvilBaseRaceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }
    }
}