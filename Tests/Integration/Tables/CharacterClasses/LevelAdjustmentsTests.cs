﻿using System;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class LevelAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.LevelAdjustments; }
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
                RaceConstants.BaseRaces.WoodElfId,
                RaceConstants.Metaraces.HalfCelestialId, 
                RaceConstants.Metaraces.HalfDragonId, 
                RaceConstants.Metaraces.HalfFiendId, 
                RaceConstants.Metaraces.NoneId, 
                RaceConstants.Metaraces.WerebearId, 
                RaceConstants.Metaraces.WereboarId,
                RaceConstants.Metaraces.WereratId, 
                RaceConstants.Metaraces.WeretigerId, 
                RaceConstants.Metaraces.WerewolfId
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 0)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 2)]
        [TestCase(RaceConstants.BaseRaces.DerroId, 1)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 3)]
        [TestCase(RaceConstants.BaseRaces.DrowId, 1)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, 1)]
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
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 1)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestialId, 1)]
        [TestCase(RaceConstants.Metaraces.HalfDragonId, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiendId, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 0)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 0)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 0)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 0)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 8)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 2)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 8)]
        [TestCase(RaceConstants.BaseRaces.OrcId, 0)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 0)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 1)]
        [TestCase(RaceConstants.Metaraces.WerebearId, 2)]
        [TestCase(RaceConstants.Metaraces.WereboarId, 1)]
        [TestCase(RaceConstants.Metaraces.WereratId, 1)]
        [TestCase(RaceConstants.Metaraces.WeretigerId, 1)]
        [TestCase(RaceConstants.Metaraces.WerewolfId, 1)]
        [TestCase(RaceConstants.Metaraces.NoneId, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}