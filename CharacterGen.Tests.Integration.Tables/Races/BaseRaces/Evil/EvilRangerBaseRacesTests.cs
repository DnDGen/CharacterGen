using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRangerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.HighElf)]
        [TestCase(2, 11, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(12, 28, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(29, 29, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(30, 30, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(31, 39, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(40, 69, RaceConstants.BaseRaces.Human)]
        [TestCase(70, 71, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(72, 72, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(73, 92, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(93, 93, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(94, 94, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(95, 95, RaceConstants.BaseRaces.Ogre)]
        [TestCase(96, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}