using NPCGen.Common.Combats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Combats
{
    [TestFixture]
    public class ArmorClassTests
    {
        private ArmorClass armorClass;

        [SetUp]
        public void Setup()
        {
            armorClass = new ArmorClass();
        }

        [Test]
        public void ArmorClassInitialized()
        {
            Assert.That(armorClass.FlatFooted, Is.EqualTo(0));
            Assert.That(armorClass.Full, Is.EqualTo(0));
            Assert.That(armorClass.Touch, Is.EqualTo(0));
        }
    }
}