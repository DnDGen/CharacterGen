using NPCGen.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common
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
            Assert.That(leadership.Cohort, Is.Null);
            Assert.That(leadership.Followers, Is.Empty);
            Assert.That(leadership.Score, Is.EqualTo(0));
            Assert.That(leadership.LeadershipModifiers, Is.Empty);
        }
    }
}