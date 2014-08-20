using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests
    {
        private ISetLevelRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetLevelRandomizer();
        }

        [Test]
        public void SetLevelRandomizerIsALevelRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<ILevelRandomizer>());
        }

        [Test]
        public void ReturnSetLevel()
        {
            randomizer.SetLevel = 9266;
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9266));
        }

        [Test]
        public void ReturnJustSetLevel()
        {
            randomizer.SetLevel = 9266;

            var levels = randomizer.GetAllPossibleResults();
            Assert.That(levels, Contains.Item(9266));
            Assert.That(levels.Count(), Is.EqualTo(1));
        }
    }
}