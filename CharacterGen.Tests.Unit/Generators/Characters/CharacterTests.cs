using CharacterGen.Characters;
using CharacterGen.Feats;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Characters
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
            Assert.That(character.Feats, Is.Empty);
            Assert.That(character.Languages, Is.Empty);
            Assert.That(character.Skills, Is.Empty);
            Assert.That(character.Abilities, Is.Empty);
            Assert.That(character.Combat, Is.Not.Null);
            Assert.That(character.Equipment, Is.Not.Null);
            Assert.That(character.Magic, Is.Not.Null);
            Assert.That(character.IsLeader, Is.False);
            Assert.That(character.Summary, Is.Empty);
        }

        [Test]
        public void IsLeaderIfHasLeadershipFeat()
        {
            character.Feats = new[]
            {
                new Feat { Name = FeatConstants.Leadership },
                new Feat { Name = "other feat" }
            };

            Assert.That(character.IsLeader, Is.True);
        }

        [Test]
        public void IsNotLeaderIfDoesNotHaveLeadershipFeat()
        {
            character.Feats = new[]
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

            Assert.That(character.Summary, Is.EqualTo("lawfulness goodness Female base race Level 9266 class name"));
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
            character.Race.IsMale = true;

            Assert.That(character.Summary, Is.EqualTo("lawfulness goodness Male metarace base race Level 9266 class name"));
        }

        [Test]
        public void ChallengeRatingForCharacterIsClassLevel()
        {
            character.Class.Level = 9266;
            Assert.That(character.ChallengeRating, Is.EqualTo(9266));
        }

        [Test]
        public void ChallengeRatingForCharacterIsClassLevelAndRacialChallengeRating()
        {
            character.Class.Level = 9266;
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void ChallengeRatingForNPCIsHalfOfClassLevel()
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = true;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266 / 2));
        }

        [Test]
        public void ChallengeRatingForNPCIsHalfOfClassLevelAndAllOfRacialChallengeRating()
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = true;
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266 / 2 + 90210));
        }

        [Test]
        public void ChallengeRatingForLevel1NPCIsOneHalf()
        {
            character.Class.Level = 1;
            character.Class.IsNPC = true;

            Assert.That(character.ChallengeRating, Is.EqualTo(.5));
        }

        [Test]
        public void ChallengeRatingForLevel1NPCWithRacialChallengeRatingIsRacialChallengeRating()
        {
            character.Class.Level = 1;
            character.Class.IsNPC = true;
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(90210));
        }

        [TestCase(RaceConstants.BaseRaces.Drow)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf)]
        public void SpecialNPCChallengeRatingIsClassLevel(string baseRace)
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = true;
            character.Race.BaseRace = baseRace;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266));
        }

        [TestCase(RaceConstants.BaseRaces.Drow)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf)]
        public void SpecialCharacterChallengeRatingIsClassLevelPlus1(string baseRace)
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = false;
            character.Race.BaseRace = baseRace;

            Assert.That(character.ChallengeRating, Is.EqualTo(9267));
        }

        [TestCase(RaceConstants.BaseRaces.Drow)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf)]
        public void SpecialNPCChallengeRatingTakesRacialChallengeRatingIntoAccount(string baseRace)
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = true;
            character.Race.BaseRace = baseRace;
            character.Race.Metarace = "metarace";
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(9266 + 90210));
        }

        [TestCase(RaceConstants.BaseRaces.Drow)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf)]
        public void SpecialCharacterChallengeRatingTakesRacialChallengeRatingIntoAccount(string baseRace)
        {
            character.Class.Level = 9266;
            character.Class.IsNPC = false;
            character.Race.BaseRace = baseRace;
            character.Race.Metarace = "metarace";
            character.Race.ChallengeRating = 90210;

            Assert.That(character.ChallengeRating, Is.EqualTo(9267 + 90210));
        }
    }
}