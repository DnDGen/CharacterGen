using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBarbarianBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.WildElf)]
        [TestCase(2, 3, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(4, 4, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(5, 5, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(6, 6, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(7, 29, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(30, 42, RaceConstants.BaseRaces.Human)]
        [TestCase(43, 44, RaceConstants.BaseRaces.HillGiant)]
        [TestCase(45, 49, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(50, 50, RaceConstants.BaseRaces.Goblin)]
        [TestCase(51, 51, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(52, 52, RaceConstants.BaseRaces.Kobold)]
        [TestCase(53, 74, RaceConstants.BaseRaces.Orc)]
        [TestCase(75, 75, RaceConstants.BaseRaces.DeathSlaad)]
        [TestCase(76, 76, RaceConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(77, 77, RaceConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(78, 78, RaceConstants.BaseRaces.Gargoyle)]
        [TestCase(79, 79, RaceConstants.BaseRaces.Troll)]
        [TestCase(80, 80, RaceConstants.BaseRaces.CloudGiant)]
        [TestCase(81, 82, RaceConstants.BaseRaces.Grimlock)]
        [TestCase(83, 83, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(84, 88, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(89, 89, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(90, 91, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(92, 95, RaceConstants.BaseRaces.Ogre)]
        [TestCase(96, 99, RaceConstants.BaseRaces.Minotaur)]
        [TestCase(100, 100, RaceConstants.BaseRaces.FrostGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}