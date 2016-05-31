using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilCommonerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Commoner); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(11, 15, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(16, 25, RaceConstants.BaseRaces.HighElf)]
        [TestCase(26, 35, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(36, 45, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(46, 91, RaceConstants.BaseRaces.Human)]
        [TestCase(92, 92, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(93, 93, RaceConstants.BaseRaces.Goblin)]
        [TestCase(94, 94, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(95, 95, RaceConstants.BaseRaces.Kobold)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Orc)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(98, 98, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(99, 99, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(100, 100, RaceConstants.BaseRaces.WildElf)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
