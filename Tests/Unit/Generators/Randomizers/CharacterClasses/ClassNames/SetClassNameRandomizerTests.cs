using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests
    {
        private ISetClassNameRandomizer randomizer;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetClassNameRandomizer();
            alignment = new Alignment();
        }

        [Test]
        public void SetClassNameRandomizerIsAClassNameRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<IClassNameRandomizer>());
        }

        [Test]
        public void ReturnSetClassName()
        {
            randomizer.SetClassName = "class name";
            var className = randomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo("class name"));
        }

        [Test]
        public void ReturnJustSetClassName()
        {
            randomizer.SetClassName = "class name";

            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Contains.Item("class name"));
            Assert.That(classNames.Count(), Is.EqualTo(1));
        }
    }
}