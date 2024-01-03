using DnDGen.CharacterGen.Generators.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class OnesAsSixesAbilitiesRandomizerTests
    {
        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            randomizer = new OnesAsSixesAbilitiesRandomizer(mockDice.Object);

            mockDice.Setup(d => d.Roll(3).d(6).AsIndividualRolls()).Returns(new[] { 2, 3, 4 });
        }

        [Test]
        public void OnesAsSixesAbilitiesCalls3d6OncePerAbility()
        {
            var abilities = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsIndividualRolls(), Times.Exactly(abilities.Count));
        }

        [Test]
        public void OnesAsSixesTreatsOnesAsSixes()
        {
            mockDice.Setup(d => d.Roll(3).d(6).AsIndividualRolls()).Returns(new[] { 2, 3, 1 });

            var abilities = randomizer.Randomize();
            var ability = abilities.Values.First();
            Assert.That(ability.Value, Is.EqualTo(11));
        }
    }
}