using NPCGen.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common
{
    [TestFixture]
    public class FamiliarTests
    {
        private Familiar familiar;

        [SetUp]
        public void Setup()
        {
            familiar = new Familiar();
        }

        [Test]
        public void FamiliarInitialized()
        {
            Assert.That(familiar.Animal, Is.Empty);
            Assert.That(familiar.ArmorClass, Is.EqualTo(0));
            Assert.That(familiar.HitPoints, Is.EqualTo(0));
        }
    }
}