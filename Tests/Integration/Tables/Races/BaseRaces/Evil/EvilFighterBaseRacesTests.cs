using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 3, 4)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 6, 7)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 8, 12)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 15, 23)]
        [TestCase(RaceConstants.BaseRaces.Human, 24, 53)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 56, 80)]
        [TestCase(RaceConstants.BaseRaces.Orc, 82, 86)]
        [TestCase(RaceConstants.BaseRaces.Drow, 87, 88)]
        [TestCase(EmptyContent, 97, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 5)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 13)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 14)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 54)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 55)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 81)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 89)]
        [TestCase(RaceConstants.BaseRaces.Derro, 90)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 91)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 92)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 93)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 94)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 95)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 96)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}