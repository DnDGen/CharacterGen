﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class PercentileTests : IntegrationTests
    {
        [Inject]
        public IPercentileMapper PercentileMapper { get; set; }

        protected const String EmptyContent = "";

        protected abstract String tableName { get; }

        private Dictionary<Int32, String> table;
        private List<Int32> testedRolls;

        public PercentileTests()
        {
            testedRolls = new List<Int32>();
        }

        [SetUp]
        public void PercentileSetup()
        {
            table = PercentileMapper.Map(tableName);
        }

        [Test]
        public void TableIsComplete()
        {
            for (var roll = 100; roll > 0; roll--)
                Assert.That(table.Keys, Contains.Item(roll), tableName);

            Assert.That(table.Keys.Count, Is.EqualTo(100), tableName);
        }

        [Test, TestFixtureTearDown]
        public void AllRollsTested()
        {
            var missingRolls = table.Keys.Except(testedRolls);
            Assert.That(missingRolls, Is.Empty, tableName);
        }

        protected void AssertPercentile(String content, Int32 lower, Int32 upper)
        {
            for (var roll = lower; roll <= upper; roll++)
                AssertPercentile(content, roll);
        }

        protected void AssertPercentile(String content, Int32 roll)
        {
            var testedBefore = testedRolls.Contains(roll);
            Assert.That(testedBefore, Is.False);

            testedRolls.Add(roll);

            Assert.That(table.Keys, Contains.Item(roll), tableName);

            var message = String.Format("Roll: {0}", roll);
            Assert.That(table[roll], Is.EqualTo(content), message);
        }
    }
}