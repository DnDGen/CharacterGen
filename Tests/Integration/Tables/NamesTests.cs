using System;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public class NamesTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.Names; }
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, RaceConstants.BaseRaces.Aasimar)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(RaceConstants.BaseRaces.DerroId, RaceConstants.BaseRaces.Derro)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, RaceConstants.BaseRaces.Doppelganger)]
        [TestCase(RaceConstants.BaseRaces.DrowId, RaceConstants.BaseRaces.Drow)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, RaceConstants.BaseRaces.DuergarDwarf)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(RaceConstants.BaseRaces.GnollId, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, RaceConstants.BaseRaces.Goblin)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, RaceConstants.BaseRaces.HighElf)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(RaceConstants.BaseRaces.HumanId, RaceConstants.BaseRaces.Human)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, RaceConstants.BaseRaces.Kobold)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, RaceConstants.BaseRaces.Minotaur)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(RaceConstants.BaseRaces.OgreId, RaceConstants.BaseRaces.Ogre)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(RaceConstants.BaseRaces.OrcId, RaceConstants.BaseRaces.Orc)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, RaceConstants.BaseRaces.WildElf)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(RaceConstants.Metaraces.HalfCelestialId, RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(RaceConstants.Metaraces.HalfDragonId, RaceConstants.Metaraces.HalfDragon)]
        [TestCase(RaceConstants.Metaraces.HalfFiendId, RaceConstants.Metaraces.HalfFiend)]
        [TestCase(RaceConstants.Metaraces.NoneId, RaceConstants.Metaraces.None)]
        [TestCase(RaceConstants.Metaraces.WerebearId, RaceConstants.Metaraces.Werebear)]
        [TestCase(RaceConstants.Metaraces.WereboarId, RaceConstants.Metaraces.Wereboar)]
        [TestCase(RaceConstants.Metaraces.WereratId, RaceConstants.Metaraces.Wererat)]
        [TestCase(RaceConstants.Metaraces.WeretigerId, RaceConstants.Metaraces.Weretiger)]
        [TestCase(RaceConstants.Metaraces.WerewolfId, RaceConstants.Metaraces.Werewolf)]
        public override void Collection(String name, params String[] collection)
        {
            base.Collection(name, collection);
        }
    }
}