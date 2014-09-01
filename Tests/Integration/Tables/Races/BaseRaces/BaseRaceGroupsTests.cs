using System;
using System.Collections.Generic;
using NPCGen.Common.Alignments;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class BaseRaceGroupsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "BaseRaceGroups"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get
            {
                return new[]
                {
                    AlignmentConstants.Evil,
                    AlignmentConstants.Good,
                    AlignmentConstants.Neutral,
                    "Standard"
                };
            }
        }

        [TestCase("Standard",
            RaceConstants.BaseRaces.HalfElf,
            RaceConstants.BaseRaces.HalfOrc,
            RaceConstants.BaseRaces.HighElf,
            RaceConstants.BaseRaces.HillDwarf,
            RaceConstants.BaseRaces.Human,
            RaceConstants.BaseRaces.LightfootHalfling,
            RaceConstants.BaseRaces.RockGnome)]
        [TestCase(AlignmentConstants.Evil,
            RaceConstants.BaseRaces.DeepDwarf,
            RaceConstants.BaseRaces.HillDwarf,
            RaceConstants.BaseRaces.HighElf,
            RaceConstants.BaseRaces.WildElf,
            RaceConstants.BaseRaces.WoodElf,
            RaceConstants.BaseRaces.HalfElf,
            RaceConstants.BaseRaces.LightfootHalfling,
            RaceConstants.BaseRaces.DeepHalfling,
            RaceConstants.BaseRaces.TallfellowHalfling,
            RaceConstants.BaseRaces.HalfOrc,
            RaceConstants.BaseRaces.Human,
            RaceConstants.BaseRaces.Lizardfolk,
            RaceConstants.BaseRaces.Goblin,
            RaceConstants.BaseRaces.Hobgoblin,
            RaceConstants.BaseRaces.Kobold,
            RaceConstants.BaseRaces.Orc,
            RaceConstants.BaseRaces.Tiefling,
            RaceConstants.BaseRaces.Drow,
            RaceConstants.BaseRaces.DuergarDwarf,
            RaceConstants.BaseRaces.Derro,
            RaceConstants.BaseRaces.Gnoll,
            RaceConstants.BaseRaces.Troglodyte,
            RaceConstants.BaseRaces.Bugbear,
            RaceConstants.BaseRaces.Ogre,
            RaceConstants.BaseRaces.Minotaur,
            RaceConstants.BaseRaces.MindFlayer,
            RaceConstants.BaseRaces.OgreMage)]
        [TestCase(AlignmentConstants.Good,
            RaceConstants.BaseRaces.Aasimar,
            RaceConstants.BaseRaces.DeepDwarf,
            RaceConstants.BaseRaces.MountainDwarf,
            RaceConstants.BaseRaces.HillDwarf,
            RaceConstants.BaseRaces.HighElf,
            RaceConstants.BaseRaces.GrayElf,
            RaceConstants.BaseRaces.WildElf,
            RaceConstants.BaseRaces.WoodElf,
            RaceConstants.BaseRaces.ForestGnome,
            RaceConstants.BaseRaces.RockGnome,
            RaceConstants.BaseRaces.HalfElf,
            RaceConstants.BaseRaces.LightfootHalfling,
            RaceConstants.BaseRaces.DeepHalfling,
            RaceConstants.BaseRaces.TallfellowHalfling,
            RaceConstants.BaseRaces.HalfOrc,
            RaceConstants.BaseRaces.Human,
            RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(AlignmentConstants.Neutral,
            RaceConstants.BaseRaces.DeepDwarf,
            RaceConstants.BaseRaces.MountainDwarf,
            RaceConstants.BaseRaces.HillDwarf,
            RaceConstants.BaseRaces.HighElf,
            RaceConstants.BaseRaces.GrayElf,
            RaceConstants.BaseRaces.WildElf,
            RaceConstants.BaseRaces.WoodElf,
            RaceConstants.BaseRaces.ForestGnome,
            RaceConstants.BaseRaces.RockGnome,
            RaceConstants.BaseRaces.HalfElf,
            RaceConstants.BaseRaces.LightfootHalfling,
            RaceConstants.BaseRaces.DeepHalfling,
            RaceConstants.BaseRaces.TallfellowHalfling,
            RaceConstants.BaseRaces.HalfOrc,
            RaceConstants.BaseRaces.Human,
            RaceConstants.BaseRaces.Lizardfolk,
            RaceConstants.BaseRaces.Doppelganger)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}