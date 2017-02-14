using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRangerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(2, 6, RaceConstants.BaseRaces.HighElf)]
        [TestCase(7, 7, RaceConstants.BaseRaces.WildElf)]
        [TestCase(8, 34, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(35, 35, RaceConstants.BaseRaces.GraySlaad)]
        [TestCase(36, 36, RaceConstants.BaseRaces.Azer)]
        [TestCase(37, 37, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(38, 38, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(39, 55, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(56, 56, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(57, 57, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(58, 67, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(68, 96, RaceConstants.BaseRaces.Human)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Janni)]
        [TestCase(98, 99, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(100, 100, RaceConstants.BaseRaces.StoneGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}