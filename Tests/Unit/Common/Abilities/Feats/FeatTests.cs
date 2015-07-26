using CharacterGen.Common.Abilities.Feats;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Abilities.Feats
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
            Assert.That(feat.Focus, Is.Empty);
            Assert.That(feat.Strength, Is.EqualTo(0));
            Assert.That(feat.Frequency, Is.Not.Null);
        }
    }
}