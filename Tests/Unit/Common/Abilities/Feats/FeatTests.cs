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

            var otherFeat = new Feat();
            otherFeat.Name.Id = "other name";
            otherFeat.SpecificApplication = "spec app";

            Assert.That(feat, Is.Not.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.Not.EqualTo(feat));
        }

        [Test]
        public void NotEqualIfSpecificApplicationsDoNotMatch()
        {
            feat.Name.Id = "name";
            feat.SpecificApplication = "spec app";

            var otherFeat = new Feat();
            otherFeat.Name.Id = "name";
            otherFeat.SpecificApplication = "other spec app";

            Assert.That(feat, Is.Not.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.Not.EqualTo(feat));
        }

        [Test]
        public void EqualIfIdsAndSpecificiApplicationsMatch()
        {
            feat.Name.Id = "name";
            feat.SpecificApplication = "spec app";

            var otherFeat = new Feat();
            otherFeat.Name.Id = "name";
            otherFeat.SpecificApplication = "spec app";

            Assert.That(feat, Is.EqualTo(otherFeat));
            Assert.That(otherFeat, Is.EqualTo(feat));
        }

        [Test]
        public void HashCodeIsHashOfIdAndHashOfSpecificApplicationAdded()
        {
            feat.Name.Id = "name";
            feat.SpecificApplication = "specific application";

            var idHash = feat.Name.Id.GetHashCode();
            var specificApplicationHash = feat.SpecificApplication.GetHashCode();
            var featHashCode = feat.GetHashCode();

            Assert.That(featHashCode, Is.EqualTo(idHash + specificApplicationHash));
        }
    }
}