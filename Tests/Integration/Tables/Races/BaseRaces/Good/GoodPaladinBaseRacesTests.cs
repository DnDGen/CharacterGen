using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodPaladinBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Paladin); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 1, 10)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 11, 20)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 23, 27)]
        [TestCase(RaceConstants.BaseRaces.Human, 31, 97)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 21)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 22)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 28)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 29)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 30)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}