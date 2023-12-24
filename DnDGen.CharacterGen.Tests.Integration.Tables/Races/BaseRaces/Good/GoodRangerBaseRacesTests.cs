using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRangerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 5, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(6, 20, RaceConstants.BaseRaces.HighElf)]
        [TestCase(21, 21, RaceConstants.BaseRaces.WildElf)]
        [TestCase(22, 34, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(35, 36, RaceConstants.BaseRaces.HoundArchon)]
        [TestCase(37, 41, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(42, 42, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(43, 57, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(58, 58, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(59, 59, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(60, 64, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(65, 96, RaceConstants.BaseRaces.Human)]
        [TestCase(97, 97, RaceConstants.BaseRaces.StormGiant)]
        [TestCase(98, 98, RaceConstants.BaseRaces.CloudGiant)]
        [TestCase(99, 100, RaceConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}