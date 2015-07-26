﻿using System;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodDruidBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.GrayElf, 1)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 37)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 47)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 48)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 49)]
        [TestCase(EmptyContent, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 2, 11)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 12, 21)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 22, 31)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 32, 36)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 38, 46)]
        [TestCase(RaceConstants.BaseRaces.Human, 50, 99)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}