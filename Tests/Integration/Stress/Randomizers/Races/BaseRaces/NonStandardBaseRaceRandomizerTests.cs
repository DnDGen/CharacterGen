using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonStandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(BaseRaceRandomizerTypeConstants.NonStandard)]
        public override IBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    RaceConstants.BaseRaces.SvirfneblinId,
                    RaceConstants.BaseRaces.AasimarId,
                    RaceConstants.BaseRaces.DerroId,
                    RaceConstants.BaseRaces.DrowId,
                    RaceConstants.BaseRaces.DuergarDwarfId,
                    RaceConstants.BaseRaces.GoblinId,
                    RaceConstants.BaseRaces.HobgoblinId,
                    RaceConstants.BaseRaces.OgreId,
                    RaceConstants.BaseRaces.OgreMageId,
                    RaceConstants.BaseRaces.OrcId,
                    RaceConstants.BaseRaces.TroglodyteId,
                    RaceConstants.BaseRaces.MindFlayerId,
                    RaceConstants.BaseRaces.MinotaurId,
                    RaceConstants.BaseRaces.GnollId,
                    RaceConstants.BaseRaces.BugbearId,
                    RaceConstants.BaseRaces.TieflingId,
                    RaceConstants.BaseRaces.KoboldId,
                    RaceConstants.BaseRaces.DoppelgangerId,
                    RaceConstants.BaseRaces.LizardfolkId,
                    RaceConstants.BaseRaces.MountainDwarfId,
                    RaceConstants.BaseRaces.ForestGnomeId,
                    RaceConstants.BaseRaces.TallfellowHalflingId,
                    RaceConstants.BaseRaces.WildElfId,
                    RaceConstants.BaseRaces.DeepDwarfId,
                    RaceConstants.BaseRaces.DeepHalflingId,
                    RaceConstants.BaseRaces.GrayElfId,
                    RaceConstants.BaseRaces.WoodElfId
                };
            }
        }

        [TestCase("NonStandardBaseRaceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }
    }
}