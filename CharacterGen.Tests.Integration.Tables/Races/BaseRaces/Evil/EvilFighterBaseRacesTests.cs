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
        [TestCase(24, 53, RaceConstants.BaseRaces.Human)]
        [TestCase(54, 54, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(55, 55, RaceConstants.BaseRaces.Goblin)]
        [TestCase(56, 80, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(81, 81, RaceConstants.BaseRaces.Kobold)]
        [TestCase(82, 86, RaceConstants.BaseRaces.Orc)]
        [TestCase(87, 88, RaceConstants.BaseRaces.Drow)]
        [TestCase(89, 89, RaceConstants.BaseRaces.DuergarDwarf)]
        [TestCase(90, 90, RaceConstants.BaseRaces.Derro)]
        [TestCase(91, 91, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(92, 92, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(93, 93, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(94, 94, RaceConstants.BaseRaces.Ogre)]
        [TestCase(95, 95, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(96, 96, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(97, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}