using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces
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
                CharacterClassConstants.Adept,
                CharacterClassConstants.Aristocrat,
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Commoner,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Expert,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Warrior,
                CharacterClassConstants.Wizard
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
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [TestCase(CharacterClassConstants.Paladin)]
        [TestCase(AlignmentConstants.Good)]
        public void GoodBaseRaces(string name)
        {
            var baseRaces = new[]
            {
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
                RaceConstants.BaseRaces.Svirfneblin
            };

            base.DistinctCollection(name, baseRaces);
        }

        [TestCase(CharacterClassConstants.Adept)]
        [TestCase(CharacterClassConstants.Aristocrat)]
        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Commoner)]
        [TestCase(CharacterClassConstants.Druid)]
        [TestCase(CharacterClassConstants.Expert)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Warrior)]
        [TestCase(CharacterClassConstants.Wizard)]
        public void ClassBaseRaces(string className)
        {
            var baseRaces = new[]
            {
                RaceConstants.BaseRaces.Aasimar,
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.DeepDwarf,
                RaceConstants.BaseRaces.DeepHalfling,
                RaceConstants.BaseRaces.Derro,
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.ForestGnome,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.Goblin,
                RaceConstants.BaseRaces.GrayElf,
                RaceConstants.BaseRaces.HalfElf,
                RaceConstants.BaseRaces.HalfOrc,
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.Hobgoblin,
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.Kobold,
                RaceConstants.BaseRaces.LightfootHalfling,
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.BaseRaces.MindFlayer,
                RaceConstants.BaseRaces.Minotaur,
                RaceConstants.BaseRaces.MountainDwarf,
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.OgreMage,
                RaceConstants.BaseRaces.Orc,
                RaceConstants.BaseRaces.RockGnome,
                RaceConstants.BaseRaces.Svirfneblin,
                RaceConstants.BaseRaces.TallfellowHalfling,
                RaceConstants.BaseRaces.Tiefling,
                RaceConstants.BaseRaces.Troglodyte,
                RaceConstants.BaseRaces.WildElf,
                RaceConstants.BaseRaces.WoodElf
            };

            base.DistinctCollection(className, baseRaces);
        }
    }
}