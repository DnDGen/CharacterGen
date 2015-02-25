using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonGoodBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(BaseRaceRandomizerTypeConstants.NonGood)]
        public override IBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> allowedBaseRaces
        {
            get
            {
                return new[] {
                    RaceConstants.BaseRaces.BugbearId,
                    RaceConstants.BaseRaces.DeepDwarfId,
                    RaceConstants.BaseRaces.DeepHalflingId,
                    RaceConstants.BaseRaces.DerroId,
                    RaceConstants.BaseRaces.DoppelgangerId,
                    RaceConstants.BaseRaces.DrowId,
                    RaceConstants.BaseRaces.DuergarDwarfId,
                    RaceConstants.BaseRaces.ForestGnomeId,
                    RaceConstants.BaseRaces.GnollId,
                    RaceConstants.BaseRaces.GoblinId,
                    RaceConstants.BaseRaces.GrayElfId,
                    RaceConstants.BaseRaces.HalfElfId,
                    RaceConstants.BaseRaces.HalfOrcId,
                    RaceConstants.BaseRaces.HighElfId,
                    RaceConstants.BaseRaces.HillDwarfId,
                    RaceConstants.BaseRaces.HobgoblinId,
                    RaceConstants.BaseRaces.HumanId,
                    RaceConstants.BaseRaces.KoboldId,
                    RaceConstants.BaseRaces.LightfootHalflingId,
                    RaceConstants.BaseRaces.LizardfolkId,
                    RaceConstants.BaseRaces.MindFlayerId,
                    RaceConstants.BaseRaces.MinotaurId,
                    RaceConstants.BaseRaces.MountainDwarfId,
                    RaceConstants.BaseRaces.OgreId,
                    RaceConstants.BaseRaces.OgreMageId,
                    RaceConstants.BaseRaces.OrcId,
                    RaceConstants.BaseRaces.RockGnomeId,
                    RaceConstants.BaseRaces.TallfellowHalflingId,
                    RaceConstants.BaseRaces.TieflingId,
                    RaceConstants.BaseRaces.TroglodyteId,
                    RaceConstants.BaseRaces.WildElfId,
                    RaceConstants.BaseRaces.WoodElfId
                };
            }
        }

        [TestCase("NonGoodBaseRaceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }
    }
}