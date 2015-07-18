using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 3)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 39)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 40)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 86)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 87)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 94)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElf, 4, 18)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 19, 38)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 41, 50)]
        [TestCase(RaceConstants.BaseRaces.Human, 51, 70)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 71, 85)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 88, 89)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 90, 93)]
        [TestCase(EmptyContent, 95, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}