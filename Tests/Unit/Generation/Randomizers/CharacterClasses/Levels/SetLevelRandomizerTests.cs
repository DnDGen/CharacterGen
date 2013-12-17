using System.Linq;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests
    {
        private SetLevelRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetLevelRandomizer();
            randomizer.Level = 9266;
        }

        [Test]
        public void RandomizeReturnSetLevel()
        {
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9266));
        }

        [Test]
        public void GetAllPossibleResultsReturnsSetLevel()
        {
            var levels = randomizer.GetAllPossibleResults();
            Assert.That(levels.First(), Is.EqualTo(9266));
            Assert.That(levels.Count(), Is.EqualTo(1));
        }
    }
}