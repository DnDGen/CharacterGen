using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [TestCase(RaceConstants.BaseRaces.WildElf, 1)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 22)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 23)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 69)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 70)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 71)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 87)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 91)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 92)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 93)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 94)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 95)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElf, 2, 16)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 17, 21)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 24, 28)]
        [TestCase(RaceConstants.BaseRaces.Human, 29, 68)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 72, 86)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 88, 90)]
        [TestCase(EmptyContent, 96, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}