﻿using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralAdeptBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1, 15)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 16, 25)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 29, 38)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 40, 48)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 49, 58)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 61, 62)]
        [TestCase(RaceConstants.BaseRaces.Human, 63, 90)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 91, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(string content, int lower, int upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 26)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 27)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 28)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 39)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 59)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 60)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 98)]
        public override void Percentile(string content, int roll)
        {
            base.Percentile(content, roll);
        }
    }
}