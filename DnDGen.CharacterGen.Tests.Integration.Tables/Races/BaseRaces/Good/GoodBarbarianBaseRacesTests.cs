using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBarbarianBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(3, 32, RaceConstants.BaseRaces.WildElf)]
        [TestCase(33, 34, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(35, 35, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(36, 36, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(37, 61, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(62, 97, RaceConstants.BaseRaces.Human)]
        [TestCase(98, 98, RaceConstants.BaseRaces.StormGiant)]
        [TestCase(99, 99, RaceConstants.BaseRaces.CloudGiant)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}