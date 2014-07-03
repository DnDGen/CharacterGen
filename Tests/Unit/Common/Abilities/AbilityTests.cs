using NPCGen.Common.Abilities;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities
{
    [TestFixture]
    public class AbilityTests
    {
        private Ability ability;

        [SetUp]
        public void Setup()
        {
            ability = new Ability();
        }

        [Test]
        public void AbilityInitialized()
        {
            Assert.That(ability.Feats, Is.Empty);
            Assert.That(ability.Languages, Is.Empty);
            Assert.That(ability.Skills, Is.Empty);
            Assert.That(ability.Stats, Is.Empty);
        }
    }
}