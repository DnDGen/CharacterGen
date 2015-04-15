using System;
using NPCGen.Common.Abilities.Feats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities.Feats
{
    [TestFixture]
    public class FeatTests
    {
        private Feat feat;

        [SetUp]
        public void Setup()
        {
            feat = new Feat();
        }

        [Test]
        public void FeatInitialized()
        {
            Assert.That(feat.Name, Is.Not.Null);
            Assert.That(feat.SpecificApplication, Is.Empty);
            Assert.That(feat.Strength, Is.EqualTo(0));
        }

        [Test]
        public void NotEqualIfNotAFeat()
        {
            var thing = new Object();
            Assert.That(feat, Is.Not.EqualTo(thing));
        }

        [Test]
        public void NotEqualIfIdsDoNotMatch()
        {
            feat.Name.Id = "name";
            feat.SpecificApplication = "spec app";
            feat.Strength = 9266;

            var otherFeat = new Feat();
            otherFeat.Name.Id = "other name";
            otherFeat.SpecificApplication = "spec app";
            otherFeat.Strength = 9266;

            Assert.That(feat, Is.Not.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.Not.EqualTo(feat));
        }

        [Test]
        public void NotEqualIfSpecificApplicationsDoNotMatch()
        {
            feat.Name.Id = "name";
            feat.SpecificApplication = "spec app";
            feat.Strength = 9266;

            var otherFeat = new Feat();
            otherFeat.Name.Id = "name";
            otherFeat.SpecificApplication = "other spec app";
            otherFeat.Strength = 9266;

            Assert.That(feat, Is.Not.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.Not.EqualTo(feat));
        }

        [Test]
        public void NotEqualIfStrengthsDoNotMatch()
        {
            feat.Name.Id = "name";
            feat.SpecificApplication = "spec app";
            feat.Strength = 9266;

            var otherFeat = new Feat();
            otherFeat.Name.Id = "name";
            otherFeat.SpecificApplication = "other spec app";
            otherFeat.Strength = 42;

            Assert.That(feat, Is.Not.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.Not.EqualTo(feat));
        }

        [Test]
        public void EqualIfIdsAndSpecificiApplicationsAndStrengthsMatch()
        {
            feat.Name.Id = "name";
            feat.SpecificApplication = "spec app";
            feat.Strength = 9266;

            var otherFeat = new Feat();
            otherFeat.Name.Id = "name";
            otherFeat.SpecificApplication = "spec app";
            otherFeat.Strength = 9266;

            Assert.That(feat, Is.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.EqualTo(feat));
        }

        [Test]
        public void HashCodeIsHashOfNameAndHashOfSpecificApplicationAndHasOfStrengthAdded()
        {
            feat.Name.Id = "nameId";
            feat.Name.Name = "name ID";
            feat.SpecificApplication = "specific application";
            feat.Strength = 9266;

            var nameHash = feat.Name.GetHashCode();
            var specificApplicationHash = feat.SpecificApplication.GetHashCode();
            var strengthHash = feat.Strength.GetHashCode();
            var featHashCode = feat.GetHashCode();

            Assert.That(featHashCode, Is.EqualTo(nameHash + specificApplicationHash + strengthHash));
        }
    }
}