using System.Linq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests
    {
        private SetClassNameRandomizer randomizer;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = "class name";
            alignment = new Alignment();
        }

        [Test]
        public void RandomizeReturnsSetClassName()
        {
            var className = randomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(randomizer.ClassName));
        }

        [Test]
        public void GetAllPossibleResultsReturnsSetClassName()
        {
            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames.Contains(randomizer.ClassName), Is.True);
            Assert.That(classNames.Count(), Is.EqualTo(1));
        }
    }
}