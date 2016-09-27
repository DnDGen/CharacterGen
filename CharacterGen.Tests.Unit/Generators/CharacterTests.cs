using CharacterGen.Abilities.Feats;
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
            Assert.That(character.Summary, Is.Empty);
        }

        [Test]
        public void IsLeaderIfHasLeadershipFeat()
        {
            character.Ability.Feats = new[]
            {
                new Feat { Name = FeatConstants.Leadership },
                new Feat { Name = "other feat" }
            };

            Assert.That(character.IsLeader, Is.True);
        }

        [Test]
        public void IsNotLeaderIfDoesNotHaveLeadershipFeat()
        {
            character.Ability.Feats = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" }
            };

            Assert.That(character.IsLeader, Is.False);
        }

        [Test]
        public void CharacterHasSummary()
        {
            character.Alignment.Goodness = "goodness";
            character.Alignment.Lawfulness = "lawfulness";
            character.Class.Level = 9266;
            character.Class.Name = "class name";
            character.Race.BaseRace = "base race";

            Assert.That(character.Summary, Is.EqualTo("lawfulness goodness Level 9266 base race class name"));
        }

        [Test]
        public void CharacterHasSummaryWithMetarace()
        {
            character.Alignment.Goodness = "goodness";
            character.Alignment.Lawfulness = "lawfulness";
            character.Class.Level = 9266;
            character.Class.Name = "class name";
            character.Race.BaseRace = "base race";
            character.Race.Metarace = "metarace";

            Assert.That(character.Summary, Is.EqualTo("lawfulness goodness Level 9266 metarace base race class name"));
        }
    }
}