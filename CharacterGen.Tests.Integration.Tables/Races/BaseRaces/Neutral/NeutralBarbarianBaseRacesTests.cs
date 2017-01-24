using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBarbarianBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(2, 2, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(3, 13, RaceConstants.BaseRaces.WildElf)]
        [TestCase(14, 14, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(15, 16, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(17, 18, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(19, 19, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(20, 58, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(59, 87, RaceConstants.BaseRaces.Human)]
        [TestCase(88, 88, RaceConstants.BaseRaces.Janni)]
        [TestCase(89, 99, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(100, 100, RaceConstants.BaseRaces.StoneGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}