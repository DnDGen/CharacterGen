using System;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class RacialBaseAttackAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments; }
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 0)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 2)]
        [TestCase(RaceConstants.BaseRaces.DerroId, 3)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 4)]
        [TestCase(RaceConstants.BaseRaces.DrowId, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 0)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 1)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 0)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 0)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestialId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfDragonId, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiendId, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 0)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 0)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 0)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 1)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 6)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 6)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 3)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 3)]
        [TestCase(RaceConstants.BaseRaces.OrcId, 0)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 1)]
        [TestCase(RaceConstants.Metaraces.WerebearId, 4)]
        [TestCase(RaceConstants.Metaraces.WereboarId, 2)]
        [TestCase(RaceConstants.Metaraces.WereratId, 0)]
        [TestCase(RaceConstants.Metaraces.WeretigerId, 4)]
        [TestCase(RaceConstants.Metaraces.WerewolfId, 1)]
        [TestCase(RaceConstants.Metaraces.NoneId, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}