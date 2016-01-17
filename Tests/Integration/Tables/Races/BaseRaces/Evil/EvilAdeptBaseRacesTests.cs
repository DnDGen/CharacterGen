﻿using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilAdeptBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 6, 8)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 9, 18)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 19, 20)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 23, 25)]
        [TestCase(RaceConstants.BaseRaces.Human, 26, 56)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 57, 63)]
        [TestCase(RaceConstants.BaseRaces.Drow, 69, 71)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 73, 74)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 75, 89)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 90, 91)]
        [TestCase(EmptyContent, 96, 100)]
        public override void Percentile(string content, int lower, int upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 3)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 4)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 5)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 21)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 22)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 64)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 65)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 66)]
        [TestCase(RaceConstants.BaseRaces.Orc, 67)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 68)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 72)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 92)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 93)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 94)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 95)]
        public override void Percentile(string content, int roll)
        {
            base.Percentile(content, roll);
        }
    }
}