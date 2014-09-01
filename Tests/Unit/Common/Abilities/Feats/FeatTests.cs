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
            Assert.That(feat.Name, Is.Empty);
            Assert.That(feat.SpecificApplication, Is.Empty);
        }

        [Test]
        public void NotEqualIfNotAFeat()
        {
            var thing = new Object();
            Assert.That(feat, Is.Not.EqualTo(thing));
        }

        [Test]
        public void NotEqualIfNamesDoNotMatch()
        {
            feat.Name = "name";
            feat.SpecificApplication = "spec app";

            var otherFeat = new Feat();
            otherFeat.Name = "other name";
            otherFeat.SpecificApplication = "spec app";

            Assert.That(feat, Is.Not.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.Not.EqualTo(feat));
        }

        [Test]
        public void NotEqualIfSpecificApplicationsDoNotMatch()
        {
            feat.Name = "name";
            feat.SpecificApplication = "spec app";

            var otherFeat = new Feat();
            otherFeat.Name = "name";
            otherFeat.SpecificApplication = "other spec app";

            Assert.That(feat, Is.Not.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.Not.EqualTo(feat));
        }

        [Test]
        public void EqualIfNamesAndSpecificiApplicationsMatch()
        {
            feat.Name = "name";
            feat.SpecificApplication = "spec app";

            var otherFeat = new Feat();
            otherFeat.Name = "name";
            otherFeat.SpecificApplication = "spec app";

            Assert.That(feat, Is.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.EqualTo(feat));
        }

        [Test]
        public void HashCodeIsHashOfNameANdHashOfSpecificApplicationAdded()
        {
            feat.Name = "name";
            feat.SpecificApplication = "specific application";

            var nameHash = feat.Name.GetHashCode();
            var specificApplicationHash = feat.SpecificApplication.GetHashCode();
            var featHashCode = feat.GetHashCode();

            Assert.That(featHashCode, Is.EqualTo(nameHash + specificApplicationHash));
        }
    }
}