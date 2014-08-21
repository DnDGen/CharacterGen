﻿using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilBardBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 1)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 2)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 18)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 19)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 20)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 98)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 99)]
        [TestCase(EmptyContent, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElf, 3, 17)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 21, 22)]
        [TestCase(RaceConstants.BaseRaces.Human, 23, 97)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}