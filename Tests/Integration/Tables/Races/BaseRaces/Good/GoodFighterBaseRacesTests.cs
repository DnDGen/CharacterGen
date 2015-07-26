using System;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1, 3)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 4, 33)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 34, 41)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 43, 47)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 49, 50)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 53, 57)]
        [TestCase(RaceConstants.BaseRaces.Human, 58, 97)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.GrayElf, 42)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 48)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 51)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 52)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}