using CharacterGen.Common;
using CharacterGen.Common.Abilities.Feats;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common
{
    [TestFixture]
    public class CharacterTests
    {
        private Character character;

        [SetUp]
        public void Setup()
        {
            character = new Character();
        }

        [Test]
        public void CharacterInitialized()
        {
            Assert.That(character.Alignment, Is.Not.Null);
            Assert.That(character.Class, Is.Not.Null);
            Assert.That(character.InterestingTrait, Is.Empty);
            Assert.That(character.Race, Is.Not.Null);
            Assert.That(character.Ability, Is.Not.Null);
            Assert.That(character.Combat, Is.Not.Null);
            Assert.That(character.Equipment, Is.Not.Null);
            Assert.That(character.Magic, Is.Not.Null);
            Assert.That(character.IsLeader, Is.False);
        }

        [Test]
        public void IsLeaderIfHasLeadershipFeat()
        {
            character.Ability.Feats = new[] { new Feat { Name = FeatConstants.Leadership } };
            Assert.That(character.IsLeader, Is.True);
        }

        [Test]
        public void IsNotLeaderIfDoesNotHaveLeadershipFeat()
        {
            character.Ability.Feats = new[] { new Feat { Name = "other feat" } };
            Assert.That(character.IsLeader, Is.False);
        }
    }
}