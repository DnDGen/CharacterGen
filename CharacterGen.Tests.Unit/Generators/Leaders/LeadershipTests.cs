﻿using CharacterGen.Leaders;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Leaders
{
    [TestFixture]
    public class LeadershipTests
    {
        private Leadership leadership;

        [SetUp]
        public void Setup()
        {
            leadership = new Leadership();
        }

        [Test]
        public void LeadershipIsInitialized()
        {
            Assert.That(leadership.Score, Is.EqualTo(0));
            Assert.That(leadership.LeadershipModifiers, Is.Empty);
            Assert.That(leadership.CohortScore, Is.EqualTo(0));
            Assert.That(leadership.FollowerQuantities, Is.Not.Null);
        }
    }
}