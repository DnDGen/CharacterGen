﻿using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class Level5FollowerQuantitiesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 5);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(1, 25).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(6, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(9, 0)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 0)]
        [TestCase(13, 0)]
        [TestCase(14, 0)]
        [TestCase(15, 0)]
        [TestCase(16, 0)]
        [TestCase(17, 0)]
        [TestCase(18, 0)]
        [TestCase(19, 1)]
        [TestCase(20, 1)]
        [TestCase(21, 1)]
        [TestCase(22, 2)]
        [TestCase(23, 2)]
        [TestCase(24, 2)]
        [TestCase(25, 2)]
        public void Level5FollowerQuantity(int leadershipScore, int cohortLevel)
        {
            base.Adjustment(leadershipScore.ToString(), cohortLevel);
        }
    }
}
