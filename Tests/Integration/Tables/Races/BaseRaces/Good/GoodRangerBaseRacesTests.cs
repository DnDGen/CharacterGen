using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 1, 5)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 6, 20)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 22, 36)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 37, 41)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 43, 57)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 60, 64)]
        [TestCase(RaceConstants.BaseRaces.Human, 65, 97)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.WildElf, 21)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 42)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 58)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 59)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}