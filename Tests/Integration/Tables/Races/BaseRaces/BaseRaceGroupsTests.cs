using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class BaseRaceGroupsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.BaseRaceGroups; }
        }

        [TestCase(TableNameConstants.Set.Collection.Groups.Standard,
            RaceConstants.BaseRaces.HalfElfId,
            RaceConstants.BaseRaces.HalfOrcId,
            RaceConstants.BaseRaces.HighElfId,
            RaceConstants.BaseRaces.HillDwarfId,
            RaceConstants.BaseRaces.HumanId,
            RaceConstants.BaseRaces.LightfootHalflingId,
            RaceConstants.BaseRaces.RockGnomeId)]
        [TestCase(AlignmentConstants.Evil,
            RaceConstants.BaseRaces.DeepDwarfId,
            RaceConstants.BaseRaces.HillDwarfId,
            RaceConstants.BaseRaces.HighElfId,
            RaceConstants.BaseRaces.WildElfId,
            RaceConstants.BaseRaces.WoodElfId,
            RaceConstants.BaseRaces.HalfElfId,
            RaceConstants.BaseRaces.LightfootHalflingId,
            RaceConstants.BaseRaces.DeepHalflingId,
            RaceConstants.BaseRaces.TallfellowHalflingId,
            RaceConstants.BaseRaces.HalfOrcId,
            RaceConstants.BaseRaces.HumanId,
            RaceConstants.BaseRaces.LizardfolkId,
            RaceConstants.BaseRaces.GoblinId,
            RaceConstants.BaseRaces.HobgoblinId,
            RaceConstants.BaseRaces.KoboldId,
            RaceConstants.BaseRaces.OrcId,
            RaceConstants.BaseRaces.TieflingId,
            RaceConstants.BaseRaces.DrowId,
            RaceConstants.BaseRaces.DuergarDwarfId,
            RaceConstants.BaseRaces.DerroId,
            RaceConstants.BaseRaces.GnollId,
            RaceConstants.BaseRaces.TroglodyteId,
            RaceConstants.BaseRaces.BugbearId,
            RaceConstants.BaseRaces.OgreId,
            RaceConstants.BaseRaces.MinotaurId,
            RaceConstants.BaseRaces.MindFlayerId,
            RaceConstants.BaseRaces.OgreMageId)]
        [TestCase(AlignmentConstants.Good,
            RaceConstants.BaseRaces.AasimarId,
            RaceConstants.BaseRaces.DeepDwarfId,
            RaceConstants.BaseRaces.MountainDwarfId,
            RaceConstants.BaseRaces.HillDwarfId,
            RaceConstants.BaseRaces.HighElfId,
            RaceConstants.BaseRaces.GrayElfId,
            RaceConstants.BaseRaces.WildElfId,
            RaceConstants.BaseRaces.WoodElfId,
            RaceConstants.BaseRaces.ForestGnomeId,
            RaceConstants.BaseRaces.RockGnomeId,
            RaceConstants.BaseRaces.HalfElfId,
            RaceConstants.BaseRaces.LightfootHalflingId,
            RaceConstants.BaseRaces.DeepHalflingId,
            RaceConstants.BaseRaces.TallfellowHalflingId,
            RaceConstants.BaseRaces.HalfOrcId,
            RaceConstants.BaseRaces.HumanId,
            RaceConstants.BaseRaces.SvirfneblinId)]
        [TestCase(AlignmentConstants.Neutral,
            RaceConstants.BaseRaces.DeepDwarfId,
            RaceConstants.BaseRaces.MountainDwarfId,
            RaceConstants.BaseRaces.HillDwarfId,
            RaceConstants.BaseRaces.HighElfId,
            RaceConstants.BaseRaces.GrayElfId,
            RaceConstants.BaseRaces.WildElfId,
            RaceConstants.BaseRaces.WoodElfId,
            RaceConstants.BaseRaces.ForestGnomeId,
            RaceConstants.BaseRaces.RockGnomeId,
            RaceConstants.BaseRaces.HalfElfId,
            RaceConstants.BaseRaces.LightfootHalflingId,
            RaceConstants.BaseRaces.DeepHalflingId,
            RaceConstants.BaseRaces.TallfellowHalflingId,
            RaceConstants.BaseRaces.HalfOrcId,
            RaceConstants.BaseRaces.HumanId,
            RaceConstants.BaseRaces.LizardfolkId,
            RaceConstants.BaseRaces.DoppelgangerId)]
        [TestCase(RaceConstants.Sizes.Large,
            RaceConstants.BaseRaces.MinotaurId,
            RaceConstants.BaseRaces.OgreId,
            RaceConstants.BaseRaces.OgreMageId)]
        [TestCase(RaceConstants.Sizes.Small,
            RaceConstants.BaseRaces.DerroId,
            RaceConstants.BaseRaces.ForestGnomeId,
            RaceConstants.BaseRaces.RockGnomeId,
            RaceConstants.BaseRaces.SvirfneblinId,
            RaceConstants.BaseRaces.GoblinId,
            RaceConstants.BaseRaces.DeepHalflingId,
            RaceConstants.BaseRaces.LightfootHalflingId,
            RaceConstants.BaseRaces.TallfellowHalflingId,
            RaceConstants.BaseRaces.KoboldId)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Monsters,
            RaceConstants.BaseRaces.BugbearId,
            RaceConstants.BaseRaces.DerroId,
            RaceConstants.BaseRaces.DoppelgangerId,
            RaceConstants.BaseRaces.GnollId,
            RaceConstants.BaseRaces.LizardfolkId,
            RaceConstants.BaseRaces.MindFlayerId,
            RaceConstants.BaseRaces.MinotaurId,
            RaceConstants.BaseRaces.OgreId,
            RaceConstants.BaseRaces.OgreMageId,
            RaceConstants.BaseRaces.TroglodyteId)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}