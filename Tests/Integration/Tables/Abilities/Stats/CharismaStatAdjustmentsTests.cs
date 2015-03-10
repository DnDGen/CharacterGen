using System;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class CharismaStatAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, StatConstants.Charisma); }
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 2)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, -2)]
        [TestCase(RaceConstants.BaseRaces.DerroId, 6)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 2)]
        [TestCase(RaceConstants.BaseRaces.DrowId, 2)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, -4)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, -2)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, -2)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, -2)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.GnollId, -2)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 2)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 0)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, -4)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, -2)]
        [TestCase(RaceConstants.Metaraces.HalfCelestialId, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragonId, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiendId, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, -2)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 0)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 0)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 0)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 0)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 6)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, -2)]
        [TestCase(RaceConstants.BaseRaces.OgreId, -4)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 6)]
        [TestCase(RaceConstants.BaseRaces.OrcId, -2)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, -2)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 0)]
        [TestCase(RaceConstants.Metaraces.WerebearId, -2)]
        [TestCase(RaceConstants.Metaraces.WereboarId, -2)]
        [TestCase(RaceConstants.Metaraces.WereratId, -2)]
        [TestCase(RaceConstants.Metaraces.WeretigerId, 0)]
        [TestCase(RaceConstants.Metaraces.WerewolfId, -2)]
        [TestCase(RaceConstants.Metaraces.NoneId, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}