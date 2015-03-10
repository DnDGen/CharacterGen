using System;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class ConstitutionStatAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, StatConstants.Constitution); }
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 0)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 2)]
        [TestCase(RaceConstants.BaseRaces.DerroId, 2)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 2)]
        [TestCase(RaceConstants.BaseRaces.DrowId, -2)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, 2)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 2)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 2)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 2)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, -2)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, -2)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, -2)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 2)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 2)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 2)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 0)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestialId, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragonId, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiendId, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 2)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 0)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, -2)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 2)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 6)]
        [TestCase(RaceConstants.BaseRaces.OrcId, 0)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 4)]
        [TestCase(RaceConstants.Metaraces.WerebearId, 2)]
        [TestCase(RaceConstants.Metaraces.WereboarId, 2)]
        [TestCase(RaceConstants.Metaraces.WereratId, 2)]
        [TestCase(RaceConstants.Metaraces.WeretigerId, 2)]
        [TestCase(RaceConstants.Metaraces.WerewolfId, 0)]
        [TestCase(RaceConstants.Metaraces.NoneId, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}