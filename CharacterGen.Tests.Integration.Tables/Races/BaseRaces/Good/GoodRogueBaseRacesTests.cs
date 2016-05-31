using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRogueBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 5, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(6, 6, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(7, 19, RaceConstants.BaseRaces.HighElf)]
        [TestCase(20, 20, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(21, 25, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(26, 35, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(36, 60, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(61, 66, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(67, 72, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(73, 77, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(78, 96, RaceConstants.BaseRaces.Human)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(98, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}