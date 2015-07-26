using System;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 1)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 29)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 30)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 72)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 93)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 94)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 95)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElf, 2, 11)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 12, 28)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 31, 39)]
        [TestCase(RaceConstants.BaseRaces.Human, 40, 69)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 70, 71)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 73, 92)]
        [TestCase(EmptyContent, 96, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}