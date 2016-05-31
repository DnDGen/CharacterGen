using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodPaladinBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Paladin); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, RaceConstants.BaseRaces.Aasimar)]
        [TestCase(11, 20, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(21, 21, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(22, 22, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(23, 27, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(28, 28, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(29, 29, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(30, 30, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(31, 97, RaceConstants.BaseRaces.Human)]
        [TestCase(98, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}