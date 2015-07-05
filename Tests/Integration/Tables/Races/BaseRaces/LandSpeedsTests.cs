using System;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class LandSpeedsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.LandSpeeds; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                RaceConstants.BaseRaces.AasimarId, 
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
                RaceConstants.BaseRaces.SvirfneblinId, 
                RaceConstants.BaseRaces.TallfellowHalflingId, 
                RaceConstants.BaseRaces.TieflingId, 
                RaceConstants.BaseRaces.TroglodyteId, 
                RaceConstants.BaseRaces.WildElfId, 
                RaceConstants.BaseRaces.WoodElfId
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 30)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 30)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 20)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 20)]
        [TestCase(RaceConstants.BaseRaces.DerroId, 20)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 30)]
        [TestCase(RaceConstants.BaseRaces.DrowId, 30)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, 20)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 20)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 30)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 30)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, 30)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 30)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 30)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 30)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 20)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 30)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 30)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 30)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 20)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 30)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 30)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 30)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 20)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 40)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 50)]
        [TestCase(RaceConstants.BaseRaces.OrcId, 30)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 20)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 20)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 20)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 30)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 30)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 30)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 30)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}