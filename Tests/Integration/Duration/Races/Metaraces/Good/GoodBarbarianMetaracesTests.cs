﻿using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodBarbarianMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodBarbarianMetaraces";
        }

        [Test]
        public void GoodBarbarianEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void GoodBarbarianHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 99);
        }

        [Test]
        public void GoodBarbarianHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}