using System;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class StrengthStatAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, StatConstants.Strength); }
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 0)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 4)]
        [TestCase(RaceConstants.BaseRaces.DerroId, 0)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 2)]
        [TestCase(RaceConstants.BaseRaces.DrowId, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, -2)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 2)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 4)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, -2)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, -2)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, -2)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, -2)]
        [TestCase(RaceConstants.Metaraces.HalfCelestialId, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragonId, 8)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiendId, 4)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 2)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, -2)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, -2)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, -2)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 0)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 0)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, -4)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 2)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 8)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 10)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 10)]
        [TestCase(RaceConstants.BaseRaces.OrcId, 4)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 0)]
        [TestCase(RaceConstants.Metaraces.WerebearId, 2)]
        [TestCase(RaceConstants.Metaraces.WereboarId, 2)]
        [TestCase(RaceConstants.Metaraces.WereratId, 0)]
        [TestCase(RaceConstants.Metaraces.WeretigerId, 2)]
        [TestCase(RaceConstants.Metaraces.WerewolfId, 2)]
        [TestCase(RaceConstants.Metaraces.NoneId, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}