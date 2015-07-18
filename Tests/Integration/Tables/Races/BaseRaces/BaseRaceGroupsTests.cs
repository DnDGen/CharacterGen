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

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                GroupConstants.Standard,
                AlignmentConstants.Evil,
                AlignmentConstants.Good,
                AlignmentConstants.Neutral,
                RaceConstants.Sizes.Large,
                RaceConstants.Sizes.Small,
                GroupConstants.Monsters,
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.Standard,
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
        [TestCase(RaceConstants.Sizes.Large,
            RaceConstants.BaseRaces.Minotaur,
            RaceConstants.BaseRaces.Ogre,
            RaceConstants.BaseRaces.OgreMage)]
        [TestCase(RaceConstants.Sizes.Small,
            RaceConstants.BaseRaces.Derro,
            RaceConstants.BaseRaces.ForestGnome,
            RaceConstants.BaseRaces.RockGnome,
            RaceConstants.BaseRaces.Svirfneblin,
            RaceConstants.BaseRaces.Goblin,
            RaceConstants.BaseRaces.DeepHalfling,
            RaceConstants.BaseRaces.LightfootHalfling,
            RaceConstants.BaseRaces.TallfellowHalfling,
            RaceConstants.BaseRaces.Kobold)]
        [TestCase(GroupConstants.Monsters,
            RaceConstants.BaseRaces.Bugbear,
            RaceConstants.BaseRaces.Derro,
            RaceConstants.BaseRaces.Doppelganger,
            RaceConstants.BaseRaces.Gnoll,
            RaceConstants.BaseRaces.Lizardfolk,
            RaceConstants.BaseRaces.MindFlayer,
            RaceConstants.BaseRaces.Minotaur,
            RaceConstants.BaseRaces.Ogre,
            RaceConstants.BaseRaces.OgreMage,
            RaceConstants.BaseRaces.Troglodyte)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}