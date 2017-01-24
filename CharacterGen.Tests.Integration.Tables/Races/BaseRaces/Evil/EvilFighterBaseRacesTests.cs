using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilFighterBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 4, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(5, 5, RaceConstants.BaseRaces.HighElf)]
        [TestCase(6, 7, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(8, 12, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(13, 13, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(14, 14, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(15, 23, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(24, 55, RaceConstants.BaseRaces.Human)]
        [TestCase(56, 56, RaceConstants.BaseRaces.CloudGiant)]
        [TestCase(57, 57, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(58, 58, RaceConstants.BaseRaces.Goblin)]
        [TestCase(59, 77, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(78, 78, RaceConstants.BaseRaces.Scorpionfolk)]
        [TestCase(79, 79, RaceConstants.BaseRaces.Grimlock)]
        [TestCase(80, 80, RaceConstants.BaseRaces.HillGiant)]
        [TestCase(81, 81, RaceConstants.BaseRaces.FireGiant)]
        [TestCase(82, 83, RaceConstants.BaseRaces.Troll)]
        [TestCase(84, 84, RaceConstants.BaseRaces.Kobold)]
        [TestCase(85, 89, RaceConstants.BaseRaces.Orc)]
        [TestCase(90, 91, RaceConstants.BaseRaces.Drow)]
        [TestCase(92, 92, RaceConstants.BaseRaces.DuergarDwarf)]
        [TestCase(93, 93, RaceConstants.BaseRaces.Derro)]
        [TestCase(94, 94, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(95, 95, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Ogre)]
        [TestCase(98, 98, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(99, 99, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, RaceConstants.BaseRaces.FrostGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}