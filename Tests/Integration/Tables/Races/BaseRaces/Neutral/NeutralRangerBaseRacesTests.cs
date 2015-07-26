using System;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 2, 6)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 8, 36)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 39, 55)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 58, 67)]
        [TestCase(RaceConstants.BaseRaces.Human, 68, 96)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 97, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 1)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 7)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 37)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 38)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 56)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 57)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}