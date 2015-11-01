using CharacterGen.Selectors;
using CharacterGen.Selectors.Domain;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class LeadershipSelectorTests
    {
        private ILeadershipSelector leadershipSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            leadershipSelector = new LeadershipSelector(mockAdjustmentsSelector.Object);

            var cohortLevels = new Dictionary<String, Int32>();
            cohortLevels["2"] = 1;
            cohortLevels["3"] = 2;
            cohortLevels["4"] = 3;

            var followerLevels = new Dictionary<String, Dictionary<String, Int32>>();
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 1)] = new Dictionary<String, Int32>();
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 2)] = new Dictionary<String, Int32>();
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 3)] = new Dictionary<String, Int32>();
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 4)] = new Dictionary<String, Int32>();
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 5)] = new Dictionary<String, Int32>();
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 6)] = new Dictionary<String, Int32>();

            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 1)]["2"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 1)]["3"] = 3;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 1)]["4"] = 4;

            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 2)]["2"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 2)]["3"] = 2;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 2)]["4"] = 3;

            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 3)]["2"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 3)]["3"] = 1;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 3)]["4"] = 3;

            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 4)]["2"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 4)]["3"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 4)]["4"] = 2;

            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 5)]["2"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 5)]["3"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 5)]["4"] = 2;

            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 6)]["2"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 6)]["3"] = 0;
            followerLevels[String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 6)]["4"] = 1;

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(It.IsAny<String>())).Returns((String s) => followerLevels[s]);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.CohortLevels)).Returns(cohortLevels);
        }

        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        public void ReturnCohortLevelFromTable(Int32 leadershipScore, Int32 cohortLevel)
        {
            var level = leadershipSelector.SelectCohortLevelFor(leadershipScore);
            Assert.That(level, Is.EqualTo(cohortLevel));
        }

        [Test]
        public void IfCohortLeadershipScoreIsHigherThanMax_ReturnMax()
        {
            var level = leadershipSelector.SelectCohortLevelFor(5);
            Assert.That(level, Is.EqualTo(3));
        }

        [Test]
        public void IfCohortLeadershipScoreIsLowerThanMin_Return0()
        {
            var level = leadershipSelector.SelectCohortLevelFor(1);
            Assert.That(level, Is.EqualTo(0));
        }

        [TestCase(2, 0, 0, 0, 0, 0, 0)]
        [TestCase(3, 3, 2, 1, 0, 0, 0)]
        [TestCase(4, 4, 3, 3, 2, 2, 1)]
        public void ReturnFollowerQuantities(Int32 leadershipScore, Int32 level1, Int32 level2, Int32 level3, Int32 level4, Int32 level5, Int32 level6)
        {
            var followerQuantities = leadershipSelector.SelectFollowerQuantitiesFor(leadershipScore);
            Assert.That(followerQuantities.Level1, Is.EqualTo(level1));
            Assert.That(followerQuantities.Level2, Is.EqualTo(level2));
            Assert.That(followerQuantities.Level3, Is.EqualTo(level3));
            Assert.That(followerQuantities.Level4, Is.EqualTo(level4));
            Assert.That(followerQuantities.Level5, Is.EqualTo(level5));
            Assert.That(followerQuantities.Level6, Is.EqualTo(level6));
        }

        [Test]
        public void IfFollowerLeadershipScoreIsHigherThanMax_ReturnMax()
        {
            var followerQuantities = leadershipSelector.SelectFollowerQuantitiesFor(5);
            Assert.That(followerQuantities.Level1, Is.EqualTo(4));
            Assert.That(followerQuantities.Level2, Is.EqualTo(3));
            Assert.That(followerQuantities.Level3, Is.EqualTo(3));
            Assert.That(followerQuantities.Level4, Is.EqualTo(2));
            Assert.That(followerQuantities.Level5, Is.EqualTo(2));
            Assert.That(followerQuantities.Level6, Is.EqualTo(1));
        }

        [Test]
        public void IfFollowerLeadershipScoreIsLowerThanMin_ReturnAll0()
        {
            var followerQuantities = leadershipSelector.SelectFollowerQuantitiesFor(1);
            Assert.That(followerQuantities.Level1, Is.EqualTo(0));
            Assert.That(followerQuantities.Level2, Is.EqualTo(0));
            Assert.That(followerQuantities.Level3, Is.EqualTo(0));
            Assert.That(followerQuantities.Level4, Is.EqualTo(0));
            Assert.That(followerQuantities.Level5, Is.EqualTo(0));
            Assert.That(followerQuantities.Level6, Is.EqualTo(0));
        }
    }
}
