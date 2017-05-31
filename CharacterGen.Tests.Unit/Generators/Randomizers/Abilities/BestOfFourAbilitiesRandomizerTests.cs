using CharacterGen.Domain.Generators.Randomizers.Abilities;
using CharacterGen.Randomizers.Abilities;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class BestOfFourAbilitiesRandomizerTests
    {
        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            randomizer = new BestOfFourAbilitiesRandomizer(mockDice.Object, generator);

            mockDice.Setup(d => d.Roll("4d6k3").AsSum()).Returns(3);
        }

        [Test]
        public void BestOfFourAbilitiesCalls4d6k3OncePerAbility()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll("4d6k3").AsSum(), Times.Exactly(stats.Count));
        }

        [Test]
        public void BestOfFourIgnoresLowestRollPerAbility()
        {
            mockDice.SetupSequence(d => d.Roll("4d6k3").AsSum()).Returns(9);

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(9));
        }
    }
}